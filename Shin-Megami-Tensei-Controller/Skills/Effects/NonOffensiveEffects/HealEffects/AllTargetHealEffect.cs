using Shin_Megami_Tensei_View;
using Shin_Megami_Tensei.Configs;

namespace Shin_Megami_Tensei.Skills.Effects.NonOffensiveEffects.HealEffects;

public class AllTargetHealEffect:Effect

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
    
    public AllTargetHealEffect(UnitData unitDataAttacking, int skillPower, ImplementedConsoleView view, TeamController teamController, TeamData teamData) : base(unitDataAttacking)
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
            if (allyTarget != null && allyTarget!=_unitDataAttacking)

            {
                int healAmount = (int)(allyTarget.maxHP * _skillPower / 100);
                allyTarget.HP = Math.Min(allyTarget.maxHP, allyTarget.HP + healAmount);
                _view.AnounceHealAllyTargetWithoutLines(_unitDataAttacking, allyTarget);
                _view.ShowHealResult(healAmount, allyTarget);
            }
            else
            {
                _wasEffectApplied = false;

            }
        }
        int healAmountForSelf = (int)(_unitDataAttacking.maxHP * _skillPower / 100);
        _unitDataAttacking.HP = Math.Min(_unitDataAttacking.maxHP, _unitDataAttacking.HP + healAmountForSelf);
        _view.AnounceHealAllyTargetWithoutLines(_unitDataAttacking, _unitDataAttacking);
        _view.ShowHealResult(healAmountForSelf, _unitDataAttacking);
        
        _turnsController.ChangeTurnsForNonOffensiveAbilities();

        
    }

    public override bool WasEffectApplied()
    {
        return true;
    }
}