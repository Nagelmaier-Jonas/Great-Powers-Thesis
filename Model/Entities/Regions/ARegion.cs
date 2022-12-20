using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Model.Entities.Units;

namespace Model.Entities.Regions;

[Table("REGIONS_BT")]
public abstract class ARegion{
    [Column("ID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id{ get; set; }

    [Column("NAME", TypeName = "VARCHAR(45)")]
    public string? Name{ get; set; }

    public List<Neighbours>? Neighbours{ get; set; }

    [Column("REGION_TYPE", TypeName = "VARCHAR(45)")]
    public ERegionType? Type{ get; set; }

    public List<Plane> StationedPlanes{ get; set; } = new List<Plane>();
    public List<Plane>? IncomingPlanes{ get; set; }

    #region Neighbours

    private List<ARegion> GetNeighbours(int distance, bool excludeSource = true, bool filterType = false,
        ERegionType type = ERegionType.LAND){
        if (distance <= 0) return new List<ARegion>();
        HashSet<ARegion> regions = new HashSet<ARegion>();
        foreach (var neighbour in Neighbours){
            if (neighbour.Neighbour.Type == type || !filterType){
                regions.Add(neighbour.Neighbour);
                regions.UnionWith(neighbour.Neighbour.GetNeighbours(distance - 1, false, filterType, type));
            }
        }

        if (excludeSource) regions.Remove(this);
        return regions.ToList();
    }

    public List<ARegion> GetAllNeighbours(int distance) =>
        GetNeighbours(distance);

    public List<ARegion> GetAllNeighboursWithSource(int distance) =>
        GetNeighbours(distance, false);

    public List<ARegion> GetNeighboursByType(int distance, ERegionType type) =>
        GetNeighbours(distance, true, true, type);

    public List<ARegion> GetNeighboursByTypeWithSource(int distance, ERegionType type) =>
        GetNeighbours(distance, false, true, type);

    #endregion

    #region FriendlyNeighbours

    protected List<LandRegion> GetFriendlyNeighbours(int distance, Nation nation, bool excludeSource = true,
        bool filterType = false, ERegionType type = ERegionType.LAND){
        if (distance <= 0) return new List<LandRegion>();
        HashSet<LandRegion> regions = new HashSet<LandRegion>();
        foreach (var neighbour in Neighbours){
            if(neighbour.Neighbour.ContainsEnemies(nation)) continue;
            LandRegion? neigh = null;
            if (neighbour.Neighbour.Type == ERegionType.LAND) neigh = (LandRegion)neighbour.Neighbour;
            if (neighbour.Neighbour.Type == type || !filterType){
                if (neigh == null){
                    regions.UnionWith(neighbour.Neighbour.GetFriendlyNeighbours(distance - 1, nation, false, filterType, type));
                    continue;
                }
                if (neigh.Nation == nation || neigh.Nation.Allies.Any(a => a.Ally == nation)){
                    regions.UnionWith(
                        neighbour.Neighbour.GetFriendlyNeighbours(distance - 1, nation, false, filterType, type));
                    regions.Add(neigh);
                }
            }
        }

        if (excludeSource && Type == ERegionType.LAND) regions.Remove((LandRegion)this);
        return regions.ToList();
    }

    public List<LandRegion> GetAllFriendlyNeighbours(int distance, Nation nation) =>
        GetFriendlyNeighbours(distance, nation);

    public List<LandRegion> GetAllFriendlyNeighboursWithSource(int distance, Nation nation) =>
        GetFriendlyNeighbours(distance, nation, false);

    public List<LandRegion> GetFriendlyNeighboursByLand(int distance, Nation nation) =>
        GetFriendlyNeighbours(distance, nation, true, true);

    public List<LandRegion> GetFriendlyNeighboursByLandWithSource(int distance, Nation nation) =>
        GetFriendlyNeighbours(distance, nation, false, true);

    #endregion

    public virtual List<AUnit> GetStationedUnits() =>
        throw new NotImplementedException();

    public List<AUnit> GetStationedFriendlyUnits(Nation nation) => GetStationedUnits()
        .Where(u => u.Nation == nation || u.Nation.Allies.Any(a => a.Ally == nation)).ToList();

    public bool ContainsEnemies(Nation nation) => GetStationedUnits()
        .Any(u => u.Nation != nation && u.Nation.Allies.All(a => a.Ally != nation));
}