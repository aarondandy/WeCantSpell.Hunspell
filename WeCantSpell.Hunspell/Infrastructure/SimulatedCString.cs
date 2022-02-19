﻿using System;

#if !NO_INLINE
using System.Runtime.CompilerServices;
#endif

namespace WeCantSpell.Hunspell.Infrastructure
{
    ref struct SimulatedCString
    {
        public SimulatedCString(string text)
        {
            buffer = text.ToCharArray();
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
            ResetCache();

            var neededLength = text.Length + destinationIndex;
            if (buffer.Length < neededLength)
            {
                Array.Resize(ref buffer, neededLength);
            }

            text.CopyTo(0, buffer, destinationIndex, text.Length);
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

        public void Assign(string text)
        {
#if DEBUG
            if (text == null) throw new ArgumentNullException(nameof(text));
            if (text.Length > buffer.Length) throw new ArgumentOutOfRangeException(nameof(text));
#endif
            ResetCache();

            text.CopyTo(0, buffer, 0, text.Length);

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
                cacheRequiresRefresh = false;
                cachedSpan = buffer.AsSpan(0, FindTerminatedLength());
            }

            return cachedSpan;
        }

        private void ResetCache()
        {
            cacheRequiresRefresh = true;
            cachedString = null;
        }

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private int FindTerminatedLength()
        {
            var length = Array.IndexOf(buffer, '\0');
            return length < 0 ? buffer.Length : length;
        }
    }
}
