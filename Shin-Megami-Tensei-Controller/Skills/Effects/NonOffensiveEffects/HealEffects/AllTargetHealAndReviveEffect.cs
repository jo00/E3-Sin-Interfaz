using Shin_Megami_Tensei_View;
using Shin_Megami_Tensei.Configs;

namespace Shin_Megami_Tensei.Skills.Effects.NonOffensiveEffects.HealEffects;

public class AllTargetHealAndReviveEffect:Effect

{
    private UnitData _unitDataAttacking;
    private int _skillPower;
    private ImplementedConsoleView _view;
    private bool _wasEffectApplied = true;
    private TurnsController _turnsController;
    private TeamController _teamController;
    private bool _wasAttackUnitAttacked = false;
    private List<string> _affinitiesApplied = new List<string>();
    private TeamData _teamData;
    
    public AllTargetHealAndReviveEffect(UnitData unitDataAttacking, int skillPower, ImplementedConsoleView view, TeamController teamController, TeamData teamData) : base(unitDataAttacking)
    {
        _unitDataAttacking = unitDataAttacking;
        _skillPower = skillPower;
        _view = view;
        _teamController = teamController;
        _teamData = teamData;
    }
    
    public override void Apply(List<UnitData> oponentUnits, TurnsController turnsController)
    {
        _view.ShowLines();
        _turnsController= turnsController;
        List<UnitData> activeUnitsAlive = _teamController.GetActiveUnitsAlive(_teamData.team);
        MenusController menuController = new MenusController(_view);
        foreach (UnitData allyTarget in activeUnitsAlive)
        {
            if ( allyTarget!=_unitDataAttacking)

            {
                Console.WriteLine("1");

                int healAmount = (int)(allyTarget.maxHP * _skillPower / 100);
                allyTarget.HP = Math.Min(allyTarget.maxHP, allyTarget.HP + healAmount);
                _view.AnounceHealAllyTargetWithoutLines(_unitDataAttacking, allyTarget);
                _view.AnounceHealResult(healAmount, allyTarget);
            }
            
        }

        foreach (UnitData allyCopy in _teamData.originalTeamOrder)
        {
            UnitData ally = GetUnitAccordingToOriginalOrder(allyCopy);
            if (allyCopy.HP<=0 || allyCopy.active==false)
            {
                

                if (ally.HP <= 0)
                {
                    
                    _view.AnounceReviveAllyTargetWithoutLines(_unitDataAttacking, ally);
                }
                else
                {
                    _view.AnounceHealAllyTargetWithoutLines(_unitDataAttacking, ally);

                }
                int healAmount = (int)(ally.maxHP * (_skillPower / 100.0));
                ally.HP = Math.Min(ally.maxHP, ally.HP + healAmount);
                ally.active = false;
                
                _view.AnounceHealResult(healAmount, ally);

            
                
            }
        }
        
        _unitDataAttacking.HP = 0;
        _unitDataAttacking.active = false;
        _view.AnounceHPFinalStateForUnit(_unitDataAttacking);
        
        
        _turnsController.ChangeTurnsForNonOffensiveAbilities();

        
    }

    public override bool WasEffectApplied()
    {
        return true;
    }

    private UnitData GetUnitAccordingToOriginalOrder(UnitData unitCopy)
    {
        foreach (UnitData unit in _teamData.team)
        {
            if (unit.Name == unitCopy.Name)
            {
                return unit;
            }
        }

        return null;
    }
}