namespace Hunspell
{
    public class ReplacementEntry
    {
        public enum Type : int
        {
            Med = 0,
            Ini = 1,
            Fin = 2,
            Isol = 3
        }

        public ReplacementEntry(string pattern)
        {
            Pattern = pattern;
        }

        public string Pattern { get; }

        /// <summary>
        /// Med, ini, fin, isol .
        /// </summary>
        public string[] OutStrings { get; } = new string[4];

        public string Med
        {
            get { return OutStrings[(int)Type.Med]; }
            set { OutStrings[(int)Type.Med] = value; }
        }

        public string Ini
        {
            get { return OutStrings[(int)Type.Ini]; }
            set { OutStrings[(int)Type.Ini] = value; }
        }

        public string Fin
        {
            get { return OutStrings[(int)Type.Fin]; }
            set { OutStrings[(int)Type.Fin] = value; }
        }

        public string Isol
        {
            get { return OutStrings[(int)Type.Isol]; }
            set { OutStrings[(int)Type.Isol] = value; }
        }
    }
}
