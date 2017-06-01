using System.Text;

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

        public int BufferLength => Buffer.Length;

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

        public string Substring(int index) => ToString().Substring(index);

        internal StringSlice Subslice(int index) => ToString().Subslice(index);

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

        public static implicit operator string(SimulatedCString cString) =>
            cString?.ToString();
    }
}
