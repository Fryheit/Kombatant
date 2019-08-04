using System.ComponentModel;

namespace Kombatant.Enums
{
    /// <summary>
    /// Enum for the automatic target selection modes.
    /// </summary>
    public enum TargetingMode
    {
        [Description("No auto target selection")]
        None = 0,
        [Description("Nearest enemy")]
        Nearest = 10,
        [Description("Assist party leader")]
        AssistLeader = 20,
        [Description("Assist tank")]
        AssistTank = 30,
        [Description("Highest level character")]
        AssistHighestLvl = 40,
        [Description("Best AoE target")]
        BestAoE = 50,
        [Description("Whitelisted enemies")]
        OnlyWhitelisted = 60,
        [Description("Assist fixed character")]
        AssistFixedCharacter = 70,
        [Description("Lowest health")]
        LowestHealth = 100,
        [Description("Lowest health percent")]
        LowestHealthPercent = 110,
        [Description("Highest health")]
        HighestHealth = 120,
        [Description("Highest health percent")]
        HighestHealthPercent = 130,
    }
}