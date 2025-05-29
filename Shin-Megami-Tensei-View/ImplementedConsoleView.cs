namespace Shin_Megami_Tensei_View;

public class ImplementedConsoleView: IView
{
    private View _view;
    private string[] _teamFilesSorted;
    private int _monstersToSummonCounter = 1;
    private int _monstersToSummonCounterWhenItCanBeDead = 1;
    private List<UnitData> _unitsToSummon;
    private List<UnitData> _unitsToSummonWhenTheyCanBeDead;
    private int _indexCounterForTargets;
    private List<UnitData> _allyUnitsThatCanBeTarget = new List<UnitData>();

    public ImplementedConsoleView(View view)
    {
        _view = view;
    }

    public void AnounceThatPlayerWon(string samuraiName, string playerNumber)
    {
        _view.WriteLine("----------------------------------------");
        _view.WriteLine($"Ganador: {samuraiName} (J{playerNumber})");
        ;
    }

    private void ShowTeams(List<string> teamLines)
    {
        foreach (string line in teamLines)
        {
            _view.WriteLine(line);
        }

    }

    public void ShowBothTeams(List<string> firstTeamLines, List<String> secondTeamLines)
    {
        _view.WriteLine("----------------------------------------");
        ShowTeams(firstTeamLines);
        ShowTeams(secondTeamLines);

    }

    public void AnounceGunDamage(UnitData unitDataAttacking, UnitData targetUnitData)
    {
        _view.WriteLine("----------------------------------------");
        _view.WriteLine($"{unitDataAttacking.Name} dispara a {targetUnitData.Name}");
    }
    public void AnounceGunDamageWithoutLines(UnitData unitDataAttacking, UnitData targetUnitData)
    {
        _view.WriteLine($"{unitDataAttacking.Name} dispara a {targetUnitData.Name}");
    }

    public void ShowSelectAbilityMenu(UnitData unitDataAttacking)
    {
        int skillsOptionsCounter = 1;
        _view.WriteLine("----------------------------------------");
        _view.WriteLine($"Seleccione una habilidad para que {unitDataAttacking.Name} use");
        foreach (SkillData skill in unitDataAttacking.GetUsableSkills())
        {
            _view.WriteLine($"{skillsOptionsCounter}-{skill.name} MP:{skill.cost}");
            skillsOptionsCounter++;
        }

        _view.WriteLine($"{skillsOptionsCounter}-Cancelar");

    }

    public int GetNumericSelectedOptionFromUser()
    {
        int choice = int.Parse(_view.ReadLine());
        return choice;

    }

    public void AnounceHPFinalStateForUnit(UnitData unitData)
    {
        _view.WriteLine($"{unitData.Name} termina con HP:{unitData.HP}/{unitData.maxHP}");
        ;

    }

    public string[] ShowTeamsToSelect(string teamsFolder)
    {
        _view.WriteLine("Elige un archivo para cargar los equipos");
        ShowTeamOptions(teamsFolder);
        return _teamFilesSorted;
    }

    private void ShowTeamOptions(string teamsFolder)
    {
        string[] teamFiles = Directory.GetFiles(teamsFolder, "*.txt");

        List<Tuple<string, int>> sortedTeamFiles = SortTeamFiles(teamFiles);
        sortedTeamFiles = sortedTeamFiles.OrderBy(t => t.Item2).ToList();

        _teamFilesSorted = sortedTeamFiles.Select(t => t.Item1).ToArray();

        ShowFilesSorted();
    }

    private List<Tuple<string, int>> SortTeamFiles(string[] teamFiles)
    {
        List<Tuple<string, int>> sortedTeamFiles = new List<Tuple<string, int>>();

        foreach (string filePath in teamFiles)
        {
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            int number;
            int.TryParse(fileName, out number);
            sortedTeamFiles.Add(new Tuple<string, int>(filePath, number));

        }

        return sortedTeamFiles;
    }

    private void ShowFilesSorted()
    {
        for (int i = 0; i < _teamFilesSorted.Length; i++)
        {
            _view.WriteLine($"{i}: {Path.GetFileName(_teamFilesSorted[i])}");
        }
    }


    public void AnounceThatTeamIsInvalid()
    {
        _view.WriteLine("Archivo de equipos inválido");
    }
    
    

    public UnitData SelectMonsterToSummonMenu(List<UnitData> team)
    {
        _view.WriteLine("----------------------------------------");
        _view.WriteLine("Seleccione un monstruo para invocar");
        ShowMonstersToSummon(team);
        _view.WriteLine($"{_monstersToSummonCounter}-Cancelar");
        int selectedMonsterIndex = Convert.ToInt32(_view.ReadLine());

        if (selectedMonsterIndex == _monstersToSummonCounter)
        {
            return null;
        }
        return _unitsToSummon[selectedMonsterIndex - 1];

    }

    private void ShowMonstersToSummon(List<UnitData> team)
    {
        _monstersToSummonCounter = 1;
        _unitsToSummon = new List<UnitData>();
        foreach (UnitData unit in team)
        {
            if (!unit.active && unit.HP > 0 && unit is not Samurai)

            {
                _view.WriteLine($"{_monstersToSummonCounter}-{unit.Name} HP:{unit.HP}/{unit.maxHP} MP:{unit.MP}/{unit.maxMP}");
                _unitsToSummon.Add(unit);
                _monstersToSummonCounter++;
            }

        }
    }
    
    
    public UnitData GetMonsterToSummonFromBenchWhenItCanBeDead(List<UnitData> team)
    {
        _view.WriteLine("----------------------------------------");
        _view.WriteLine("Seleccione un monstruo para invocar");
        ShowMonstersToSummonWhenTheyCanBeDead(team);
        
        _view.WriteLine($"{_monstersToSummonCounterWhenItCanBeDead}-Cancelar");
        int selectedMonsterIndex = Convert.ToInt32(_view.ReadLine());
        if (selectedMonsterIndex == _monstersToSummonCounterWhenItCanBeDead)
        {
            return null;
        }
        return _unitsToSummonWhenTheyCanBeDead[selectedMonsterIndex - 1];
    }

    private void ShowMonstersToSummonWhenTheyCanBeDead(List<UnitData> team)
    {
        _monstersToSummonCounterWhenItCanBeDead = 1;
        _unitsToSummonWhenTheyCanBeDead = new List<UnitData>();
        foreach (UnitData unit in team)
        {
            if ((!unit.active || unit.HP<=0) && unit is not Samurai)
            {
                _view.WriteLine($"{_monstersToSummonCounterWhenItCanBeDead}-{unit.Name} HP:{unit.HP}/{unit.maxHP} MP:{unit.MP}/{unit.maxMP}");
                _unitsToSummonWhenTheyCanBeDead .Add(unit);
                _monstersToSummonCounterWhenItCanBeDead++;
            }

        }
    }

    public void AnounceThatTargetUnitIsWeak(UnitData targetUnitData, UnitData unitDataAttacking)
    {
        _view.WriteLine($"{targetUnitData.Name} es débil contra el ataque de {unitDataAttacking.Name}");

    }

    public void AnounceThatTargetIsRessistent(UnitData targetUnitData, UnitData unitDataAttacking)
    {
        _view.WriteLine($"{targetUnitData.Name} es resistente el ataque de {unitDataAttacking.Name}");

    }
    
    public void AnounceThatTargetBlockedAttack(UnitData targetUnitData, UnitData unitDataAttacking)
    {
        _view.WriteLine($"{targetUnitData.Name} bloquea el ataque de {unitDataAttacking.Name}");
    }
    
    public void AnounceThatTargetUnitAbsorbedDamage(UnitData targetUnitData, int damage)
    {
        _view.WriteLine($"{targetUnitData.Name} absorbe {damage} daño");
    }
    
    public void AnounceThatTargetUnitReflectedDamage(UnitData targetUnitData, UnitData unitDataAttacking, int baseDamage)
    {
        _view.WriteLine($"{targetUnitData.Name} devuelve {baseDamage} daño a {unitDataAttacking.Name}");
    }

    public void ShowSeparator()
    {
        _view.WriteLine("----------------------------------------");

    }
    
    public void AnounceAttack(UnitData unitDataAttacking, UnitData targetUnitData)
    {
        _view.WriteLine($"{unitDataAttacking.Name} ataca a {targetUnitData.Name}");
    }

    public void AnounceDamageReceived(UnitData target, int damageWithAffinities)
    {
        _view.WriteLine($"{target.Name} recibe {damageWithAffinities} de daño");
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

    public void AnounceEffectOfDamage(UnitData unitDataAttacking, UnitData target, int damageWithAffinities)
    {

        if (damageWithAffinities > 0)
        {
            _view.WriteLine($"{target.Name} recibe {damageWithAffinities} de daño");

        }
        _view.WriteLine($"{target.Name} termina con HP:{target.HP}/{target.maxHP}");
    }
    
    public void AnounceGunTarget(UnitData unitDataAttacking, UnitData target)
    {

    {
        _view.WriteLine("----------------------------------------");
        _view.WriteLine($"{unitDataAttacking.Name} dispara a {target.Name}");
    }    }

    public void AnounceAttackTarget(UnitData unitDataAttacking, UnitData target)
    {
        _view.WriteLine("----------------------------------------");
        _view.WriteLine($"{unitDataAttacking.Name} ataca a {target.Name}");
    }

    public void AnounceAttackWithouLines(UnitData unitDataAttacking, UnitData target)
    {
        _view.WriteLine($"{unitDataAttacking.Name} ataca a {target.Name}");

    }
    
    public void AnounceFireTarget(UnitData unitDataAttacking, UnitData target)
    {
        _view.WriteLine("----------------------------------------");
        _view.WriteLine($"{unitDataAttacking.Name} lanza fuego a {target.Name}");
    }
    
    public void AnounceIceTarget(UnitData unitDataAttacking, UnitData target)
    {
        _view.WriteLine("----------------------------------------");
        _view.WriteLine($"{unitDataAttacking.Name} lanza hielo a {target.Name}");
    }
    
    public void AnounceElecTarget(UnitData unitDataAttacking, UnitData target)
    {
        _view.WriteLine("----------------------------------------");
        _view.WriteLine($"{unitDataAttacking.Name} lanza electricidad a {target.Name}");
    }
    
    public void AnounceForceTarget(UnitData unitDataAttacking, UnitData target)
    {
        _view.WriteLine("----------------------------------------");
        _view.WriteLine($"{unitDataAttacking.Name} lanza viento a {target.Name}");
    }
    
    public void AnounceFireTargetWithoutLines(UnitData unitDataAttacking, UnitData target)
    {
        _view.WriteLine($"{unitDataAttacking.Name} lanza fuego a {target.Name}");
    }
    
    public void AnounceIceTargetWithoutLines(UnitData unitDataAttacking, UnitData target)
    {
        _view.WriteLine($"{unitDataAttacking.Name} lanza hielo a {target.Name}");
    }
    
    public void AnounceElecTargetWithoutLines(UnitData unitDataAttacking, UnitData target)
    {
        _view.WriteLine($"{unitDataAttacking.Name} lanza electricidad a {target.Name}");
    }
    
    public void AnounceForceTargetWithoutLines(UnitData unitDataAttacking, UnitData target)
    {
        _view.WriteLine($"{unitDataAttacking.Name} lanza viento a {target.Name}");
    }
    
    public void AnounceHealAllyTargetWithoutLines(UnitData unitDataAttacking, UnitData target)
    {
        _view.WriteLine($"{unitDataAttacking.Name} cura a {target.Name}");
    }
    public void AnounceHealAllyTarget(UnitData unitDataAttacking, UnitData target)
    {
        _view.WriteLine("----------------------------------------");
        _view.WriteLine($"{unitDataAttacking.Name} cura a {target.Name}");
    }

    public void AnounceReviveAllyTarget(UnitData unitDataAttacking, UnitData target)
    {
        _view.WriteLine("----------------------------------------");
        _view.WriteLine($"{unitDataAttacking.Name} revive a {target.Name}");
    }
    public void AnounceReviveAllyTargetWithoutLines(UnitData unitDataAttacking, UnitData target)
    {
        _view.WriteLine($"{unitDataAttacking.Name} revive a {target.Name}");
    }
    
    public void AnounceCharge(UnitData unitDataAttacking)
    {
        _view.WriteLine("----------------------------------------");
        _view.WriteLine($"{unitDataAttacking.Name} ha cargado su siguiente ataque físico o disparo a más del doble");
    }
    
    public void AnounceConcentrate(UnitData unitDataAttacking)
    {
        _view.WriteLine("----------------------------------------");
        _view.WriteLine($"{unitDataAttacking.Name} ha cargado su siguiente ataque mágico a más del doble");
    }

    public void AnounceBloodRitual(UnitData unitDataAttacking)
    {
        _view.WriteLine("----------------------------------------");
        _view.WriteLine($"El ataque de {unitDataAttacking.Name} ha aumentado");
        _view.WriteLine($"La defensa de {unitDataAttacking.Name} ha aumentado");
        AnounceHPFinalStateForUnit(unitDataAttacking);
    }

    public void AnounceHealResult(int heal, UnitData target)
    {
        _view.WriteLine($"{target.Name} recibe {heal} de HP");
        _view.WriteLine($"{target.Name} termina con HP:{target.HP}/{target.maxHP}");
    }

    public UnitData GetAllyTarget(UnitData unitDataAttacking, List<UnitData> activeUnitsAlive)
    {
        
        _view.WriteLine("----------------------------------------");
        _view.WriteLine($"Seleccione un objetivo para {unitDataAttacking.Name}");
       ShowTargetUnitsAlive(activeUnitsAlive);
       ShowCancelOption(activeUnitsAlive);
       return GetTargetUnit(_allyUnitsThatCanBeTarget);

    }

    public UnitData GetDeadAllyTarget(UnitData unitDataAttacking, List<UnitData> deadUnits)
    {
        
        _view.WriteLine("----------------------------------------");
        _view.WriteLine($"Seleccione un objetivo para {unitDataAttacking.Name}");
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
    
    public UnitData GetMonsterToGetOut(List<UnitData> activeUnits)
    {
        _view.WriteLine( "----------------------------------------");
        _view.WriteLine("Seleccione una posición para invocar");

        ShowPositionsToGetOut(activeUnits);
        
        _view.WriteLine($"4-Cancelar");

        int positionChoosen = Convert.ToInt32(_view.ReadLine());
        if(positionChoosen == 4)
        {
            return null;
        }
       
        return activeUnits[positionChoosen];

    }
    
    private void ShowPositionsToGetOut(List<UnitData> activeUnits)
    {
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
    }
    
    public void AnounceUnitsOrderOfAction(List<UnitData> activeUnitsInOrder)
    {
        _view.WriteLine("Orden:");
        
            
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



    
    public void AnounceRevive( UnitData attackingUnitData, UnitData allyUnitData)
    {
        _view.WriteLine($"{attackingUnitData.Name} revive a {allyUnitData.Name}");
        _view.WriteLine($"{allyUnitData.Name} recibe {allyUnitData.maxHP} de HP");
        _view.WriteLine($"{allyUnitData.Name} termina con HP:{allyUnitData.HP}/{allyUnitData.maxHP}");
    }


    public void AnounceTurnsState(int fullTurnsUsedDuringThisAction, int blinkinTurnsUsedDuringThisAction, int blinkinTurnsoObtainedDuringThisAction)
    {
        _view.WriteLine("----------------------------------------");
        _view.WriteLine($"Se han consumido {fullTurnsUsedDuringThisAction} Full Turn(s) y {blinkinTurnsUsedDuringThisAction} Blinking Turn(s)");
        _view.WriteLine($"Se han obtenido {blinkinTurnsoObtainedDuringThisAction} Blinking Turn(s)");
    }
    
    public void ShowNumberOfTurns(int numberOfFullTurnsLeft, int blinkingTurnsCounter)
    {

        _view.WriteLine("----------------------------------------");
        _view.WriteLine($"Full Turns: {numberOfFullTurnsLeft}");
        _view.WriteLine($"Blinking Turns: {blinkingTurnsCounter}");
        _view.WriteLine("----------------------------------------");
    }

    public void AnounceLightAttack(UnitData unitDataAttacking, UnitData unitDataTarget)
    {

        _view.WriteLine($"{unitDataAttacking.Name} ataca con luz a {unitDataTarget.Name}");
    }
    
    public void AnounceDarkAttack(UnitData unitDataAttacking, UnitData unitDataTarget)
    {

        _view.WriteLine($"{unitDataAttacking.Name} ataca con oscuridad a {unitDataTarget.Name}");
    }

    public void AnounceThatInstaKillFailed(UnitData unitDataAttacking)
    {
        _view.WriteLine($"{unitDataAttacking.Name} ha fallado el ataque");
    }
    
    public void AnounceThatTargetHasBeenEliminated(UnitData targetUnitData)
    {
        _view.WriteLine($"{targetUnitData.Name} ha sido eliminado");
    }

    public void ShowLines()
    {
        _view.WriteLine("----------------------------------------");
    }

    
    



}