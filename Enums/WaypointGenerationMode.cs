using System.ComponentModel;

namespace Kombatant.Enums
{
    public enum WaypointGenerationMode
    {
        [Description("None")]
        None = 0,
        [Description("Use NavGraph")]
        NavGraph = 10,
        [Description("Use Offmesh navigation")]
        Offmesh = 20
    }
}