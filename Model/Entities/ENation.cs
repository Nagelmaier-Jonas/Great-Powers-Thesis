using System.ComponentModel;

namespace Model.Entities;

public enum ENation{
    [Description("Soviet Union")] SovietUnion,
    [Description("Deutsches Reich")] Germany,
    [Description("Vereinigtes Königreich")] UnitedKingdom,
    [Description("Japan")] Japan,
    [Description("Vereinigte Staaten")] UnitedStates,
    [Description("Neutral")] Neutral
}