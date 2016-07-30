namespace Hunspell
{
    public class PatternEntry
    {
        public PatternEntry(string pattern, string pattern2, string pattern3, int condition, int condition2)
        {
            Pattern = pattern;
            Pattern2 = pattern2;
            Pattern3 = pattern3;
            Condition = condition;
            Condition2 = condition2;
        }

        public string Pattern { get; }

        public string Pattern2 { get; }

        public string Pattern3 { get; }

        public int Condition { get; }

        public int Condition2 { get; }
    }
}
