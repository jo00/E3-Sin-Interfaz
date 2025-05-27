using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei.Skills.Effects;

public class ElecEffect:Effect
{
    private UnitData _unitDataAttacking;
    private int _skillPower;
    private ImplementedConsoleView _view;
    private bool _wasEffectApplied = true;
    private AffinitiesController _affinitiesController;
    
    public ElecEffect(UnitData unitDataAttacking, int skillPower, ImplementedConsoleView view) : base(unitDataAttacking)
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
            menuController.ShowElecTarget(_unitDataAttacking, target);
            double damage = (Math.Sqrt(_unitDataAttacking.Magic * _skillPower));
            _affinitiesController = new AffinitiesController("Elec", damage, target, _unitDataAttacking, _view, turnsController);

        
            int damageWithAffinities = _affinitiesController.ApplyAffinity();
            target.DiscountHp(damageWithAffinities);
            if (!_affinitiesController.IsReturnDamageAffinity())
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