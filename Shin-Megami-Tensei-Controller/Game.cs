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
    private List<UnitData> _firstTeam;
    private List<UnitData> _firstTeamUnitsThatAlreadyPlayed = new List<UnitData>();
    private List<UnitData> _secondTeamUnitsThatAlreadyPlayed = new List<UnitData>();

    private List<UnitData> _secondTeam;
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
    
    private UnitData _monsterToGetOut;
    private UnitData _monsterToSummon;
    private SummonController _summonController;
    
    private ImplementedConsoleView _implementedConsoleView;
    
   


    public Game(View view, string teamsFolder)
    {
        _view = view;
        _implementedConsoleView = new ImplementedConsoleView(_view);
        _loader = new DataLoader();
        _teamsFolder = teamsFolder;
        _turnsController = new TurnsController(_implementedConsoleView);
        _menusController = new MenusController(_implementedConsoleView);

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
        _teamController = new TeamController(_implementedConsoleView, _samurais, _monsters, _skills);
        _teamController.SelectTeam(teamsFolder);
        _summonController = new SummonController(_implementedConsoleView, _teamController, _turnsController, _menusController);

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

    private void ResetSpeedForOrderValues(List<UnitData> team)
    {
        foreach (UnitData unit in team)
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
        _implementedConsoleView.AnounceThatPlayerWon(samuraiName, playerNumber);
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
        _firstTeamData = new TeamData(_firstTeam, _firstSamurai, _firstTeamUnitsThatAlreadyPlayed, _numberOneAsString);
        _secondTeamData = new TeamData(_secondTeam, _secondSamurai, _secondTeamUnitsThatAlreadyPlayed, _numberTwoAsString);
    }

    

    private void PlayPlayerTurn(TeamData teamData, List<UnitData> oponentTeam)
    {
        Samurai samurai = teamData.samurai;
        List<UnitData> team = teamData.team;
        teamData.teamUnitsThatAlreadyPlayed  = new List<UnitData>(); 
        
        _implementedConsoleView.AnounceRound(samurai.Name, teamData.playerNumber);
        _turnsController.RestartTurns(team, _teamController);
        PlayEachUnitTurn(teamData, oponentTeam);
        

    }

    private void PlayEachUnitTurn(TeamData teamData, List<UnitData> oponentTeam)
    {
        while ((_turnsController.CalculateNumberOfFullTurnsLeft()>0|| _turnsController.CalculateNumberOfBlinkingTurns()>0)&& _canFirstTeamKeepPlaying && _canSecondTeamKeepPlaying)
        {
            PlayUnitTurn(teamData, oponentTeam);
        }
    }

    private void PlayUnitTurn(TeamData teamData, List<UnitData> oponentTeam)
    {
        List<UnitData> unitsInOrderOfAction = _teamController.GetActiveUnitsInOrderOfAction(_teamController.GetActiveUnitsAlive(teamData.team), teamData.teamUnitsThatAlreadyPlayed);
            
        for(int i =0; i < unitsInOrderOfAction.Count;i++)
        {
            unitsInOrderOfAction = _teamController.GetActiveUnitsInOrderOfAction(_teamController.GetActiveUnitsAlive(teamData.team), teamData.teamUnitsThatAlreadyPlayed);
               
            UnitData unitData = unitsInOrderOfAction[0];    
            if (_canFirstTeamKeepPlaying && _canSecondTeamKeepPlaying && (_turnsController.CalculateNumberOfFullTurnsLeft()>0 || _turnsController.CalculateNumberOfBlinkingTurns()>0))
            {
                DevelopUnitTurn(teamData, unitData, oponentTeam); 
            }
                
        } 
    }

    

    private void DevelopUnitTurn(TeamData teamData, UnitData unitData, List<UnitData> oponentTeam)
    {
        
        _actionExecuted = false;
        AnounceInformationForUnitTurn(teamData);
        _turnsController.RestartTurnValuesForUnitTurn();
        while (!_actionExecuted )
        {
            _wasActionAMonsterSummon = false;
            int actionChoosen = SelectAction(unitData);
            ExecuteAction(actionChoosen, unitData, oponentTeam);
        }
        EvaluateIfActionWasSummonToChangeListOfUnitsThatAlreadyPlayed(teamData, unitData);
        
        
    }

    private void EvaluateIfActionWasSummonToChangeListOfUnitsThatAlreadyPlayed(TeamData teamData, UnitData unitData)
    {
        if (!_wasActionAMonsterSummon)
        {
            teamData.teamUnitsThatAlreadyPlayed.Add(unitData);

        }
        else if (_wasActionAMonsterSummon)
        {
            teamData.teamUnitsThatAlreadyPlayed.Remove(unitData);
        }
    }

   

    private void AnounceInformationForUnitTurn(TeamData teamData)
    {
        ShowTeams();
        _turnsController.ShowNumberOfTurns();
        _menusController.AnounceUnitsOrderOfAction(teamData, _teamController);
    }

    

    

    private int SelectAction(UnitData unitData)
    {
        _implementedConsoleView.ShowActionMenu(unitData);
        int actionChoosen = _implementedConsoleView.GetNumericSelectedOptionFromUser();
        return actionChoosen;
    }

    

    private void ShowTeams()
    {
        List<string> firstTeamLines = _teamController.GetTeamDisplayLines(_firstTeamData);
        List<string> secondTeamLines = _teamController.GetTeamDisplayLines(_secondTeamData);
        _implementedConsoleView.ShowBothTeams(firstTeamLines,secondTeamLines);
    }

   

    private void ExecuteAction(int actionChoosen, UnitData unitDataAttacking, List<UnitData> oponentTeam)
    {
        if (unitDataAttacking is Samurai)
        {
            ExecuteActionSamurai(actionChoosen, unitDataAttacking, oponentTeam);
        }

        else
        {
            ExecuteActionMonster(actionChoosen, unitDataAttacking, oponentTeam);
        }

    }

    private void ExecuteActionSamurai(int actionChoosen,UnitData unitDataAttacking , List<UnitData> oponentTeam)
    {
        switch (actionChoosen)
        {
            case 1:
                MakeAttackDamage(oponentTeam, unitDataAttacking);
                break;
            case 2:
                MakeGunDamage(unitDataAttacking, oponentTeam);
                break;   
            case 3:
                UseAbility(oponentTeam, unitDataAttacking);
                break;
            case 4:
                SummonForSamurai(unitDataAttacking);
                break;
            case 5:
                PassTurn();
                break;
            case 6:
                Surrender(unitDataAttacking);
                break;
        }
        CheckIfTheOponentTeamCanKeepPlaying(oponentTeam);
    }
    
    private void ExecuteActionMonster(int actionChoosen, UnitData unitDataAttacking, List<UnitData> oponentTeam)
    {
        switch (actionChoosen)
        {
            case 1:
                MakeAttackDamage(oponentTeam, unitDataAttacking);
                break;
            case 2:
                UseAbility(oponentTeam, unitDataAttacking);
                break; 
            case 3:
                SummonForMonster(unitDataAttacking);
                break;
            case 4:
                PassTurn();
                break;
        }
        CheckIfTheOponentTeamCanKeepPlaying(oponentTeam);
    }

    


    private void MakeAttackDamage(List<UnitData> oponentTeam, UnitData unitDataAttacking)
    {

        UnitData targetUnitData = _menusController.SelectTarget(_teamController.GetActiveUnitsAlive(oponentTeam), unitDataAttacking);

        if (targetUnitData != null)
        {
            _actionExecuted = true;
            _implementedConsoleView.AnounceAttackDamage(unitDataAttacking, targetUnitData);
            DiscountAttackDamageFromOponent(unitDataAttacking, targetUnitData);
            if (unitDataAttacking.shouldDiscountAttackAfterApplyingIt )
            {
                unitDataAttacking.shouldDiscountAttackAfterApplyingIt  = false;
                unitDataAttacking.Strength =unitDataAttacking.Strength/ 2.5;
                unitDataAttacking.Skill = unitDataAttacking.Skill / 2.5;
            }
        }
        CheckIfTeamsCanKeepPlaying();
    }
    
   

    

    private void MakeGunDamage(UnitData unitDataAttacking, List<UnitData> oponentTeam)
    {
        UnitData targetUnitData =_menusController.SelectTarget(_teamController.GetActiveUnitsAlive(oponentTeam), unitDataAttacking);
        if (targetUnitData != null)
        {


            _actionExecuted = true;
            _implementedConsoleView.AnounceGunDamage(unitDataAttacking, targetUnitData);
            DiscountGunDamage(unitDataAttacking, targetUnitData);
            if (unitDataAttacking.shouldDiscountAttackAfterApplyingIt )
            {
                unitDataAttacking.shouldDiscountAttackAfterApplyingIt  = false;
                unitDataAttacking.Strength =unitDataAttacking.Strength/ 2.5;
                unitDataAttacking.Skill = unitDataAttacking.Skill / 2.5;
            }
        }
        CheckIfTeamsCanKeepPlaying();
    }
    

    private TeamData GetWichTeamIsPlaying(UnitData unitDataAttacking)
    {
        if (_firstTeamData.team.Contains(unitDataAttacking))
        {
            return _firstTeamData;
        }

        return _secondTeamData;
    }

    private void UseAbility(List<UnitData> oponentTeam, UnitData unitDataAttacking)
    {
        TeamData teamData = GetWichTeamIsPlaying(unitDataAttacking);
        SkillData ability = SelectAbility(unitDataAttacking);
        if (ability != null)
        {
            EffectsSetter effectsSetter = new EffectsSetter(ability, unitDataAttacking, _implementedConsoleView, _turnsController, teamData, _summonController, _teamController);
            SkillController skillController = effectsSetter.SetEffectsForSkill();
            skillController.ApplySkillEffects(oponentTeam);
            if(skillController.WasSkillApplied())
            {
                _turnsController.AnounceTurnsState();
                teamData.abilitiesUsedCounter += 1;
                _actionExecuted = true;
            }

            if (skillController.WasEffectCharge())
            {
                unitDataAttacking.shouldDiscountAttackAfterApplyingIt = true;
            }
            
            

            if (skillController.WasEffectOffensiveMagic() && unitDataAttacking.incrementMagic)
            {
                unitDataAttacking.incrementMagic = false;
            }
            if(skillController.WasEffectOffensivePhysOrGun() && unitDataAttacking.shouldDiscountAttackAfterApplyingIt)
            {
                unitDataAttacking.shouldDiscountAttackAfterApplyingIt = false;
                unitDataAttacking.Strength = unitDataAttacking.Strength / 2.5;
                unitDataAttacking.Skill = unitDataAttacking.Skill / 2.5;
            }
        }
        CheckIfTeamsCanKeepPlaying();
    }
    
    private void CheckIfTeamsCanKeepPlaying()
    {
        _canFirstTeamKeepPlaying= _teamController.CanTeamKeepPlaying(_firstTeamData.team);
        _canSecondTeamKeepPlaying = _teamController.CanTeamKeepPlaying(_secondTeamData.team);
    }

    private void Surrender(UnitData unitDataAttacking)
    {
        _actionExecuted = true;
        if (_firstTeamData.team.Contains(unitDataAttacking))
        {
            _implementedConsoleView.AnounceSurrender(_firstSamurai.Name, _numberOneAsString);
            _canFirstTeamKeepPlaying = false;
        }
        else
        {
            _implementedConsoleView.AnounceSurrender(_secondSamurai.Name, _numberTwoAsString);
            _canSecondTeamKeepPlaying = false;
        } 
        
        _turnsController.ChangeTurnStateForMissNeutralOrResistAffinity();
    }

    

    private void PassTurn()
    {
        _turnsController.ChangeTurnsStateWhenPassOrSummon();
        _turnsController.AnounceTurnsState();
        _actionExecuted = true;
    }
    
    
  
    
    private void SummonForSamurai(UnitData unitDataAttacking)
    {
        TeamData teamData = GetWichTeamIsPlaying(unitDataAttacking);
        _summonController.SummonForSamurai(unitDataAttacking, teamData, () => _actionExecuted = true);

    }

    private void SummonForMonster(UnitData unitDataAttacking)
    {
        TeamData teamData = GetWichTeamIsPlaying(unitDataAttacking);
        _summonController.SummonForMonster(unitDataAttacking, teamData, () => _actionExecuted = true, monster => teamData.teamUnitsThatAlreadyPlayed.Add(monster));
        _wasActionAMonsterSummon = true;
    }
    
    


    
    

    private void CheckIfTheOponentTeamCanKeepPlaying(List<UnitData> oponentTeam)
    {
        if (oponentTeam == _firstTeamData.team)
            _canFirstTeamKeepPlaying = _teamController.CanTeamKeepPlaying(_firstTeamData.team);
        else
            _canSecondTeamKeepPlaying = _teamController.CanTeamKeepPlaying(_secondTeamData.team);
    }

    private SkillData SelectAbility(UnitData unitDataAttacking)
    {
        _implementedConsoleView.ShowSelectAbilityMenu(unitDataAttacking);
        int choice = _implementedConsoleView.GetNumericSelectedOptionFromUser();

        return GetSelectedSkill(choice, unitDataAttacking);

    }

    private SkillData GetSelectedSkill(int choice, UnitData unitDataAttacking)
    {
        _skillsOptionsCounter=unitDataAttacking.GetUsableSkills().Count + 1; 
        if (choice != _skillsOptionsCounter)
        {
            SkillData selectedSkillData = unitDataAttacking.GetUsableSkills()[choice - 1];
            return selectedSkillData;
        }
        return null;

    }

    

  

   
    
    private void DiscountGunDamage(UnitData unitDataAttacking, UnitData targetUnitData)
    {
        
        double damage = CalculateGunDamage(unitDataAttacking);
        AffinitiesController affinitiesController = new AffinitiesController("Gun", damage, targetUnitData, unitDataAttacking, _implementedConsoleView, _turnsController,1);
        int damageWithAffinities = (int)affinitiesController.ApplyAffinity();
        targetUnitData.DiscountHp(damageWithAffinities);
        if (!affinitiesController.IsReturnDamageAffinity())
        {
            _menusController.ShowEffectOfDamage(unitDataAttacking, targetUnitData, damageWithAffinities);

        }
        
        else
        {
            _implementedConsoleView.AnounceHPFinalStateForUnit(unitDataAttacking);
        }
        _turnsController.AnounceTurnsState();
    }

   

    


    private void DiscountAttackDamageFromOponent(UnitData unitDataAttacking, UnitData targetUnitData)
    {
        double damage = CalculateAttackDamage(unitDataAttacking);
        AffinitiesController affinitiesController = new AffinitiesController("Phys", damage, targetUnitData, unitDataAttacking, _implementedConsoleView, _turnsController,1);
        int damageWithAffinities = (int)affinitiesController.ApplyAffinity();
        targetUnitData.DiscountHp(damageWithAffinities);
        if (!affinitiesController.IsReturnDamageAffinity())
        {
            _menusController.ShowEffectOfDamage(unitDataAttacking, targetUnitData, damageWithAffinities);

        }
        else
        {
            _implementedConsoleView.AnounceHPFinalStateForUnit(unitDataAttacking);
            
        }
        _turnsController.AnounceTurnsState();

        
        
    }



    private double CalculateAttackDamage(UnitData unitDataAttacking)
    {
        double damagePonderator = 0.0114;
        double attackDamagePonderator = 54;
        double originalDamage = unitDataAttacking.Strength * damagePonderator * attackDamagePonderator;
       

        return originalDamage;

    }
    
    private double CalculateGunDamage(UnitData unitDataAttacking)
    {
        double damagePonderator = 0.0114;
        double gunDamagePonderator = 80;
        double originalDamage = unitDataAttacking.Skill * damagePonderator  * gunDamagePonderator;

        return originalDamage;

    }
 

    
}