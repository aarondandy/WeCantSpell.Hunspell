using System;
using System.Text;

#if !NO_INLINE
using System.Runtime.CompilerServices;
#endif

namespace WeCantSpell.Hunspell.Infrastructure
{
    sealed class SimulatedCString
    {
        public SimulatedCString(ReadOnlySpan<char> text)
        {
            Buffer = StringBuilderPool.Get(text);
        }

        private StringBuilder Buffer;

        private string toStringCache = null;

        public char this[int index]
        {
            get => index < 0 || index >= Buffer.Length ? '\0' : Buffer[index];
            set
            {
                toStringCache = null;
                Buffer[index] = value;
            }
        }

        public int BufferLength
        {
#if !NO_INLINE
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            get => Buffer.Length;
        }

        public void WriteChars(string text, int destinationIndex)
        {
            toStringCache = null;
            Buffer.WriteChars(text.AsSpan(), destinationIndex);
        }

        public void WriteChars(int sourceIndex, ReadOnlySpan<char> text, int destinationIndex)
        {
            toStringCache = null;
            Buffer.WriteChars(sourceIndex, text, destinationIndex);
        }

        public void Assign(ReadOnlySpan<char> text)
        {
            toStringCache = null;
            Buffer.Clear();
            Buffer.Append(text);
        }

        public void Destroy()
        {
            if (Buffer != null)
            {
                StringBuilderPool.Return(Buffer);
            }

            toStringCache = null;
            Buffer = null;
        }

        public override string ToString() =>
            toStringCache ?? (toStringCache = Buffer.ToStringTerminated());
    }
}
