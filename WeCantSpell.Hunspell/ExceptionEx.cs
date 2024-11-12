using System;
using System.Runtime.CompilerServices;

namespace WeCantSpell.Hunspell;

internal static class ExceptionEx
{

#if !NO_EXPOSED_NULLANNOTATIONS
    [System.Diagnostics.CodeAnalysis.DoesNotReturn]
#endif
    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static void ThrowNotSupported() => throw new NotSupportedException();

#if !NO_EXPOSED_NULLANNOTATIONS
    [System.Diagnostics.CodeAnalysis.DoesNotReturn]
#endif
    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static T ThrowNotSupported<T>() => throw new NotSupportedException();

#if !NO_EXPOSED_NULLANNOTATIONS
    [System.Diagnostics.CodeAnalysis.DoesNotReturn]
#endif
    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static void ThrowNotImplementedYet() => throw new NotImplementedException();

#if !NO_EXPOSED_NULLANNOTATIONS
    [System.Diagnostics.CodeAnalysis.DoesNotReturn]
#endif
    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static T ThrowNotImplementedYet<T>() => throw new NotImplementedException();

#if !NO_EXPOSED_NULLANNOTATIONS
    [System.Diagnostics.CodeAnalysis.DoesNotReturn]
#endif
    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static void ThrowInvalidOperation(string message) => throw new InvalidOperationException(message);

#if !HAS_THROWOOR || DEBUG

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfArgumentEmpty(ReadOnlySpan<char> value, string paramName)
    {
        if (value.IsEmpty) ThrowArgumentOutOfRange(paramName);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfArgumentNull<T>(T value, string paramName) where T : class
    {
#if HAS_THROWNULL
        ArgumentNullException.ThrowIfNull(value, paramName);
#else
        if (value is null) ThrowArgumentNull(paramName);
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfArgumentEqual<T>(T value, T other, string paramName) where T : IEquatable<T>
    {
#if HAS_THROWOOR
        ArgumentOutOfRangeException.ThrowIfEqual(value, other, paramName);
#else
        if (other is null ? value is null : other.Equals(value)) ThrowArgumentOutOfRange(paramName);
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfArgumentNotEqual<T>(T value, T other, string paramName) where T : IEquatable<T>
    {
#if HAS_THROWOOR
        ArgumentOutOfRangeException.ThrowIfNotEqual(value, other, paramName);
#else
        if (other is null ? value is not null : !other.Equals(value)) ThrowArgumentOutOfRange(paramName);
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfArgumentLessThan<T>(T value, T other, string paramName) where T : IComparable<T>
    {
#if HAS_THROWOOR
        ArgumentOutOfRangeException.ThrowIfLessThan(value, other, paramName);
#else
        if (value.CompareTo(other) < 0) ThrowArgumentOutOfRange(paramName);
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfArgumentLessThanOrEqual<T>(T value, T other, string paramName) where T : IComparable<T>
    {
#if HAS_THROWOOR
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(value, other, paramName);
#else
        if (value.CompareTo(other) <= 0) ThrowArgumentOutOfRange(paramName);
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfArgumentGreaterThan<T>(T value, T other, string paramName) where T : IComparable<T>
    {
#if HAS_THROWOOR
        ArgumentOutOfRangeException.ThrowIfGreaterThan(value, other, paramName);
#else
        if (value.CompareTo(other) > 0) ThrowArgumentOutOfRange(paramName);
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfArgumentGreaterThanOrEqual<T>(T value, T other, string paramName) where T : IComparable<T>
    {
#if HAS_THROWOOR
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(value, other, paramName);
#else
        if (value.CompareTo(other) >= 0) ThrowArgumentOutOfRange(paramName);
#endif
    }

#if !NO_EXPOSED_NULLANNOTATIONS
    [System.Diagnostics.CodeAnalysis.DoesNotReturn]
#endif
    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static void ThrowArgumentNull(string paramName) => throw new ArgumentNullException(paramName);

#if !NO_EXPOSED_NULLANNOTATIONS
    [System.Diagnostics.CodeAnalysis.DoesNotReturn]
#endif
    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static void ThrowArgumentOutOfRange(string paramName) => throw new ArgumentOutOfRangeException(paramName);

#if !NO_EXPOSED_NULLANNOTATIONS
    [System.Diagnostics.CodeAnalysis.DoesNotReturn]
#endif
    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static void ThrowArgumentOutOfRange(string paramName, string message) => throw new ArgumentOutOfRangeException(paramName, message);

#if !NO_EXPOSED_NULLANNOTATIONS
    [System.Diagnostics.CodeAnalysis.DoesNotReturn]
#endif
    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static void ThrowInvalidOperation() => throw new InvalidOperationException();

#endif

}
