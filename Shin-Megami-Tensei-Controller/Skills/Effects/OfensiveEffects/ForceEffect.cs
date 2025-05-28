using Shin_Megami_Tensei_View;
using Shin_Megami_Tensei.Skills.Effects.OfensiveEffects;

namespace Shin_Megami_Tensei.Skills.Effects;

public class ForceEffect:OffensiveMagicEffect
{
    private UnitData _unitDataAttacking;
    private int _skillPower;
    private ImplementedConsoleView _view;
    private bool _wasEffectApplied = true;
    
    public ForceEffect(UnitData unitDataAttacking, int skillPower, ImplementedConsoleView view) : base(unitDataAttacking)
    {
        _unitDataAttacking = unitDataAttacking;
        _skillPower = skillPower;
        _view = view;
    }

    public override void Apply(List<UnitData> oponentUnits, TurnsController turnsController)
    {
        MenusController menuController = new MenusController(_view);
        UnitData target = menuController.SelectTarget(oponentUnits, _unitDataAttacking);
        if (target != null)
        {
            _view.ShowForceTarget(_unitDataAttacking, target);
            double damage = (Math.Sqrt(_unitDataAttacking.Magic * _skillPower));
            double increment = 1.0;
            if (_unitDataAttacking.incrementMagic)
            {
                increment=2.5;
            }
            AffinitiesController affinitiesController = new AffinitiesController("Force", damage, target, _unitDataAttacking, _view, turnsController, increment);
            double damageWithAffinities = affinitiesController.ApplyAffinity();
            
            target.DiscountHp((int)damageWithAffinities);
            if (!affinitiesController.IsReturnDamageAffinity())
            {
                menuController.ShowEffectOfDamage(_unitDataAttacking, target, (int)damageWithAffinities);

            }
        
            else
            {
                _view.AnounceHPFinalStateForUnit(_unitDataAttacking);

            }  
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