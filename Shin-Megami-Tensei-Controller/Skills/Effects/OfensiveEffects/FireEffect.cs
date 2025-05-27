using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei.Skills.Effects;

public class FireEffect:Effect
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
            int damageWithAffinities = affinitiesController.ApplyAffinity();
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
            _wasEffectApplied= false;
        }

    
    }

    public override bool WasEffectApplied()
    {
        return _wasEffectApplied;
    }
}