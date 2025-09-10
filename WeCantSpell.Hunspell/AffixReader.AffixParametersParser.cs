using System;

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
            return flagParser.TryParseFlag(flagSpan, out flag);
        }

        public ReadOnlySpan<char> ParseNextArgument()
        {
            ReadOnlySpan<char> resultSpan;
            AdvanceThroughWhiteSpace();

            if (_text.Length > 0)
            {
                var i = _text.IndexOfTabOrSpace();

                if (i < 0)
                {
                    resultSpan = _text;
                    _text = [];
                }
                else
                {
                    resultSpan = _text.Slice(0, i);
                    _text = _text.Slice(resultSpan.Length);
                }
            }
            else
            {
                resultSpan = [];
            }

            return resultSpan;
        }

        public ReadOnlySpan<char> ParseFinalArguments()
        {
            AdvanceThroughWhiteSpace();

            var remainder = _text;

            var commentIndex = locateComments(remainder);
            if (commentIndex >= 0)
            {
                remainder = remainder.Slice(0, commentIndex);
            }

            return remainder;

            static int locateComments(ReadOnlySpan<char> span)
            {
                var i = 0;
                while (i < span.Length)
                {
                    i = span.IndexOf('#', i);

                    if (i == 0)
                    {
                        return 0;
                    }
                    else if (i < 0)
                    {
                        goto fail;
                    }

                    if (span[i - 1].IsTabOrSpace())
                    {
                        return i;
                    }

                    i++;
                }

            fail:
                return -1;
            }
        }

        public override readonly string ToString() => _text.ToString();

        private void AdvanceThroughWhiteSpace()
        {
            var i = 0;
            for (; i < _text.Length && _text[i].IsTabOrSpace(); i++) ;

            if (i > 0)
            {
                _text = _text.Slice(i);
            }
        }
    }
}
