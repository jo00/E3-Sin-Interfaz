using Shin_Megami_Tensei_View;
using Shin_Megami_Tensei.Skills.Effects.OfensiveEffects;

namespace Shin_Megami_Tensei.Skills.Effects;

public class FireEffect:OffensiveMagicEffect
{
    private UnitData _unitDataAttacking;
    private int _skillPower;
    private ImplementedConsoleView _view;
    private bool _wasEffectApplied = true;

    public FireEffect(UnitData unitDataAttacking, int skillPower, ImplementedConsoleView view) : base(unitDataAttacking)
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
            _view.ShowFireTarget(_unitDataAttacking, target);
            double damage = (Math.Sqrt(_unitDataAttacking.Magic * _skillPower));
        
            AffinitiesController affinitiesController = new AffinitiesController("Fire", damage, target, _unitDataAttacking, _view, turnsController);
            double damageWithAffinities = affinitiesController.ApplyAffinity();
            if (_unitDataAttacking.incrementMagic)
            {
                damageWithAffinities = (int)(damageWithAffinities * 2.5);
            }
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
            _wasEffectApplied= false;
        }

    
    }

    public override bool WasEffectApplied()
    {
        return _wasEffectApplied;
    }
}