using System;

#if !NO_INLINE
using System.Runtime.CompilerServices;
#endif

namespace WeCantSpell.Hunspell.Infrastructure
{
    ref struct SimulatedCString
    {
        public SimulatedCString(ReadOnlySpan<char> text)
        {
            buffer = text.ToArray();
            cachedSpan = buffer.AsSpan();
            cachedString = null;
            cacheRequiresRefresh = true;
        }

        private char[] buffer;
        private string cachedString;
        private Span<char> cachedSpan;
        private bool cacheRequiresRefresh;

        public char this[int index]
        {
            get => index < 0 || index >= buffer.Length ? '\0' : buffer[index];
            set
            {
                ResetCache();
                buffer[index] = value;
            }
        }

        public int BufferLength
        {
#if !NO_INLINE
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            get => buffer.Length;
        }

        public void WriteChars(string text, int destinationIndex)
        {
            WriteChars(text.AsSpan(), destinationIndex);
        }

        public void WriteChars(ReadOnlySpan<char> text, int destinationIndex)
        {
            ResetCache();

            var neededLength = text.Length + destinationIndex;
            if (buffer.Length < neededLength)
            {
                Array.Resize(ref buffer, neededLength);
            }

            text.CopyTo(buffer.AsSpan(destinationIndex));
        }

        public void WriteChars(int sourceIndex, ReadOnlySpan<char> text, int destinationIndex)
        {
            WriteChars(text.Slice(sourceIndex), destinationIndex);
        }

        public void Assign(ReadOnlySpan<char> text)
        {
#if DEBUG
            if (text.Length > buffer.Length) throw new ArgumentOutOfRangeException(nameof(text));
#endif
            ResetCache();

            text.CopyTo(buffer.AsSpan());
            if (text.Length < buffer.Length)
            {
                Array.Clear(buffer, text.Length, buffer.Length - text.Length);
            }
        }

        public void Destroy()
        {
            ResetCache();
            buffer = null;
        }

        public override string ToString() =>
            cachedString ?? (cachedString = GetTerminatedSpan().ToString());

        public Span<char> GetTerminatedSpan()
        {
            if (cacheRequiresRefresh)
            {
                var nullIndex = Array.IndexOf(buffer, '\0');
                cachedSpan = nullIndex >= 0
                    ? buffer.AsSpan(0, nullIndex)
                    : buffer.AsSpan();
                cacheRequiresRefresh = false;
            }

            return cachedSpan;
        }

        private void ResetCache()
        {
            cacheRequiresRefresh = true;
            cachedString = null;
        }
    }
}
