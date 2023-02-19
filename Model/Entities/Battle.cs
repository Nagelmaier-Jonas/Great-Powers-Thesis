using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Model.Entities.Regions;
using Model.Entities.Units;
using Model.Entities.Units.Abstract;

namespace Model.Entities;

[Table("BATTLES")]
public class Battle{
    [Key]
    [Column("BATTLE_ID")][DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id{ get; set; }

    [Column("CURRENT_NATION_ID")] public int CurrentNationId{ get; set; }
    public Nation CurrentNation{ get; set; }

    [Column("LOCATION_ID")] public int LocationId{ get; set; }
    public ARegion Location{ get; set; }

    [Column("BATTLE_PHASE", TypeName = "VARCHAR(45)")]
    public EBattlePhase Phase{ get; set; }

    [Column("BATTLE_ROUND")] public int Round{ get; set; } = 1;

    [Column("ATTACKER_DECIDED", TypeName = "TINYINT")]
    public bool AttackerDecided{ get; set; } = false;

    public List<AUnit> Attackers{ get; set; } = new List<AUnit>();
    public List<AUnit> Defenders{ get; set; } = new List<AUnit>();
    public List<AUnit> Casualties{ get; set; } = new List<AUnit>();

    public int NonAirHits{ get; set; }
    public int NonSubmarineHits{ get; set; }
    public int NormalHits{ get; set; }

    public int AttackingInfantryRolls{ get; set; }

    public int DefendingInfantryRolls{ get; set; }

    [Column("IS_DECIDED", TypeName = "TINYINT")]
    public bool IsDecided{ get; set; } = false;

    [NotMapped]
    public Dictionary<int, int> DiceRolls{ get; set; } = new(){
        { 1, 0 },
        { 2, 0 },
        { 3, 0 },
        { 4, 0 },
        { 5, 0 },
        { 6, 0 }
    };

    public int GetInfantryRolls(Nation nation){
        if (IsAttacker(nation)) return Attackers.Any(u => u.IsArtillery()) ? 2 : 1;
        if (!IsAttacker(nation)) return Defenders.Any(u => u.IsArtillery()) ? 3 : 2;
        return 1;
    }

    private bool CheckForDestroyers(AUnit submarine){
        if (!submarine.IsSubmarine()) return true;
        if (Attackers.Contains(submarine)) return Defenders.Any(u => u.IsDestroyer());
        return Defenders.Contains(submarine) && Attackers.Any(u => u.IsDestroyer());
    }

    private List<AUnit> GetCurrentNationsUnits() => IsAttacker(CurrentNation) ? Attackers : Defenders.Where(u => u.NationId == CurrentNationId).ToList();

    private List<AUnit> GetCurrentNationsEnemies() => IsAttacker(CurrentNation) ? Defenders : Attackers;

    private void HitUnit(AUnit unit){
        if (!CheckForOpenHits()) return;

        bool hit = false;
        if (!unit.IsPlane() && NonAirHits > 0){
            hit = true;
            NonAirHits -= 1;
        }

        if (!unit.IsSubmarine() && NonSubmarineHits > 0){
            hit = true;
            NonSubmarineHits -= 1;
        }

        if (!hit) NormalHits -= 1;

        unit.HitPoints -= 1;
        if (Phase != EBattlePhase.SPECIAL_SUBMARINE) return;
        if (unit.HitPoints != 0) return;
        Attackers.Remove(unit);
        Defenders.Remove(unit);
    }

    private List<AUnit> GetTargetsForHits() => GetCurrentNationsEnemies().Where(u => u.HitPoints > 0).ToList();

    private bool CheckForOpenHits(){
        if (GetTargetsForHits().Count == 0) return false;
        if (NormalHits > 0) return true;
        if (GetCurrentNationsEnemies().Any(u => !u.IsPlane()) && NonAirHits > 0) return true;
        return GetCurrentNationsEnemies().Any(u => !u.IsSubmarine()) && NonSubmarineHits > 0;
    }

    public bool IsAttacker(Nation nation) => Attackers.Any(u => u.Nation.Id == nation.Id);

    private void RollForHits(){
        bool attacker = IsAttacker(CurrentNation);
        List<AUnit> units = Phase == EBattlePhase.SPECIAL_SUBMARINE ? GetCurrentNationsUnits().Where(u => u.IsSubmarine()).ToList() : GetCurrentNationsEnemies();
        foreach (var unit in units){
            int roll = Dice.Roll();
            DiceRolls[roll] += 1;
            if (unit.IsPlane()){
                if (attacker){
                    if (roll <= unit.Attack) NonSubmarineHits += 1;
                    continue;
                }

                if (roll <= unit.Defense) NonSubmarineHits += 1;
                continue;
            }

            if (unit.IsSubmarine()){
                if (CheckForDestroyers(unit) && Phase == EBattlePhase.SPECIAL_SUBMARINE) continue;

                if (attacker){
                    if (roll <= unit.Attack) NonAirHits += 1;
                    continue;
                }

                if (roll <= unit.Defense) NonAirHits += 1;
                continue;
            }

            if (attacker){
                if (unit.IsInfantry()){
                    if (roll <= AttackingInfantryRolls) NormalHits += 1;
                    continue;
                }

                if (roll <= unit.Attack) NormalHits += 1;
                continue;
            }

            if (unit.IsInfantry()){
                if (roll <= DefendingInfantryRolls) NormalHits += 1;
                continue;
            }

            if (roll <= unit.Defense) NormalHits += 1;
        }
    }

    private bool CheckForDefenselessTransports(){
        if (!Attackers.All(att => Defenders.All(def => !def.CanAttack(att)))) return false;
        Defenders.RemoveAll(u => u.IsTransport());
        return true;
    }

    public void ResolveCasualties(){
        Casualties.AddRange(Attackers.Where(u => u.HitPoints == 0));
        Casualties.AddRange(Defenders.Where(u => u.HitPoints == 0));
        
        Attackers.RemoveAll(u => u.HitPoints == 0);
        Defenders.RemoveAll(u => u.HitPoints == 0);
    }

    private bool AreSubmarinesInvolved() => Attackers.Any(u => u.IsSubmarine()) || Defenders.Any(u => u.IsSubmarine());

    public List<Nation> GetDefendingNations() =>
        (from u in Defenders.DistinctBy(u => u.Nation) select u.Nation).ToList();

    public Nation GetAttacker() => Attackers.FirstOrDefault().Nation;

    private Nation GetNextNation(){
        List<Nation> defenders = GetDefendingNations();
        defenders.Sort();
        if (IsAttacker(CurrentNation)) return defenders.FirstOrDefault();
        int index = defenders.IndexOf(CurrentNation);
        return index == defenders.Count - 1 ? GetAttacker() : defenders.ElementAt(index + 1);
    }

    public bool AdvanceCombat(){
        switch (Phase){
            case EBattlePhase.SPECIAL_SUBMARINE:
                if (!AreSubmarinesInvolved()){
                    Phase = EBattlePhase.ATTACK;
                    return true;
                }
                Phase = EBattlePhase.ATTACK;
                return true;
            case EBattlePhase.ATTACK:
                if (CheckForDefenselessTransports()){
                    Phase = EBattlePhase.RESOLUTION;
                    return true;
                }
                RollForHits();
                Phase = EBattlePhase.DEFENSE;
                return true;
            case EBattlePhase.DEFENSE:
                if (CheckForOpenHits()) return false;
                DiceRolls = new Dictionary<int, int>{
                    { 1, 0 },
                    { 2, 0 },
                    { 3, 0 },
                    { 4, 0 },
                    { 5, 0 },
                    { 6, 0 }
                };
                NormalHits = 0;
                NonAirHits = 0;
                NonSubmarineHits = 0;
                CurrentNation = GetNextNation();
                CurrentNationId = CurrentNation.Id;
                Phase = CurrentNation.Id == GetAttacker().Id ? EBattlePhase.RESOLUTION : EBattlePhase.ATTACK;
                return true;
            case EBattlePhase.RESOLUTION:
                if (!AttackerDecided) return false;
                ResolveCasualties();
                if (CheckForWinner()){
                    IsDecided = true;
                    return true;
                }
                AttackingInfantryRolls = GetInfantryRolls(GetAttacker());
                DefendingInfantryRolls = GetInfantryRolls(GetDefendingNations().FirstOrDefault());
                Round += 1;
                Phase = IsAquaticBattle() ? EBattlePhase.SPECIAL_SUBMARINE : EBattlePhase.ATTACK;
                AttackerDecided = false;
                return true;
        }
        return true;
    }

    public bool Submerge(AUnit submarine){
        if (CheckForDestroyers(submarine)) return false;
        if (!GetCurrentNationsUnits().Contains(submarine)) return false;
        if (Phase != EBattlePhase.SPECIAL_SUBMARINE) return false;
        Attackers.Remove(submarine);
        Defenders.Remove(submarine);
        return true;
    }

    public bool PlaceHit(AUnit unit){
        if (!CheckForOpenHits()) return false;
        if (GetCurrentNationsEnemies().All(u => u.Id != unit.Id)) return false;
        HitUnit(unit);
        return true;
    }

    public bool AttackerRetreats(){
        if (Phase != EBattlePhase.RESOLUTION) return false;
        AttackerDecided = true;
        //List<AUnit> retreatingUnits = Attackers.Where(unit => unit.GetPossibleRetreatTargets((from u in Attackers select u.GetPreviousLocation()).ToList()).Count > 0).ToList();
        return true;
    }

    public bool AttackerContinues(){
        if (Phase != EBattlePhase.RESOLUTION) return false;
        AttackerDecided = true;
        return true;
    }

    public bool CheckForWinner() => Attackers.Count == 0 || Defenders.Count == 0;

    public bool IsAquaticBattle() => Location.IsWaterRegion();
}