using Shin_Megami_Tensei_View;
using Shin_Megami_Tensei.Configs;

namespace Shin_Megami_Tensei;

public class MenusController
{
    private View _view;
    private int _indexCounterForTargets;
    private List<Unit> _allyUnitsThatCanBeTarget = new List<Unit>();
    public MenusController(View view)
    {
        _view = view;
    }

    public Unit SelectTarget(List<Unit> oponentUnits, Unit unitAttacking)
    {
        _view.WriteLine("----------------------------------------");
        _view.WriteLine($"Seleccione un objetivo para {unitAttacking.Name}");
        ShowTargetUnitsAlive(oponentUnits);
        ShowCancelOption(oponentUnits);
        return GetTargetUnit(_allyUnitsThatCanBeTarget);
    }
    
    private void ShowTargetUnitsAlive(List<Unit> enemyUnits)
    {
        _indexCounterForTargets = 1;
        _allyUnitsThatCanBeTarget = new List<Unit>();
        for (int i = 0; i < enemyUnits.Count; i++)
        {

            Unit enemyUnit = enemyUnits[i];
            if (enemyUnit.HP > 0 && enemyUnit.active)
            {
                _allyUnitsThatCanBeTarget.Add(enemyUnit);
                _view.WriteLine($"{_indexCounterForTargets}-{enemyUnit.Name} HP:{enemyUnit.HP}/{enemyUnit.maxHP} MP:{enemyUnit.MP}/{enemyUnit.maxMP}");
                _indexCounterForTargets += 1;
            }
            
        }
    }
    
    private void ShowTargetUnitsDead(List<Unit> enemyUnits)
    {
        _indexCounterForTargets = 1;
        _allyUnitsThatCanBeTarget = new List<Unit>();
        for (int i = 0; i < enemyUnits.Count; i++)
        {

            Unit enemyUnit = enemyUnits[i];
            if (enemyUnit.HP <= 0)
            {
                _allyUnitsThatCanBeTarget.Add(enemyUnit);
                _view.WriteLine($"{_indexCounterForTargets}-{enemyUnit.Name} HP:{enemyUnit.HP}/{enemyUnit.maxHP} MP:{enemyUnit.MP}/{enemyUnit.maxMP}");
                _indexCounterForTargets += 1;
            }
            
        }
    }
    
    private void ShowCancelOption(List<Unit> oponentUnits)
    {
        _view.WriteLine($"{_indexCounterForTargets}-Cancelar");
    }
    
    private Unit GetTargetUnit(List<Unit> oponentUnits)
    {
        int choice = int.Parse(_view.ReadLine());

        if (choice >= 1 && choice <= oponentUnits.Count)
        {
            return oponentUnits[choice - 1];
        }
        

        return null;
    }

    public void ShowEffectOfDamage(Unit unitAttacking, Unit target, int damageWithAffinities)
    {

        if (damageWithAffinities > 0)
        {
            _view.WriteLine($"{target.Name} recibe {damageWithAffinities} de daño");

        }
        _view.WriteLine($"{target.Name} termina con HP:{target.HP}/{target.maxHP}");
    }
    
    public void ShowGunTarget(Unit unitAttacking, Unit target)
    {

    {
        _view.WriteLine("----------------------------------------");
        _view.WriteLine($"{unitAttacking.Name} dispara a {target.Name}");
    }    }

    public void ShowAttackTarget(Unit unitAttacking, Unit target)
    {
        _view.WriteLine("----------------------------------------");
        _view.WriteLine($"{unitAttacking.Name} ataca a {target.Name}");
    }
    
    public void ShowFireTarget(Unit unitAttacking, Unit target)
    {
        _view.WriteLine("----------------------------------------");
        _view.WriteLine($"{unitAttacking.Name} lanza fuego a {target.Name}");
    }
    
    public void ShowIceTarget(Unit unitAttacking, Unit target)
    {
        _view.WriteLine("----------------------------------------");
        _view.WriteLine($"{unitAttacking.Name} lanza hielo a {target.Name}");
    }
    
    public void ShowElecTarget(Unit unitAttacking, Unit target)
    {
        _view.WriteLine("----------------------------------------");
        _view.WriteLine($"{unitAttacking.Name} lanza electricidad a {target.Name}");
    }
    
    public void ShowForceTarget(Unit unitAttacking, Unit target)
    {
        _view.WriteLine("----------------------------------------");
        _view.WriteLine($"{unitAttacking.Name} lanza viento a {target.Name}");
    }
    
    public void ShowHealAllyTarget(Unit unitAttacking, Unit target)
    {
        _view.WriteLine("----------------------------------------");
        _view.WriteLine($"{unitAttacking.Name} cura a {target.Name}");
    }

    public void ShowReviveAllyTarget(Unit unitAttacking, Unit target)
    {
        _view.WriteLine("----------------------------------------");
        _view.WriteLine($"{unitAttacking.Name} revive a {target.Name}");
    }

    public void ShowHealResult(int heal, Unit target)
    {
        _view.WriteLine($"{target.Name} recibe {heal} de HP");
        _view.WriteLine($"{target.Name} termina con HP:{target.HP}/{target.maxHP}");
    }

    public Unit GetAllyTarget(Unit unitAttacking, TeamData teamData)
    {
        TeamController teamController = teamData.teamController;
        List<Unit> fullTeam = teamData.team;
        _view.WriteLine("----------------------------------------");
        _view.WriteLine($"Seleccione un objetivo para {unitAttacking.Name}");
       List<Unit> activeUnitsAlive= teamController.GetActiveUnitsAlive(fullTeam);
       ShowTargetUnitsAlive(activeUnitsAlive);
       ShowCancelOption(activeUnitsAlive);
       return GetTargetUnit(_allyUnitsThatCanBeTarget);

    }

    public Unit GetDeadAllyTarget(Unit unitAttacking, TeamData teamData)
    {
        TeamController teamController = teamData.teamController;
        List<Unit> fullTeam = teamData.team;
        _view.WriteLine("----------------------------------------");
        _view.WriteLine($"Seleccione un objetivo para {unitAttacking.Name}");
        List<Unit> deadUnits= teamController.GetDeadUnits(fullTeam);
        ShowTargetUnitsDead(deadUnits);
        ShowCancelOption(deadUnits);
        return GetTargetUnit(_allyUnitsThatCanBeTarget);
    }
    
    public void AnounceRound(string samuraiName, string playerNumber)
    {
        _view.WriteLine("----------------------------------------");
        _view.WriteLine($"Ronda de {samuraiName} (J{playerNumber})");
    }

    public void AnounceThatAMonsterHasBeenSummon(Unit monsterToSummon)
    {
        _view.WriteLine("----------------------------------------");
        _view.WriteLine($"{monsterToSummon.Name} ha sido invocado");
    }
    
    public void AnounceAttackDamage(Unit unitAttacking, Unit targetUnit)
    {
        _view.WriteLine("----------------------------------------");
        _view.WriteLine($"{unitAttacking.Name} ataca a {targetUnit.Name}");;
    }
    
    public void AnounceSurrender(string samuaraiName, string playerNumber)
    {
        _view.WriteLine("----------------------------------------");
        _view.WriteLine($"{samuaraiName} (J{playerNumber}) se rinde");
    }
    
    public Unit GetMonsterToGetOut( TeamData teamData, TeamController teamController)
    {
        _view.WriteLine( "----------------------------------------");
        _view.WriteLine("Seleccione una posición para invocar");
        List<Unit> activeUnits = teamController.GetActiveUnits(teamData.team);
       
        for (int i = 1; i < activeUnits.Count; i++)
        {
            Unit unit = activeUnits[i];
            if (unit.HP>0)
            {
                _view.WriteLine($"{i}-{unit.Name} HP:{unit.HP}/{unit.maxHP} MP:{unit.MP}/{unit.maxMP} (Puesto {i+1})");

                
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
    
    public int ShowSelectAbilityMenu(Unit unitAttacking, int skillsOptionsCounter)
    {
        _view.WriteLine("----------------------------------------");
        _view.WriteLine($"Seleccione una habilidad para que {unitAttacking.Name} use");
        int updatedSkillsOptionsCounter = ShowUsableSkills(unitAttacking, skillsOptionsCounter);
        _view.WriteLine($"{skillsOptionsCounter}-Cancelar");
        return updatedSkillsOptionsCounter;
    }

    private int ShowUsableSkills(Unit unitAttacking, int skillsOptionsCounter)
    {
        foreach (SkillData skill in unitAttacking.Skills)
        {
            Console.WriteLine(skill.name);
        }
        foreach (SkillData skill in unitAttacking.GetUsableSkills())
        {
            _view.WriteLine($"{skillsOptionsCounter}-{skill.name} MP:{skill.cost}");
            skillsOptionsCounter++;
        }

        return skillsOptionsCounter;

    }
    
    public void AnounceUnitsOrderOfAction(TeamData teamData, TeamController teamController)
    {
        _view.WriteLine("Orden:");
        List<Unit> activeUnits = teamController.GetActiveUnitsAlive(teamData.team);
        List<Unit> activeUnitsInOrder = teamController.GetActiveUnitsInOrderOfAction(activeUnits, teamData.teamUnitsThatAlreadyPlayed);
            
        for (int i = 0; i < activeUnitsInOrder.Count; i++)
        {
            Unit unit = activeUnitsInOrder[i];
            _view.WriteLine($"{i + 1}-{unit.Name}");
        }

    }

    public void ShowActionMenu(Unit unit)
    {
        _view.WriteLine("----------------------------------------");
        _view.WriteLine($"Seleccione una acción para {unit.Name}");
    
        if (unit is Samurai)
        {
            ShowActionsForSamurai();
        }
        else if (unit is Monster)
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

    public void AnounceHPFinalState(Unit unit)
    {
        _view.WriteLine($"{unit.Name} termina con HP:{unit.HP}/{unit.maxHP}");;

    }

    public void AnounceFinalStateAfterAbility(AffinitiesController affinitiesController, Unit unitAttacking, Unit target)
    {
        int damageWithAffinities = affinitiesController.ApplyAffinity();

        if (!affinitiesController.IsReturnDamageAffinity())
        {
            ShowEffectOfDamage(unitAttacking, target, damageWithAffinities);

        }
        
        else
        {
            _view.WriteLine($"{unitAttacking.Name} termina con HP:{unitAttacking.HP}/{unitAttacking.maxHP}");;

        }
    }
    
    
    public void AnounceRevive( Unit attackingUnit, Unit allyUnit)
    {
        _view.WriteLine($"{attackingUnit.Name} revive a {allyUnit.Name}");
        _view.WriteLine($"{allyUnit.Name} recibe {allyUnit.maxHP} de HP");
        _view.WriteLine($"{allyUnit.Name} termina con HP:{allyUnit.HP}/{allyUnit.maxHP}");
    }

}