using System.ComponentModel;

namespace Kombatant.Enums
{
    /// <summary>
    /// Enum for the different follow modes.
    /// </summary>
    public enum FollowMode
    {
        [Description("Do not follow")]
        None = 0,
        [Description("Follow targeted character")]
        TargetedCharacter = 10,
        [Description("Follow specified character")]
        FixedCharacter = 20,
        [Description("Follow party leader")]
        PartyLeader = 30,
        [Description("Follow party tank")]
        Tank = 40
    }
}