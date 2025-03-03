using System;

namespace WeCantSpell.Hunspell;

internal delegate bool TryParseFlagValueDelegate(ReadOnlySpan<char> text, out FlagValue value);

internal delegate FlagSet ParseFlagSetDelegate(ReadOnlySpan<char> text);

internal delegate FlagValue[] ParseFlagValuesDelegate(ReadOnlySpan<char> text);

internal delegate MorphSet ParseMorphSetDelegate(ReadOnlySpan<char> text);
