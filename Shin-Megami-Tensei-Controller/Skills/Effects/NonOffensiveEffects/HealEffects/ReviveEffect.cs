using Shin_Megami_Tensei_View;
using Shin_Megami_Tensei.Configs;

namespace Shin_Megami_Tensei.Skills.Effects.OfensiveEffects;

public class ReviveEffect:Effect
{
    private Unit _unitAttacking;
    private TeamData _teamData;
    private View _view;
    private int _power;
    private TurnsController _turnsController;
    private bool _wasEffectApplied;

    public ReviveEffect(Unit unitAttacking, TeamData teamData, View view,  int power, TurnsController turnsController) : base(unitAttacking)
    {
        _unitAttacking = unitAttacking;
        _teamData = teamData;
        _view = view;
        _power = power;
        _turnsController = turnsController;
        _wasEffectApplied = true;
    }
    public override void Apply(List<Unit> oponentUnits, TurnsController turnsController)
    {
        MenusController menusController = new MenusController(_view);
        Unit allyTarget = menusController.GetDeadAllyTarget(_unitAttacking, _teamData);
        Console.WriteLine("got ally target");
        Console.WriteLine(allyTarget);
        if (allyTarget != null)
        {
            
            int healAmount = (int)(allyTarget.maxHP * (_power / 100.0));
            allyTarget.HP = Math.Min(allyTarget.maxHP, allyTarget.HP + healAmount);
            allyTarget.active = false;
            Console.WriteLine(healAmount);
            menusController.ShowReviveAllyTarget(_unitAttacking, allyTarget);
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