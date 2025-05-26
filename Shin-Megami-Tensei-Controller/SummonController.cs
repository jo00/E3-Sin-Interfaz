using Shin_Megami_Tensei;
using Shin_Megami_Tensei_View;
using Shin_Megami_Tensei.Configs;

public class SummonController
{
    private readonly View _view;
    private readonly TeamController _teamController;
    private readonly TurnsController _turnsController;
    private readonly MenusController _menusController;

    public SummonController(View view, TeamController teamController, TurnsController turnsController,
        MenusController menusController)
    {
        _view = view;
        _teamController = teamController;
        _turnsController = turnsController;
        _menusController = menusController;
    }

    public void SummonForSamurai(Unit samurai, TeamData teamData, Action markActionExecuted)
    {
        Unit monsterToSummon = _teamController.GetMonsterToSummonFromBench(teamData.team);
        if (monsterToSummon == null) return;

        Unit monsterToGetOut = _menusController.GetMonsterToGetOut(teamData, _teamController);
        if (monsterToSummon != null && monsterToGetOut != null)
        {
            ApplySummon(samurai, monsterToGetOut, monsterToSummon, teamData);
            markActionExecuted();
        }

        
    }

    public void SummonForMonster(Unit monsterAttacking, TeamData teamData, Action markActionExecuted,
        Action<Unit> addToTurnList)
    {
        var monsterToSummon = _teamController.GetMonsterToSummonFromBench(teamData.team);
        if (monsterToSummon == null) return;

        ApplySummon(monsterAttacking,monsterAttacking, monsterToSummon, teamData);
        addToTurnList(monsterToSummon);
        markActionExecuted();
    }

    private void ApplySummon(Unit unitThatUsedTheAbility, Unit outMonster, Unit inMonster, TeamData teamData)
    {
        _menusController.AnounceThatAMonsterHasBeenSummon(inMonster);
        _teamController.ChangeUnitsWhenSummonIsMade(unitThatUsedTheAbility,outMonster, inMonster, teamData);
        _turnsController.ChangeTurnsStateWhenPassOrSummon();
        _turnsController.AnounceTurnsState();
        UpdateTurnList(outMonster, inMonster, teamData);
    }

    private void UpdateTurnList(Unit outMonster, Unit inMonster, TeamData teamData)
    {
        if (!teamData.teamUnitsThatAlreadyPlayed.Contains(outMonster)) return;
        int index = _teamController.FindUnitIndexInTeam(outMonster, teamData.teamUnitsThatAlreadyPlayed);
        teamData.teamUnitsThatAlreadyPlayed.Insert(index, inMonster);
        teamData.teamUnitsThatAlreadyPlayed.Remove(outMonster);
        
    }

    public bool SummonAndReviveFromAbility(Unit unitThatUsedAbility, TeamData teamData, Action markActionExecuted)
    {
        Unit inMonster = _teamController.GetMonsterToSummonFromBenchWhenItCanBeDead(teamData.team);
        if (inMonster == null)
        {
            Console.WriteLine("es null");
            return false;
        }

        Unit outMonster = _menusController.GetMonsterToGetOut(teamData, _teamController);

        _menusController.AnounceThatAMonsterHasBeenSummon(inMonster);

        if (inMonster.HP <= 0)
        {
            inMonster.HP = inMonster.maxHP;
            _menusController.AnounceRevive(unitThatUsedAbility,inMonster);
        }

        _teamController.ChangeUnitsWhenSummonIsMade(unitThatUsedAbility,outMonster, inMonster, teamData);
        
        UpdateTurnList(outMonster, inMonster, teamData);


        markActionExecuted();
        return true;
    }
    
    public bool SummonFromAbility(Unit unitThatUsedAbility, TeamData teamData, Action markActionExecuted)
    {
        Unit inMonster = _teamController.GetMonsterToSummonFromBench(teamData.team);
        if (inMonster == null)
        {
            return false;
        }

        Unit outMonster = _menusController.GetMonsterToGetOut(teamData, _teamController);
        if(outMonster == null )
        {
            return false;
        }

        _menusController.AnounceThatAMonsterHasBeenSummon(inMonster);

        _teamController.ChangeUnitsWhenSummonIsMade(unitThatUsedAbility, outMonster, inMonster, teamData);
        
        UpdateTurnList(outMonster, inMonster, teamData);


        markActionExecuted();
        return true;
    }



}
