using System;

namespace Kombatant.Enums
{
    /// <summary>
    /// Enum for differentiating a type of job (i.e. melee dps, ranged dps, healer or tank)
    /// </summary>
    [Flags]
    public enum ClassJobEffectType : uint
    {
        None      = 0,
        MeleeDps  = 1 << 0,
        RangedDps = 1 << 1,
        MagicDps  = 1 << 2,
        Tank      = 1 << 3,
        Healer    = 1 << 4,
        Dps       = MeleeDps | RangedDps | MagicDps,
        Melee     = MeleeDps | Tank,
        Ranged    = RangedDps | MagicDps | Healer,
        Magic     = MagicDps | Healer,
        All       = UInt32.MaxValue
    }
}