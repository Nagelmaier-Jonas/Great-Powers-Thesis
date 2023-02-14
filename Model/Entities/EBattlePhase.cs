using System.ComponentModel;

namespace Model.Entities;

public enum EBattlePhase{
    [Description("Spezial")] SPECIAL_SUBMARINE,
    [Description("Angriff")] ATTACK,
    [Description("Verteidigung")] DEFENSE,
    [Description("Auswertung")] RESOLUTION
}