#if !NO_INLINE
using System.Runtime.CompilerServices;
#endif

namespace WeCantSpell.Hunspell
{
    public sealed class PrefixEntry : AffixEntry
    {
#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public PrefixEntry(
            string strip,
            string affixText,
            CharacterConditionGroup conditions,
            MorphSet morph,
            FlagSet contClass)
            : base(strip, affixText, conditions, morph, contClass)
        {
        }

        public sealed override string Key
        {
#if !NO_INLINE
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            get => Append;
        }
    }
}
