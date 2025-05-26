using Shin_Megami_Tensei_View;
using Shin_Megami_Tensei.Configs;
using Shin_Megami_Tensei.Skills;

namespace Shin_Megami_Tensei;

public class Game
{
    private List<SkillData> _skills;
    private List<Monster> _monsters;
    private List<Samurai> _samurais;
    private View _view;
    private DataLoader _loader;
    private string _teamsFolder;
    private List<Unit> _firstTeam;
    private List<Unit> _firstTeamUnitsThatAlreadyPlayed = new List<Unit>();
    private List<Unit> _secondTeamUnitsThatAlreadyPlayed = new List<Unit>();

    private List<Unit> _secondTeam;
    private Samurai _firstSamurai;
    private Samurai _secondSamurai;
    private TeamController _teamController;
   
    private bool _actionExecuted;
    
    
    private bool _canSecondTeamKeepPlaying = true;
    private bool _canFirstTeamKeepPlaying = true;

    private int _skillsOptionsCounter;
    private TeamData _firstTeamData;
    private TeamData _secondTeamData;
    private string _numberOneAsString = "1";
    private string _numberTwoAsString = "2";

    private bool _wasActionAMonsterSummon;

    private TurnsController _turnsController;
    private MenusController _menusController;
    
    private Unit _monsterToGetOut;
    private Unit _monsterToSummon;
    private SummonController _summonController;


    public Game(View view, string teamsFolder)
    {
        _view = view;
        _loader = new DataLoader(view);
        _teamsFolder = teamsFolder;
        _turnsController = new TurnsController(view);
        _menusController = new MenusController(view);

    }

    public void Play()
    {

        InitializeGame(_teamsFolder);
    }

    public void InitializeGame(string teamsFolder)
    {
        InstantiateData();
        InitializeTeams(teamsFolder);
       
        if (_teamController.AreBoothTeamsValid())
        {
            InstantiateValidTeams();
            PlayTurns();
        }
    }
    private void InstantiateData()
    {
        _skills = _loader.LoadSkills("data/skills.json");
        _monsters = _loader.GetMonsters("data/monsters.json");
        _samurais = _loader.GetSamurais("data/samurai.json", _skills);
        
    }

    private void InitializeTeams(string teamsFolder)
    {
        _teamController = new TeamController(_view, _samurais, _monsters, _skills);
        _teamController.SelectTeam(teamsFolder);
        _summonController = new SummonController(_view, _teamController, _turnsController, _menusController);

    }

    private void PlayTurns()
    {
        while (_canFirstTeamKeepPlaying && _canSecondTeamKeepPlaying)
        {
            
            ResetSpeedForOrderValues(_firstTeamData.team);
            PlayFirstPlayerTurn();
            
            if (_canSecondTeamKeepPlaying && _canFirstTeamKeepPlaying)
            {
                ResetSpeedForOrderValues(_secondTeamData.team);
                PlaySecondPlayerTurn();
            }
         
        }
    }

    private void ResetSpeedForOrderValues(List<Unit> team)
    {
        foreach (Unit unit in team)
        {
            unit.speedForOrder = unit.Speed;
        }
        
            
    }

    private void PlayFirstPlayerTurn()
    {
        PlayPlayerTurn(_firstTeamData, _secondTeamData.team);
        _canSecondTeamKeepPlaying = _teamController.CanTeamKeepPlaying(_secondTeamData.team);
        if (!_canFirstTeamKeepPlaying)
        {
            MakePlayerWin(_secondSamurai.Name, _numberTwoAsString);
        }
        else if (!_canSecondTeamKeepPlaying)
        {
            MakePlayerWin(_firstSamurai.Name, _numberOneAsString);
        }
    }

    private void PlaySecondPlayerTurn()
    {
            PlayPlayerTurn(_secondTeamData,  _firstTeamData.team);
            _canFirstTeamKeepPlaying = _teamController.CanTeamKeepPlaying(_firstTeam);
            if (!_canFirstTeamKeepPlaying)
            {
                MakePlayerWin(_secondSamurai.Name, _numberTwoAsString);
            }

            else if (!_canSecondTeamKeepPlaying)
            {
                MakePlayerWin(_firstSamurai.Name, _numberOneAsString);
                
            }
        
    }

    private void MakePlayerWin(string samuraiName, string playerNumber)
    {
        _view.WriteLine("----------------------------------------");
        _view.WriteLine($"Ganador: {samuraiName} (J{playerNumber})");;
        _canSecondTeamKeepPlaying = false;
        _canFirstTeamKeepPlaying = false;
        _actionExecuted = true;
    }

   

    private void InstantiateValidTeams()
    {
        _firstTeam = _teamController.GetFirstTeam();
        _secondTeam = _teamController.GetSecondTeam();
        _firstSamurai = _teamController.GetSamuraiFromFirstTeam();
        _secondSamurai = _teamController.GetSamuraiFromSecondTeam();
        _firstTeamData = new TeamData(_firstTeam, _firstSamurai, _firstTeamUnitsThatAlreadyPlayed, _numberOneAsString, _teamController);
        _secondTeamData = new TeamData(_secondTeam, _secondSamurai, _secondTeamUnitsThatAlreadyPlayed, _numberTwoAsString, _teamController);
    }

    

    private void PlayPlayerTurn(TeamData teamData, List<Unit> oponentTeam)
    {
        Samurai samurai = teamData.samurai;
        List<Unit> team = teamData.team;
        teamData.teamUnitsThatAlreadyPlayed  = new List<Unit>(); 
        
        _menusController.AnounceRound(samurai.Name, teamData.playerNumber);
        _turnsController.RestartTurns(team, _teamController);
        PlayEachUnitTurn(teamData, oponentTeam);
        

    }

    private void PlayEachUnitTurn(TeamData teamData, List<Unit> oponentTeam)
    {
        while ((_turnsController.CalculateNumberOfFullTurnsLeft()>0|| _turnsController.CalculateNumberOfBlinkingTurns()>0)&& _canFirstTeamKeepPlaying && _canSecondTeamKeepPlaying)
        {
            PlayUnitTurn(teamData, oponentTeam);
        }
    }

    private void PlayUnitTurn(TeamData teamData, List<Unit> oponentTeam)
    {
        List<Unit> unitsInOrderOfAction = _teamController.GetActiveUnitsInOrderOfAction(_teamController.GetActiveUnitsAlive(teamData.team), teamData.teamUnitsThatAlreadyPlayed);
            
        for(int i =0; i < unitsInOrderOfAction.Count;i++)
        {
            unitsInOrderOfAction = _teamController.GetActiveUnitsInOrderOfAction(_teamController.GetActiveUnitsAlive(teamData.team), teamData.teamUnitsThatAlreadyPlayed);
               
            Unit unit = unitsInOrderOfAction[0];    
            if (_canFirstTeamKeepPlaying && _canSecondTeamKeepPlaying && (_turnsController.CalculateNumberOfFullTurnsLeft()>0 || _turnsController.CalculateNumberOfBlinkingTurns()>0))
            {
                DevelopUnitTurn(teamData, unit, oponentTeam); 
            }
                
        } 
    }

    

    private void DevelopUnitTurn(TeamData teamData, Unit unit, List<Unit> oponentTeam)
    {
        
        _actionExecuted = false;
        AnounceInformationForUnitTurn(teamData);
        _turnsController.RestartTurnValuesForUnitTurn();
        while (!_actionExecuted )
        {
            _wasActionAMonsterSummon = false;
            string actionChoosen = SelectAction(unit);
            ExecuteAction(actionChoosen, unit, oponentTeam);
        }
        EvaluateIfActionWasSummonToChangeListOfUnitsThatAlreadyPlayed(teamData, unit);
        
        
    }

    private void EvaluateIfActionWasSummonToChangeListOfUnitsThatAlreadyPlayed(TeamData teamData, Unit unit)
    {
        if (!_wasActionAMonsterSummon)
        {
            teamData.teamUnitsThatAlreadyPlayed.Add(unit);

        }
        else if (_wasActionAMonsterSummon)
        {
            teamData.teamUnitsThatAlreadyPlayed.Remove(unit);
        }
    }

   

    private void AnounceInformationForUnitTurn(TeamData teamData)
    {
        ShowTeams();
        _turnsController.ShowNumberOfTurns();
        _menusController.AnounceUnitsOrderOfAction(teamData, _teamController);
    }

    

    

    private string SelectAction(Unit unit)
    {
        _menusController.ShowActionMenu(unit);
        string actionChoosen = _view.ReadLine();
        return actionChoosen;
    }

    

    private void ShowTeams()
    {
        _view.WriteLine("----------------------------------------");
        _teamController.ShowTeams(_firstTeamData);
        _teamController.ShowTeams(_secondTeamData);
    }

   

    private void ExecuteAction(string actionChoosen, Unit unitAttacking, List<Unit> oponentTeam)
    {
        if (unitAttacking is Samurai)
        {
            ExecuteActionSamurai(actionChoosen, unitAttacking, oponentTeam);
        }

        else
        {
            ExecuteActionMonster(actionChoosen, unitAttacking, oponentTeam);
        }

    }

    private void ExecuteActionSamurai(string actionChoosen,Unit unitAttacking , List<Unit> oponentTeam)
    {
        switch (actionChoosen)
        {
            case "1":
                MakeAttackDamage(oponentTeam, unitAttacking);
                break;
            case "2":
                MakeGunDamage(unitAttacking, oponentTeam);
                break;   
            case "3":
                UseAbility(oponentTeam, unitAttacking);
                break;
            case "4":
                SummonForSamurai(unitAttacking);
                break;
            case "5":
                PassTurn();
                break;
            case "6":
                Surrender(unitAttacking);
                break;
        }
        CheckIfTheOponentTeamCanKeepPlaying(oponentTeam);
    }
    
    private void ExecuteActionMonster(string actionChoosen, Unit unitAttacking, List<Unit> oponentTeam)
    {
        switch (actionChoosen)
        {
            case "1":
                MakeAttackDamage(oponentTeam, unitAttacking);
                break;
            case "2":
                UseAbility(oponentTeam, unitAttacking);
                break; 
            case "3":
                SummonForMonster(unitAttacking);
                break;
            case "4":
                PassTurn();
                break;
        }
        CheckIfTheOponentTeamCanKeepPlaying(oponentTeam);
    }

    


    private void MakeAttackDamage(List<Unit> oponentTeam, Unit unitAttacking)
    {

        Unit targetUnit = _menusController.SelectTarget(_teamController.GetActiveUnitsAlive(oponentTeam), unitAttacking);

        if (targetUnit != null)
        {
            _actionExecuted = true;
            _menusController.AnounceAttackDamage(unitAttacking, targetUnit);
            DiscountAttackDamageFromOponent(unitAttacking, targetUnit);
        }
    }
    
   

    

    private void MakeGunDamage(Unit unitAttacking, List<Unit> oponentTeam)
    {
        Unit targetUnit =_menusController.SelectTarget(_teamController.GetActiveUnitsAlive(oponentTeam), unitAttacking);
        if (targetUnit != null)
        {


            _actionExecuted = true;
            AnounceGunDamage(unitAttacking, targetUnit);
            DiscountGunDamage(unitAttacking, targetUnit);
        }
    }

    private void AnounceGunDamage(Unit unitAttacking, Unit targetUnit)
    {
        _view.WriteLine("----------------------------------------");
        _view.WriteLine($"{unitAttacking.Name} dispara a {targetUnit.Name}");
    }

    private TeamData GetWichTeamIsPlaying(Unit unitAttacking)
    {
        if (_firstTeamData.team.Contains(unitAttacking))
        {
            return _firstTeamData;
        }

        return _secondTeamData;
    }

    private void UseAbility(List<Unit> oponentTeam, Unit unitAttacking)
    {
        TeamData teamData = GetWichTeamIsPlaying(unitAttacking);
        SkillData ability = SelectAbility(unitAttacking);
        if (ability != null)
        {
            EffectsSetter effectsSetter = new EffectsSetter(ability, unitAttacking, _view, _turnsController, teamData, _summonController);
            SkillController skillController = effectsSetter.SetEffectsForSkill();
            skillController.ApplySkillEffects(oponentTeam);
            if(skillController.WasSkillApplied())
            {
                _turnsController.AnounceTurnsState();
                teamData.abilitiesUsedCounter += 1;
                _actionExecuted = true;
            }
            
        }
        CheckIfTeamsCanKeepPlaying();
    }
    
    private void CheckIfTeamsCanKeepPlaying()
    {
        _canFirstTeamKeepPlaying= _teamController.CanTeamKeepPlaying(_firstTeamData.team);
        _canSecondTeamKeepPlaying = _teamController.CanTeamKeepPlaying(_secondTeamData.team);
    }

    private void Surrender(Unit unitAttacking)
    {
        _actionExecuted = true;
        if (_firstTeamData.team.Contains(unitAttacking))
        {
            _menusController.AnounceSurrender(_firstSamurai.Name, _numberOneAsString);
            _canFirstTeamKeepPlaying = false;
        }
        else
        {
            _menusController.AnounceSurrender(_secondSamurai.Name, _numberTwoAsString);
            _canSecondTeamKeepPlaying = false;
        } 
        
        _turnsController.ChangeTurnStateForNeutralOrResistAffinity();
    }

    

    private void PassTurn()
    {
        _turnsController.ChangeTurnsStateWhenPassOrSummon();
        _turnsController.AnounceTurnsState();
        _actionExecuted = true;
    }
    
    
  
    
    private void SummonForSamurai(Unit unitAttacking)
    {
        TeamData teamData = GetWichTeamIsPlaying(unitAttacking);
        _summonController.SummonForSamurai(unitAttacking, teamData, () => _actionExecuted = true);

    }

    private void SummonForMonster(Unit unitAttacking)
    {
        TeamData teamData = GetWichTeamIsPlaying(unitAttacking);
        _summonController.SummonForMonster(unitAttacking, teamData, () => _actionExecuted = true, monster => teamData.teamUnitsThatAlreadyPlayed.Add(monster));
        _wasActionAMonsterSummon = true;
    }
    
    


    
    

    private void CheckIfTheOponentTeamCanKeepPlaying(List<Unit> oponentTeam)
    {
        if (oponentTeam == _firstTeamData.team)
            _canFirstTeamKeepPlaying = _teamController.CanTeamKeepPlaying(_firstTeamData.team);
        else
            _canSecondTeamKeepPlaying = _teamController.CanTeamKeepPlaying(_secondTeamData.team);
    }

    private SkillData SelectAbility(Unit unitAttacking)
    {
        _skillsOptionsCounter = 1;
        ShowSelectAbilityMenu(unitAttacking);
        int choice = int.Parse(_view.ReadLine());

        return GetSelectedSkill(choice, unitAttacking);

    }

    private SkillData GetSelectedSkill(int choice, Unit unitAttacking)
    {
        if (choice != _skillsOptionsCounter)
        {
            SkillData selectedSkillData = unitAttacking.GetUsableSkills()[choice - 1];
            return selectedSkillData;
        }
        return null;

    }

    private void ShowSelectAbilityMenu(Unit unitAttacking)
    {
        _view.WriteLine("----------------------------------------");
        _view.WriteLine($"Seleccione una habilidad para que {unitAttacking.Name} use");
        ShowUsableSkills(unitAttacking);
        _view.WriteLine($"{_skillsOptionsCounter}-Cancelar");
    }

    private void ShowUsableSkills(Unit unitAttacking)
    {
        
        
        foreach (SkillData skill in unitAttacking.GetUsableSkills())
        {
            _view.WriteLine($"{_skillsOptionsCounter}-{skill.name} MP:{skill.cost}");
            _skillsOptionsCounter++;
        }

    }

   
    
    private void DiscountGunDamage(Unit unitAttacking, Unit targetUnit)
    {
        
        double damage = CalculateGunDamage(unitAttacking);
        AffinitiesController affinitiesController = new AffinitiesController("Gun", damage, targetUnit, unitAttacking, _view, _turnsController);
        int damageWithAffinities = affinitiesController.ApplyAffinity();
        targetUnit.DiscountHp(damageWithAffinities);
        if (!affinitiesController.IsReturnDamageAffinity())
        {
            _menusController.ShowEffectOfDamage(unitAttacking, targetUnit, damageWithAffinities);

        }
        
        else
        {
            _menusController.AnounceHPFinalState(unitAttacking);
        }
        _turnsController.AnounceTurnsState();
    }

   

    


    private void DiscountAttackDamageFromOponent(Unit unitAttacking, Unit targetUnit)
    {
        double damage = CalculateAttackDamage(unitAttacking);
        AffinitiesController affinitiesController = new AffinitiesController("Phys", damage, targetUnit, unitAttacking, _view, _turnsController);
        int damageWithAffinities = affinitiesController.ApplyAffinity();
        targetUnit.DiscountHp(damageWithAffinities);
        if (!affinitiesController.IsReturnDamageAffinity())
        {
            _menusController.ShowEffectOfDamage(unitAttacking, targetUnit, damageWithAffinities);

        }
        else
        {
          
            _view.WriteLine($"{unitAttacking.Name} termina con HP:{unitAttacking.HP}/{unitAttacking.maxHP}");;

        }
        _turnsController.AnounceTurnsState();

        
        
    }



    private double CalculateAttackDamage(Unit unitAttacking)
    {
        double damagePonderator = 0.0114;
        double attackDamagePonderator = 54;
        double originalDamage = unitAttacking.Strength * damagePonderator * attackDamagePonderator;
       

        return originalDamage;

    }
    
    private double CalculateGunDamage(Unit unitAttacking)
    {
        double damagePonderator = 0.0114;
        double gunDamagePonderator = 80;
        double originalDamage = unitAttacking.Skill * damagePonderator  * gunDamagePonderator;

        return originalDamage;

    }
 

    
}