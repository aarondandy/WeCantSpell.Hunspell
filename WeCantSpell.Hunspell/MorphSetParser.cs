using System;

namespace WeCantSpell.Hunspell;

internal readonly struct MorphSetParser
{
    public MorphSetParser(AffixConfig affix)
        : this(complexPrefixes: affix.ComplexPrefixes)
    {
    }

    public MorphSetParser(bool complexPrefixes)
    {
        ComplexPrefixes = complexPrefixes;

        if (complexPrefixes)
        {
            ParseMorphSet = ParseMorphSetReversed;
        }
        else
        {
            ParseMorphSet = ParseMorphSetNormal;
        }
    }

    public readonly ParseMorphSetDelegate ParseMorphSet;
    public readonly bool ComplexPrefixes;

    private readonly MorphSet ParseMorphSetNormal(ReadOnlySpan<char> text)
    {
        var index = text.IndexOfTabOrSpace();
        if (index < 0)
        {
            return MorphSet.CreateSingle(text);
        }

        var morphsBuilder = ArrayBuilder<string>.Pool.Get(2);

        morphsBuilder.Add(text.Slice(0, index).ToString());

        foreach (var morph in text.Slice(index + 1).SplitOnTabOrSpace())
        {
            morphsBuilder.Add(morph.ToString());
        }

        return MorphSet.CreateUsingArray(ArrayBuilder<string>.Pool.ExtractAndReturn(morphsBuilder));
    }

    private readonly MorphSet ParseMorphSetReversed(ReadOnlySpan<char> text)
    {
        var index = text.IndexOfTabOrSpace();
        if (index < 0)
        {
            return MorphSet.CreateSingle(text.ToStringReversed());
        }

        var morphsBuilder = ArrayBuilder<string>.Pool.Get(2);

        morphsBuilder.Add(text.Slice(0, index).ToStringReversed());

        foreach (var morph in text.Slice(index + 1).SplitOnTabOrSpace())
        {
            morphsBuilder.Add(morph.ToStringReversed());
        }

        morphsBuilder.Reverse();

        return MorphSet.CreateUsingArray(ArrayBuilder<string>.Pool.ExtractAndReturn(morphsBuilder));
    }
}
