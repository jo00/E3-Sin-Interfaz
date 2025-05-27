using Shin_Megami_Tensei;
using Shin_Megami_Tensei_View;
using Shin_Megami_Tensei.Configs;

public class SummonController
{
    private readonly ImplementedConsoleView _view;
    private readonly TeamController _teamController;
    private readonly TurnsController _turnsController;
    private readonly MenusController _menusController;

    public SummonController(ImplementedConsoleView view, TeamController teamController, TurnsController turnsController,
        MenusController menusController)
    {
        _view = view;
        _teamController = teamController;
        _turnsController = turnsController;
        _menusController = menusController;
    }

    public void SummonForSamurai(UnitData samurai, TeamData teamData, Action markActionExecuted)
    {
        UnitData monsterToSummon = _teamController.GetMonsterToSummonFromBench(teamData.originalTeamOrder);
        if (monsterToSummon == null) return;

        UnitData monsterToGetOut = _menusController.GetMonsterToGetOut(teamData, _teamController);
        if (monsterToSummon != null && monsterToGetOut != null)
        {
            ApplySummon(samurai, monsterToGetOut, monsterToSummon, teamData);
            markActionExecuted();
        }

        
    }

    public void SummonForMonster(UnitData monsterAttacking, TeamData teamData, Action markActionExecuted,
        Action<UnitData> addToTurnList)
    {
        var monsterToSummon = _teamController.GetMonsterToSummonFromBench(teamData.originalTeamOrder);
        if (monsterToSummon == null) return;

        ApplySummon(monsterAttacking,monsterAttacking, monsterToSummon, teamData);
        addToTurnList(monsterToSummon);
        markActionExecuted();
    }

    private void ApplySummon(UnitData unitDataThatUsedTheAbility, UnitData outMonster, UnitData inMonster, TeamData teamData)
    {
        _view.AnounceThatAMonsterHasBeenSummon(inMonster);
        _teamController.ChangeUnitsWhenSummonIsMade(unitDataThatUsedTheAbility,outMonster, inMonster, teamData);
        _turnsController.ChangeTurnsStateWhenPassOrSummon();
        _turnsController.AnounceTurnsState();
        UpdateTurnList(outMonster, inMonster, teamData);
    }

    private void UpdateTurnList(UnitData outMonster, UnitData inMonster, TeamData teamData)
    {
        if (!teamData.teamUnitsThatAlreadyPlayed.Contains(outMonster)) return;
        int index = _teamController.FindUnitIndexInTeam(outMonster, teamData.teamUnitsThatAlreadyPlayed);
        teamData.teamUnitsThatAlreadyPlayed.Insert(index, inMonster);
        teamData.teamUnitsThatAlreadyPlayed.Remove(outMonster);
        
    }

    public bool SummonAndReviveFromAbility(UnitData unitDataThatUsedAbility, TeamData teamData, Action markActionExecuted)
    {
        UnitData inMonster = _teamController.GetMonsterToSummonFromBenchWhenItCanBeDead(teamData.team);
        if (inMonster == null)
        {
            Console.WriteLine("es null");
            return false;
        }

        UnitData outMonster = _menusController.GetMonsterToGetOut(teamData, _teamController);

        _view.AnounceThatAMonsterHasBeenSummon(inMonster);

        if (inMonster.HP <= 0)
        {
            inMonster.HP = inMonster.maxHP;
            _view.AnounceRevive(unitDataThatUsedAbility,inMonster);
        }

        _teamController.ChangeUnitsWhenSummonIsMade(unitDataThatUsedAbility,outMonster, inMonster, teamData);
        
        UpdateTurnList(outMonster, inMonster, teamData);


        markActionExecuted();
        return true;
    }
    
    public bool SummonFromAbility(UnitData unitDataThatUsedAbility, TeamData teamData, Action markActionExecuted)
    {
        UnitData inMonster = _teamController.GetMonsterToSummonFromBench(teamData.originalTeamOrder);
        if (inMonster == null)
        {
            return false;
        }

        UnitData outMonster = _menusController.GetMonsterToGetOut(teamData, _teamController);
        if(outMonster == null )
        {
            return false;
        }

        _view.AnounceThatAMonsterHasBeenSummon(inMonster);

        _teamController.ChangeUnitsWhenSummonIsMade(unitDataThatUsedAbility, outMonster, inMonster, teamData);
        
        UpdateTurnList(outMonster, inMonster, teamData);


        markActionExecuted();
        return true;
    }



}
