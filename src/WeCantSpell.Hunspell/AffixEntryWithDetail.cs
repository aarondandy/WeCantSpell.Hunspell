using System;

#if !NO_INLINE
using System.Runtime.CompilerServices;
#endif

namespace WeCantSpell.Hunspell
{
    public sealed class AffixEntryWithDetail<TEntry>
        where TEntry : AffixEntry
    {
        public AffixEntryWithDetail(AffixEntryGroup<TEntry> group, TEntry entry)
        {
            AffixEntry = entry ?? throw new ArgumentNullException(nameof(entry));

            if (group == null)
            {
                throw new ArgumentNullException(nameof(group));
            }

            AFlag = group.AFlag;
            Options = group.Options;
        }

        public TEntry AffixEntry
        {
#if !NO_INLINE
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            get;
        }

        public FlagValue AFlag
        {
#if !NO_INLINE
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            get;
        }

        public AffixEntryOptions Options
        {
#if !NO_INLINE
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            get;
        }

        public string Key
        {
#if !NO_INLINE
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            get => AffixEntry.Key;
        }

        public string Append
        {
#if !NO_INLINE
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            get => AffixEntry.Append;
        }

        public string Strip
        {
#if !NO_INLINE
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            get => AffixEntry.Strip;
        }

        public bool HasContClasses
        {
#if !NO_INLINE
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            get => AffixEntry.ContClass.HasItems;
        }

        public CharacterConditionGroup Conditions
        {
#if !NO_INLINE
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            get => AffixEntry.Conditions;
        }

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public bool ContainsContClass(FlagValue value) => AffixEntry.ContainsContClass(value);

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public bool ContainsAnyContClass(FlagValue a, FlagValue b) => AffixEntry.ContainsAnyContClass(a, b);
    }
}
