using System.Text;

namespace Hunspell.Infrastructure
{
    internal sealed class SimulatedCString
    {
        public SimulatedCString()
        {
            Buffer = StringBuilderPool.Get();
        }

        public SimulatedCString(string text)
        {
            Buffer = StringBuilderPool.Get(text);
        }

        private StringBuilder Buffer;
        private string toStringCache = null;

        public char this[int index]
        {
            get
            {
                return index < 0 || index >= Buffer.Length
                    ? '\0'
                    : Buffer[index];
            }
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

        public string Substring(int index)
        {
            return ToString().Substring(index);
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

        public override string ToString()
        {
            return toStringCache ?? (toStringCache = Buffer.ToStringTerminated());
        }

        public static implicit operator string(SimulatedCString cString)
        {
            return cString?.ToString();
        }
    }
}
