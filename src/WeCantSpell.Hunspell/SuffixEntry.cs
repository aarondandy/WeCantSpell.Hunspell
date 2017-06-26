using WeCantSpell.Hunspell.Infrastructure;

#if !NO_INLINE
using System.Runtime.CompilerServices;
#endif

namespace WeCantSpell.Hunspell
{
    public sealed class SuffixEntry : AffixEntry
    {
#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public SuffixEntry(
            string strip,
            string affixText,
            CharacterConditionGroup conditions,
            MorphSet morph,
            FlagSet contClass)
            : base(strip, affixText, conditions, morph, contClass)
        {
        }

        public sealed override string Append
        {
#if !NO_INLINE
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
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
