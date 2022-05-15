using System;

using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell;

public partial class AffixReader
{
    private ref struct AffixParametersParser
    {
        public AffixParametersParser(ReadOnlySpan<char> text)
        {
            _text = text;
        }

        private ReadOnlySpan<char> _text;

        public bool TryParseNextAffixFlag(FlagParser flagParser, out FlagValue flag)
        {
            var flagSpan = ParseNextArgument();

            if (flagSpan.IsEmpty || !flagParser.TryParseFlag(flagSpan, out flag))
            {
                flag = default;
                return false;
            }

            return true;
        }

        public ReadOnlySpan<char> ParseNextArgument()
        {
            AdvanceThroughWhiteSpace();

            var resultSpan = ReadGroup();
            _text = _text.Slice(resultSpan.Length);

            return resultSpan;
        }

        public ReadOnlySpan<char> ParseFinalArguments()
        {
            AdvanceThroughWhiteSpace();

            var remainder = _text;

            var commentIndex = LocateComments(remainder);
            if (commentIndex >= 0)
            {
                remainder = remainder.Slice(0, commentIndex);
            }

            return remainder;
        }

        private static int LocateComments(ReadOnlySpan<char> span)
        {
            var i = 0;
            while (i >= 0)
            {
                i = span.IndexOf('#', i);
                if (i < 0)
                {
                    break;
                }
                else if (i == 0)
                {
                    return 0;
                }
                else if (i > 0)
                {
                    if (span[i - 1].IsTabOrSpace())
                    {
                        return i;
                    }
                }
            }

            return -1;
        }

        private void AdvanceThroughWhiteSpace()
        {
            var i = 0;
            for (; i < _text.Length && _text[i].IsTabOrSpace(); i++) ;

            if (i > 0)
            {
                _text = _text.Slice(i);
            }
        }

        private ReadOnlySpan<char> ReadGroup()
        {
            if (_text.IsEmpty)
            {
                return ReadOnlySpan<char>.Empty;
            }

            var i = _text.IndexOfTabOrSpace();
            return i < 0 ? _text : _text.Slice(0, i);
        }
    }
}
