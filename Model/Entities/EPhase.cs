using System.ComponentModel;
using System.Reflection;

namespace Model.Entities;

public enum EPhase{
    [Description("Kaufphase")] PurchaseUnits,
    [Description("Kampfbewegung")] CombatMove,
    [Description("Kampf")] ConductCombat,
    [Description("Bewegung")] NonCombatMove,
    [Description("Mobilisierung")] MobilizeNewUnits,
    [Description("Zusammenfassung")] CollectIncome
}