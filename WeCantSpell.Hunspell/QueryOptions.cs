using System;

namespace WeCantSpell.Hunspell;

public sealed class QueryOptions
{
    /// <summary>
    /// The maximum depth of ß sharps to check.
    /// </summary>
    /// <remarks>
    /// The actual results returned may or may not actually be limited by this value.
    /// </remarks>
    public int MaxSharps { get; set; } = 5;

    /// <summary>
    /// The maximum number of compound suggestions to suggest.
    /// </summary>
    /// <remarks>
    /// The actual results returned may or may not actually be limited by this value.
    /// </remarks>
    public int MaxCompoundSuggestions { get; set; } = 3;

    /// <summary>
    /// The maximum number of suggestions to produce.
    /// </summary>
    /// <remarks>
    /// The actual results returned may or may not actually be limited by this value.
    /// </remarks>
    public int MaxSuggestions { get; set; } = 15;

    /// <remarks>
    /// The actual results returned may or may not actually be limited by this value.
    /// </remarks>
    public int MaxRoots { get; set; } = 100;

    /// <remarks>
    /// The actual results returned may or may not actually be limited by this value.
    /// </remarks>
    public int MaxWords { get; set; } = 100;

    /// <remarks>
    /// The actual results returned may or may not actually be limited by this value.
    /// </remarks>
    public int MaxGuess { get; set; } = 200;

    /// <remarks>
    /// The actual results returned may or may not actually be limited by this value.
    /// </remarks>
    public int MaxPhoneticSuggestions { get; set; } = 2;

    /// <remarks>
    /// The actual results returned may or may not actually be limited by this value.
    /// </remarks>
    public int MaxCharDistance { get; set; } = 4;

    /// <summary>
    /// This is the number of checks to be performed before considering time based cancellation.
    /// </summary>
    /// <remarks>
    /// I think the purpose of this is to ensure a minimum number of results in slower environments.
    /// </remarks>
    public int MinTimer { get; set; } = 100;

    /// <summary>
    /// The time limit for some long running steps during suggestion generation.
    /// </summary>
    /// <remarks>
    /// Timelimit: max ~1/4 sec (process time on Linux) for a time consuming function.
    /// </remarks>
    public TimeSpan TimeLimitSuggestStep { get; set; } = TimeSpan.FromMilliseconds(250);

    /// <summary>
    /// The time limit for each compound suggestion iteration.
    /// </summary>
    public TimeSpan TimeLimitCompoundSuggest { get; set; } = TimeSpan.FromMilliseconds(100);

    /// <summary>
    /// The time limit for each compound word check operation.
    /// </summary>
    public TimeSpan TimeLimitCompoundCheck { get; set; } = TimeSpan.FromMilliseconds(50);

    /// <summary>
    /// A somewhat overall time limit for the suggestion algorithm.
    /// </summary>
    public TimeSpan TimeLimitSuggestGlobal { get; set; } = TimeSpan.FromMilliseconds(250);
}
