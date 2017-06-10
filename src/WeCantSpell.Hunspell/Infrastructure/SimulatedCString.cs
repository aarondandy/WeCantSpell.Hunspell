using System.Text;

#if !NO_INLINE
using System.Runtime.CompilerServices;
#endif

namespace WeCantSpell.Hunspell.Infrastructure
{
    internal sealed class SimulatedCString
    {
        public SimulatedCString(string text) =>
            Buffer = StringBuilderPool.Get(text);

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
            Buffer.WriteChars(text, destinationIndex);
        }

        public void WriteChars(int sourceIndex, string text, int destinationIndex)
        {
            toStringCache = null;
            Buffer.WriteChars(sourceIndex, text, destinationIndex);
        }

        public void Assign(string text)
        {
            toStringCache = null;
            Buffer.Clear();
            Buffer.Append(text);
        }

        public string Substring(int startIndex) =>
            ToString().Substring(startIndex);

        public string Substring(int startIndex, int length) =>
            ToString().Substring(startIndex, length);

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        internal StringSlice Subslice(int startIndex) =>
            ToString().Subslice(startIndex);

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        internal StringSlice Subslice(int startIndex, int length) =>
            ToString().Subslice(startIndex, length);

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
