using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace WeCantSpell.Hunspell;

public readonly struct AffixEntryGroupCollection<TEntry> : IReadOnlyList<AffixEntryGroup<TEntry>> where TEntry : AffixEntry
{
    public static AffixEntryGroupCollection<TEntry> Empty { get; } = new(Array.Empty<AffixEntryGroup<TEntry>>());

    public static AffixEntryGroupCollection<TEntry> Create(IEnumerable<AffixEntryGroup<TEntry>> groups) =>
        new((groups ?? throw new ArgumentNullException(nameof(groups))).ToArray());

    internal AffixEntryGroupCollection(AffixEntryGroup<TEntry>[] groups)
    {
#if DEBUG
        if (groups is null) throw new ArgumentNullException(nameof(groups));
#endif
        Groups = groups;
    }

    internal AffixEntryGroup<TEntry>[] Groups { get; }

    public int Count => Groups.Length;
    public bool IsEmpty => !HasItems;
    public bool HasItems => Groups is { Length: > 0 };
    public AffixEntryGroup<TEntry> this[int index] => Groups[index];
    public Enumerator<AffixEntryGroup<TEntry>> GetEnumerator() => new(Groups);
    IEnumerator<AffixEntryGroup<TEntry>> IEnumerable<AffixEntryGroup<TEntry>>.GetEnumerator() => ((IEnumerable<AffixEntryGroup<TEntry>>)Groups).GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => Groups.GetEnumerator();

    public struct Enumerator<TValue> : IEnumerator<TValue>
    {
        internal Enumerator(TValue[] items)
        {
            _items = items;
            _index = -1;
        }

        private TValue[] _items;
        private int _index;

        public TValue Current
        {
            get
            {
                return _index >= 0 && _index < _items.Length ? _items[_index] : throwBadIndex();
                static TValue throwBadIndex() => throw new InvalidOperationException("Invalid index");
            }
        }

        object IEnumerator.Current => (object)Current!;

        public bool MoveNext()
        {
            if (_index < _items.Length)
            {
                return ++_index < _items.Length;
            }

            return false;
        }

        public void Reset()
        {
            _index = 0;
        }

        public void Dispose()
        {
        }
    }
}
