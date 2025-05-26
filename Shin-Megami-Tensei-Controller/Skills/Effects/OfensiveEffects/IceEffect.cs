using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei.Skills.Effects;

public class IceEffect:Effect
{
    private Unit _unitAttacking;
    private int _skillPower;
    private View _view;
    private bool _wasEffectApplied = true;
    
    
    public IceEffect(Unit unitAttacking, int skillPower, View view) : base(unitAttacking)
    {
        _unitAttacking = unitAttacking;
        _skillPower = skillPower;
        _view = view;
    }

    public override void Apply(List<Unit> oponentUnits, TurnsController turnsController)
    {
        MenusController menuController = new MenusController(_view);
        Unit target = menuController.SelectTarget(oponentUnits, _unitAttacking);
        if (target != null)
        {
            menuController.ShowIceTarget(_unitAttacking, target);
            double damage = (Math.Sqrt(_unitAttacking.Magic * _skillPower));
        
            AffinitiesController affinitiesController = new AffinitiesController("Ice", damage, target, _unitAttacking, _view, turnsController);
            int damageWithAffinities = affinitiesController.ApplyAffinity();
            target.DiscountHp(damageWithAffinities);
        
            if (!affinitiesController.IsReturnDamageAffinity())
            {
                menuController.ShowEffectOfDamage(_unitAttacking, target, damageWithAffinities);

            }
        
            else
            {
                _view.WriteLine($"{_unitAttacking.Name} termina con HP:{_unitAttacking.HP}/{_unitAttacking.maxHP}");;

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