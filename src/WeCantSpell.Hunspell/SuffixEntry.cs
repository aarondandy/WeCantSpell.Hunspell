using WeCantSpell.Hunspell.Infrastructure;

#if !NO_INLINE
using System.Runtime.CompilerServices;
#endif

namespace WeCantSpell.Hunspell
{
    public sealed class SuffixEntry : AffixEntry
    {
        public sealed override string Append
        {
            get => base.Append;
            protected set
            {
                base.Append = value;
                RAppend = value.Reverse();
            }
        }

        public string RAppend { get; private set; }

        public sealed override string Key
        {
#if !NO_INLINE
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            get => RAppend;
        }
    }
}
