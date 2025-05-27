using Shin_Megami_Tensei_View;
using Shin_Megami_Tensei.Configs;

namespace Shin_Megami_Tensei;

public class MenusController
{
    private ImplementedConsoleView _view;
    
    public MenusController(ImplementedConsoleView view)
    {
        _view = view;
    }

    public UnitData SelectTarget(List<UnitData> oponentUnits, UnitData unitDataAttacking)
    {
        return _view.SelectTarget(oponentUnits, unitDataAttacking);
    }
    

    public void ShowEffectOfDamage(UnitData unitDataAttacking, UnitData target, int damageWithAffinities)
    {

        _view.ShowEffectOfDamage(unitDataAttacking, target, damageWithAffinities);
    }

    public void ShowGunTarget(UnitData unitDataAttacking, UnitData target)
    {

        _view.ShowGunTarget(unitDataAttacking, target);
    }

    public void ShowAttackTarget(UnitData unitDataAttacking, UnitData target)
    {
        _view.ShowAttackTarget(unitDataAttacking, target);
    }
    
    public void ShowFireTarget(UnitData unitDataAttacking, UnitData target)
    {
        _view.ShowFireTarget(unitDataAttacking, target);
    }
    
    public void ShowIceTarget(UnitData unitDataAttacking, UnitData target)
    {
        _view.ShowIceTarget(unitDataAttacking, target);
    }
    
    public void ShowElecTarget(UnitData unitDataAttacking, UnitData target)
    {
        _view.ShowElecTarget(unitDataAttacking, target);
    }
    
    public void ShowForceTarget(UnitData unitDataAttacking, UnitData target)
    {
        _view.ShowForceTarget(unitDataAttacking, target);
    }
    
    public void ShowHealAllyTarget(UnitData unitDataAttacking, UnitData target)
    {
        _view.ShowHealAllyTarget(unitDataAttacking, target);
    }

    public void ShowReviveAllyTarget(UnitData unitDataAttacking, UnitData target)
    {
        _view.ShowReviveAllyTarget(unitDataAttacking, target);
    }

    public void ShowHealResult(int heal, UnitData target)
    {
        _view.ShowHealResult(heal, target);
    }

    public UnitData GetAllyTarget(UnitData unitDataAttacking, TeamData teamData)
    {
        TeamController teamController = teamData.teamController;
        List<UnitData> fullTeam = teamData.team;
       List<UnitData> activeUnitsAlive= teamController.GetActiveUnitsAlive(fullTeam);
       
       return _view.GetAllyTarget(unitDataAttacking, activeUnitsAlive);

    }

    public UnitData GetDeadAllyTarget(UnitData unitDataAttacking, TeamData teamData)
    {
        TeamController teamController = teamData.teamController;
        List<UnitData> fullTeam = teamData.team;
        List<UnitData> deadUnits= teamController.GetDeadUnits(fullTeam);
        return _view.GetDeadAllyTarget(unitDataAttacking, deadUnits);
        
    }
    

    public void AnounceThatAMonsterHasBeenSummon(UnitData monsterToSummon)
    {
        _view.AnounceThatAMonsterHasBeenSummon(monsterToSummon);
    }
    
    
    public UnitData GetMonsterToGetOut( TeamData teamData, TeamController teamController)
    {
        
        List<UnitData> activeUnits = teamController.GetActiveUnits(teamData.team);

        return _view.GetMonsterToGetOut(activeUnits);

    }
    
    

    
    public void AnounceUnitsOrderOfAction(TeamData teamData, TeamController teamController)
    {
        List<UnitData> activeUnits = teamController.GetActiveUnitsAlive(teamData.team);
        List<UnitData> activeUnitsInOrder = teamController.GetActiveUnitsInOrderOfAction(activeUnits, teamData.teamUnitsThatAlreadyPlayed);
        _view.AnounceUnitsOrderOfAction(activeUnitsInOrder);

    }

    
    
    public void AnounceRevive( UnitData attackingUnitData, UnitData allyUnitData)
    {
        _view.AnounceRevive(attackingUnitData, allyUnitData);
    }

}