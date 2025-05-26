using Shin_Megami_Tensei_View;
using Shin_Megami_Tensei.Configs;

namespace Shin_Megami_Tensei.Skills.Effects;

public class HealEffect:Effect
{
    private Unit _unitAttacking;
    private TeamData _teamData;
    private View _view;
    private int _power;
    private TurnsController _turnsController;
    private bool _wasEffectApplied = true;
    public HealEffect(Unit unitAttacking, TeamData teamData, View view,  int power, TurnsController turnsController) : base(unitAttacking)
    {
        _unitAttacking = unitAttacking;
        _teamData = teamData;
        _view = view;
        _power = power;
        _turnsController = turnsController;
    }

    public override void Apply(List<Unit> oponentUnits, TurnsController turnsController)
    {
        MenusController menusController = new MenusController(_view);
        Unit allyTarget = menusController.GetAllyTarget(_unitAttacking, _teamData);
        if (allyTarget != null)
        {
            
            int healAmount = (int)(allyTarget.maxHP * _power / 100);
            allyTarget.HP = Math.Min(allyTarget.maxHP, allyTarget.HP + healAmount);
            menusController.ShowHealAllyTarget(_unitAttacking, allyTarget);
            menusController.ShowHealResult(healAmount, allyTarget);
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