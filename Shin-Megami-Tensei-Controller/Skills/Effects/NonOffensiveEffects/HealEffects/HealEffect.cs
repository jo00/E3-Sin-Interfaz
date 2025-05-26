using Shin_Megami_Tensei_View;
using Shin_Megami_Tensei.Configs;

namespace Shin_Megami_Tensei.Skills.Effects;

public class HealEffect:Effect
{
    private UnitData _unitDataAttacking;
    private TeamData _teamData;
    private View _view;
    private int _power;
    private TurnsController _turnsController;
    private bool _wasEffectApplied = true;
    public HealEffect(UnitData unitDataAttacking, TeamData teamData, View view,  int power, TurnsController turnsController) : base(unitDataAttacking)
    {
        _unitDataAttacking = unitDataAttacking;
        _teamData = teamData;
        _view = view;
        _power = power;
        _turnsController = turnsController;
    }

    public override void Apply(List<UnitData> oponentUnits, TurnsController turnsController)
    {
        MenusController menusController = new MenusController(_view);
        UnitData allyTarget = menusController.GetAllyTarget(_unitDataAttacking, _teamData);
        if (allyTarget != null)
        {
            
            int healAmount = (int)(allyTarget.maxHP * _power / 100);
            allyTarget.HP = Math.Min(allyTarget.maxHP, allyTarget.HP + healAmount);
            menusController.ShowHealAllyTarget(_unitDataAttacking, allyTarget);
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