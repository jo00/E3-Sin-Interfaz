using Shin_Megami_Tensei_View;
using Shin_Megami_Tensei.Configs;

namespace Shin_Megami_Tensei.Skills.Effects;

public class HealEffect:Effect
{
    private UnitData _unitDataAttacking;
    private TeamData _teamData;
    private int _power;
    private TurnsController _turnsController;
    private bool _wasEffectApplied = true;
    private ImplementedConsoleView _view;
    private TeamController _teamController;
    public HealEffect(UnitData unitDataAttacking, TeamData teamData,  int power, TurnsController turnsController, ImplementedConsoleView view, TeamController teamController) : base(unitDataAttacking)
    {
        _teamController = teamController;
        _view = view;
        _unitDataAttacking = unitDataAttacking;
        _teamData = teamData;
        _power = power;
        _turnsController = turnsController;
    }

    public override void Apply(List<UnitData> oponentUnits, TurnsController turnsController)
    {
        MenusController menusController = new MenusController(_view);
        UnitData allyTarget = menusController.GetAllyTarget(_unitDataAttacking, _teamData, _teamController);
        if (allyTarget != null)
        {
            
            int healAmount = (int)(allyTarget.maxHP * _power / 100);
            allyTarget.HP = Math.Min(allyTarget.maxHP, allyTarget.HP + healAmount);
            _view.AnounceHealAllyTarget(_unitDataAttacking, allyTarget);
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