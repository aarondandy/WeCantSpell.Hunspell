using System;

#if !NO_INLINE
using System.Runtime.CompilerServices;
#endif

namespace WeCantSpell.Hunspell.Infrastructure;

ref struct SimulatedCString
{
    public SimulatedCString(string text)
    {
        _buffer = text.ToCharArray();
        _cachedSpan = _buffer.AsSpan();
        _cachedString = null;
        _cacheRequiresRefresh = true;
    }

    private char[] _buffer;
    private string _cachedString;
    private Span<char> _cachedSpan;
    private bool _cacheRequiresRefresh;

    public char this[int index]
    {
        get => index < 0 || index >= _buffer.Length ? '\0' : _buffer[index];
        set
        {
            ResetCache();
            _buffer[index] = value;
        }
    }

    public int BufferLength
    {
#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        get => _buffer.Length;
    }

    public void WriteChars(string text, int destinationIndex)
    {
        ResetCache();

        var neededLength = text.Length + destinationIndex;
        if (_buffer.Length < neededLength)
        {
            Array.Resize(ref _buffer, neededLength);
        }

        text.CopyTo(0, _buffer, destinationIndex, text.Length);
    }

    public void WriteChars(ReadOnlySpan<char> text, int destinationIndex)
    {
        ResetCache();

        var neededLength = text.Length + destinationIndex;
        if (_buffer.Length < neededLength)
        {
            Array.Resize(ref _buffer, neededLength);
        }

        text.CopyTo(_buffer.AsSpan(destinationIndex));
    }

    public void Assign(string text)
    {
#if DEBUG
        if (text is null) throw new ArgumentNullException(nameof(text));
        if (text.Length > _buffer.Length) throw new ArgumentOutOfRangeException(nameof(text));
#endif
        ResetCache();

        text.CopyTo(0, _buffer, 0, text.Length);

        if (text.Length < _buffer.Length)
        {
            Array.Clear(_buffer, text.Length, _buffer.Length - text.Length);
        }
    }

    public void Destroy()
    {
        ResetCache();
        _buffer = null;
    }

    public override string ToString() =>
        _cachedString ?? (_cachedString = GetTerminatedSpan().ToString());

    public Span<char> GetTerminatedSpan()
    {
        if (_cacheRequiresRefresh)
        {
            _cacheRequiresRefresh = false;
            _cachedSpan = _buffer.AsSpan(0, FindTerminatedLength());
        }

        return _cachedSpan;
    }

    private void ResetCache()
    {
        _cacheRequiresRefresh = true;
        _cachedString = null;
    }

#if !NO_INLINE
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
    private int FindTerminatedLength()
    {
        var length = Array.IndexOf(_buffer, '\0');
        return length < 0 ? _buffer.Length : length;
    }
}
