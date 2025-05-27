using Shin_Megami_Tensei_View;
using Shin_Megami_Tensei.Configs;

namespace Shin_Megami_Tensei.Skills.Effects.OfensiveEffects;

public class ReviveEffect:Effect
{
    private UnitData _unitDataAttacking;
    private TeamData _teamData;
    private int _power;
    private TurnsController _turnsController;
    private bool _wasEffectApplied;
    private ImplementedConsoleView _view;

    public ReviveEffect(UnitData unitDataAttacking, TeamData teamData,  int power, TurnsController turnsController, ImplementedConsoleView view) : base(unitDataAttacking)
    {
        _unitDataAttacking = unitDataAttacking;
        _teamData = teamData;
        _view= view;
        _power = power;
        _turnsController = turnsController;
        _wasEffectApplied = true;
    }
    public override void Apply(List<UnitData> oponentUnits, TurnsController turnsController)
    {
        MenusController menusController = new MenusController(_view);
        UnitData allyTarget = menusController.GetDeadAllyTarget(_unitDataAttacking, _teamData);
        if (allyTarget != null)
        {
            
            int healAmount = (int)(allyTarget.maxHP * (_power / 100.0));
            allyTarget.HP = Math.Min(allyTarget.maxHP, allyTarget.HP + healAmount);
            allyTarget.active = false;
            _view.ShowReviveAllyTarget(_unitDataAttacking, allyTarget);
            _view.ShowHealResult(healAmount, allyTarget);
            _turnsController.ChangeTurnsForNonOffensiveAbilities();  
        }
        else
        {
            _wasEffectApplied = false;
        }
        
    }

    public override bool WasEffectApplied()
    {
        return _wasEffectApplied;
    }
}