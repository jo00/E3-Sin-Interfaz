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


}