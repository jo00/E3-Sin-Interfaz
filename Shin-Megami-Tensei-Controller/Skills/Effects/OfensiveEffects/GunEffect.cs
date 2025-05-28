using Shin_Megami_Tensei_View;
using Shin_Megami_Tensei.Skills.Effects.OfensiveEffects;

namespace Shin_Megami_Tensei.Skills.Effects;

public class GunEffect:OffensiveGunEffect
{
    private UnitData _unitDataAttacking;
    private int _skillPower;
    private ImplementedConsoleView _view;
    private bool _wasEffectApplied = true;
    
    public GunEffect(UnitData unitDataAttacking, int skillPower, ImplementedConsoleView view) : base(unitDataAttacking)
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
            _view.ShowGunTarget(_unitDataAttacking, target);
            double damage = (Math.Sqrt(_unitDataAttacking.Skill * _skillPower));
        
            AffinitiesController affinitiesController = new AffinitiesController("Gun", damage, target, _unitDataAttacking, _view, turnsController,1);
            int damageWithAffinities = (int)affinitiesController.ApplyAffinity();
            target.DiscountHp(damageWithAffinities);
            if (!affinitiesController.IsReturnDamageAffinity())
            {
                menuController.ShowEffectOfDamage(_unitDataAttacking, target, damageWithAffinities);

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