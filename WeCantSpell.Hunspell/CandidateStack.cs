using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace WeCantSpell.Hunspell;

[DebuggerDisplay("Count = {Count}")]
internal struct CandidateStack
{
    internal const int MaxCandidateStackDepth = 2048;

    public static void Push(ref CandidateStack stack, string value)
    {
        if (stack._s0 is null)
        {
            stack._s0 = value;
        }
        else
        {
            (stack._rest ??= []).Add(value);
        }
    }

    public static void Pop(ref CandidateStack stack)
    {
        if (stack._rest is { Count: > 0 })
        {
            pop(stack._rest);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            static void pop(List<string> items) => items.RemoveAt(items.Count - 1);
        }
        else
        {
            stack._s0 = null;
        }
    }

    private string? _s0;
    private List<string>? _rest;

    public CandidateStack()
    {
    }

    /// <remarks>
    /// apply a fairly arbitrary depth limit
    /// </remarks>
    public readonly bool ExceedsArbitraryDepthLimit => Count > MaxCandidateStackDepth;

    public readonly int Count => _s0 is null
        ? 0
        : (_rest is null ? 1 : _rest.Count + 1);

    public readonly bool Contains(string value)
    {
        return
            _s0 is not null
            &&
            (
                _s0.Equals(value)
                ||
                (_rest is not null && _rest.Contains(value))
            );
    }

    public readonly bool Contains(ReadOnlySpan<char> value)
    {
        return
            _s0 is not null
            &&
            (
                _s0.AsSpan().SequenceEqual(value)
                ||
                (_rest is not null && _rest.Contains(value))
            );
    }

}
