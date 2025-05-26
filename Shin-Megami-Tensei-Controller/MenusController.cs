using Shin_Megami_Tensei_View;
using Shin_Megami_Tensei.Configs;

namespace Shin_Megami_Tensei;

public class MenusController
{
    private View _view;
    private int _indexCounterForTargets;
    private List<UnitData> _allyUnitsThatCanBeTarget = new List<UnitData>();
    public MenusController(View view)
    {
        _view = view;
    }

    public UnitData SelectTarget(List<UnitData> oponentUnits, UnitData unitDataAttacking)
    {
        _view.WriteLine("----------------------------------------");
        _view.WriteLine($"Seleccione un objetivo para {unitDataAttacking.Name}");
        ShowTargetUnitsAlive(oponentUnits);
        ShowCancelOption(oponentUnits);
        return GetTargetUnit(_allyUnitsThatCanBeTarget);
    }
    
    private void ShowTargetUnitsAlive(List<UnitData> enemyUnits)
    {
        _indexCounterForTargets = 1;
        _allyUnitsThatCanBeTarget = new List<UnitData>();
        for (int i = 0; i < enemyUnits.Count; i++)
        {

            UnitData enemyUnitData = enemyUnits[i];
            if (enemyUnitData.HP > 0 && enemyUnitData.active)
            {
                _allyUnitsThatCanBeTarget.Add(enemyUnitData);
                _view.WriteLine($"{_indexCounterForTargets}-{enemyUnitData.Name} HP:{enemyUnitData.HP}/{enemyUnitData.maxHP} MP:{enemyUnitData.MP}/{enemyUnitData.maxMP}");
                _indexCounterForTargets += 1;
            }
            
        }
    }
    
    private void ShowTargetUnitsDead(List<UnitData> enemyUnits)
    {
        _indexCounterForTargets = 1;
        _allyUnitsThatCanBeTarget = new List<UnitData>();
        for (int i = 0; i < enemyUnits.Count; i++)
        {

            UnitData enemyUnitData = enemyUnits[i];
            if (enemyUnitData.HP <= 0)
            {
                _allyUnitsThatCanBeTarget.Add(enemyUnitData);
                _view.WriteLine($"{_indexCounterForTargets}-{enemyUnitData.Name} HP:{enemyUnitData.HP}/{enemyUnitData.maxHP} MP:{enemyUnitData.MP}/{enemyUnitData.maxMP}");
                _indexCounterForTargets += 1;
            }
            
        }
    }
    
    private void ShowCancelOption(List<UnitData> oponentUnits)
    {
        _view.WriteLine($"{_indexCounterForTargets}-Cancelar");
    }
    
    private UnitData GetTargetUnit(List<UnitData> oponentUnits)
    {
        int choice = int.Parse(_view.ReadLine());

        if (choice >= 1 && choice <= oponentUnits.Count)
        {
            return oponentUnits[choice - 1];
        }
        

        return null;
    }

    public void ShowEffectOfDamage(UnitData unitDataAttacking, UnitData target, int damageWithAffinities)
    {

        if (damageWithAffinities > 0)
        {
            _view.WriteLine($"{target.Name} recibe {damageWithAffinities} de daño");

        }
        _view.WriteLine($"{target.Name} termina con HP:{target.HP}/{target.maxHP}");
    }
    
    public void ShowGunTarget(UnitData unitDataAttacking, UnitData target)
    {

    {
        _view.WriteLine("----------------------------------------");
        _view.WriteLine($"{unitDataAttacking.Name} dispara a {target.Name}");
    }    }

    public void ShowAttackTarget(UnitData unitDataAttacking, UnitData target)
    {
        _view.WriteLine("----------------------------------------");
        _view.WriteLine($"{unitDataAttacking.Name} ataca a {target.Name}");
    }
    
    public void ShowFireTarget(UnitData unitDataAttacking, UnitData target)
    {
        _view.WriteLine("----------------------------------------");
        _view.WriteLine($"{unitDataAttacking.Name} lanza fuego a {target.Name}");
    }
    
    public void ShowIceTarget(UnitData unitDataAttacking, UnitData target)
    {
        _view.WriteLine("----------------------------------------");
        _view.WriteLine($"{unitDataAttacking.Name} lanza hielo a {target.Name}");
    }
    
    public void ShowElecTarget(UnitData unitDataAttacking, UnitData target)
    {
        _view.WriteLine("----------------------------------------");
        _view.WriteLine($"{unitDataAttacking.Name} lanza electricidad a {target.Name}");
    }
    
    public void ShowForceTarget(UnitData unitDataAttacking, UnitData target)
    {
        _view.WriteLine("----------------------------------------");
        _view.WriteLine($"{unitDataAttacking.Name} lanza viento a {target.Name}");
    }
    
    public void ShowHealAllyTarget(UnitData unitDataAttacking, UnitData target)
    {
        _view.WriteLine("----------------------------------------");
        _view.WriteLine($"{unitDataAttacking.Name} cura a {target.Name}");
    }

    public void ShowReviveAllyTarget(UnitData unitDataAttacking, UnitData target)
    {
        _view.WriteLine("----------------------------------------");
        _view.WriteLine($"{unitDataAttacking.Name} revive a {target.Name}");
    }

    public void ShowHealResult(int heal, UnitData target)
    {
        _view.WriteLine($"{target.Name} recibe {heal} de HP");
        _view.WriteLine($"{target.Name} termina con HP:{target.HP}/{target.maxHP}");
    }

    public UnitData GetAllyTarget(UnitData unitDataAttacking, TeamData teamData)
    {
        TeamController teamController = teamData.teamController;
        List<UnitData> fullTeam = teamData.team;
        _view.WriteLine("----------------------------------------");
        _view.WriteLine($"Seleccione un objetivo para {unitDataAttacking.Name}");
       List<UnitData> activeUnitsAlive= teamController.GetActiveUnitsAlive(fullTeam);
       ShowTargetUnitsAlive(activeUnitsAlive);
       ShowCancelOption(activeUnitsAlive);
       return GetTargetUnit(_allyUnitsThatCanBeTarget);

    }

    public UnitData GetDeadAllyTarget(UnitData unitDataAttacking, TeamData teamData)
    {
        TeamController teamController = teamData.teamController;
        List<UnitData> fullTeam = teamData.team;
        _view.WriteLine("----------------------------------------");
        _view.WriteLine($"Seleccione un objetivo para {unitDataAttacking.Name}");
        List<UnitData> deadUnits= teamController.GetDeadUnits(fullTeam);
        ShowTargetUnitsDead(deadUnits);
        ShowCancelOption(deadUnits);
        return GetTargetUnit(_allyUnitsThatCanBeTarget);
    }
    
    public void AnounceRound(string samuraiName, string playerNumber)
    {
        _view.WriteLine("----------------------------------------");
        _view.WriteLine($"Ronda de {samuraiName} (J{playerNumber})");
    }

    public void AnounceThatAMonsterHasBeenSummon(UnitData monsterToSummon)
    {
        _view.WriteLine("----------------------------------------");
        _view.WriteLine($"{monsterToSummon.Name} ha sido invocado");
    }
    
    public void AnounceAttackDamage(UnitData unitDataAttacking, UnitData targetUnitData)
    {
        _view.WriteLine("----------------------------------------");
        _view.WriteLine($"{unitDataAttacking.Name} ataca a {targetUnitData.Name}");;
    }
    
    public void AnounceSurrender(string samuaraiName, string playerNumber)
    {
        _view.WriteLine("----------------------------------------");
        _view.WriteLine($"{samuaraiName} (J{playerNumber}) se rinde");
    }
    
    public UnitData GetMonsterToGetOut( TeamData teamData, TeamController teamController)
    {
        _view.WriteLine( "----------------------------------------");
        _view.WriteLine("Seleccione una posición para invocar");
        List<UnitData> activeUnits = teamController.GetActiveUnits(teamData.team);
       
        for (int i = 1; i < activeUnits.Count; i++)
        {
            UnitData unitData = activeUnits[i];
            if (unitData.HP>0)
            {
                _view.WriteLine($"{i}-{unitData.Name} HP:{unitData.HP}/{unitData.maxHP} MP:{unitData.MP}/{unitData.maxMP} (Puesto {i+1})");

                
            }

            else
            {
                _view.WriteLine($"{i}-Vacío (Puesto {i+1})");
            }
        }

        for (int i = 0; i < 4 - activeUnits.Count;i++)
        {
            _view.WriteLine($"{activeUnits.Count+i}-Vacío (Puesto {activeUnits.Count+1+i})");

        }
        
        _view.WriteLine($"4-Cancelar");

        int positionChoosen = Convert.ToInt32(_view.ReadLine());
        if(positionChoosen == activeUnits.Count)
        {
            Console.WriteLine("eligio canselar");
            return null;
        }
       
        return activeUnits[positionChoosen];

    }
    
    public int ShowSelectAbilityMenu(UnitData unitDataAttacking, int skillsOptionsCounter)
    {
        _view.WriteLine("----------------------------------------");
        _view.WriteLine($"Seleccione una habilidad para que {unitDataAttacking.Name} use");
        int updatedSkillsOptionsCounter = ShowUsableSkills(unitDataAttacking, skillsOptionsCounter);
        _view.WriteLine($"{skillsOptionsCounter}-Cancelar");
        return updatedSkillsOptionsCounter;
    }

    private int ShowUsableSkills(UnitData unitDataAttacking, int skillsOptionsCounter)
    {
        foreach (SkillData skill in unitDataAttacking.Skills)
        {
            Console.WriteLine(skill.name);
        }
        foreach (SkillData skill in unitDataAttacking.GetUsableSkills())
        {
            _view.WriteLine($"{skillsOptionsCounter}-{skill.name} MP:{skill.cost}");
            skillsOptionsCounter++;
        }

        return skillsOptionsCounter;

    }
    
    public void AnounceUnitsOrderOfAction(TeamData teamData, TeamController teamController)
    {
        _view.WriteLine("Orden:");
        List<UnitData> activeUnits = teamController.GetActiveUnitsAlive(teamData.team);
        List<UnitData> activeUnitsInOrder = teamController.GetActiveUnitsInOrderOfAction(activeUnits, teamData.teamUnitsThatAlreadyPlayed);
            
        for (int i = 0; i < activeUnitsInOrder.Count; i++)
        {
            UnitData unitData = activeUnitsInOrder[i];
            _view.WriteLine($"{i + 1}-{unitData.Name}");
        }

    }

    public void ShowActionMenu(UnitData unitData)
    {
        _view.WriteLine("----------------------------------------");
        _view.WriteLine($"Seleccione una acción para {unitData.Name}");
    
        if (unitData is Samurai)
        {
            ShowActionsForSamurai();
        }
        else if (unitData is Monster)
        {
            ShowActionsForMonster();
        }
    
    }

    public void ShowActionsForSamurai()
    {
        _view.WriteLine("1: Atacar");
        _view.WriteLine("2: Disparar");
        _view.WriteLine("3: Usar Habilidad");
        _view.WriteLine("4: Invocar");
        _view.WriteLine("5: Pasar Turno");
        _view.WriteLine("6: Rendirse");
    }
        
    public void ShowActionsForMonster()
    {
        _view.WriteLine("1: Atacar");
        _view.WriteLine("2: Usar Habilidad");
        _view.WriteLine("3: Invocar");
        _view.WriteLine("4: Pasar Turno");
    }

    public void AnounceHPFinalState(UnitData unitData)
    {
        _view.WriteLine($"{unitData.Name} termina con HP:{unitData.HP}/{unitData.maxHP}");;

    }

    public void AnounceFinalStateAfterAbility(AffinitiesController affinitiesController, UnitData unitDataAttacking, UnitData target)
    {
        int damageWithAffinities = affinitiesController.ApplyAffinity();

        if (!affinitiesController.IsReturnDamageAffinity())
        {
            ShowEffectOfDamage(unitDataAttacking, target, damageWithAffinities);

        }
        
        else
        {
            _view.WriteLine($"{unitDataAttacking.Name} termina con HP:{unitDataAttacking.HP}/{unitDataAttacking.maxHP}");;

        }
    }
    
    
    public void AnounceRevive( UnitData attackingUnitData, UnitData allyUnitData)
    {
        _view.WriteLine($"{attackingUnitData.Name} revive a {allyUnitData.Name}");
        _view.WriteLine($"{allyUnitData.Name} recibe {allyUnitData.maxHP} de HP");
        _view.WriteLine($"{allyUnitData.Name} termina con HP:{allyUnitData.HP}/{allyUnitData.maxHP}");
    }

}