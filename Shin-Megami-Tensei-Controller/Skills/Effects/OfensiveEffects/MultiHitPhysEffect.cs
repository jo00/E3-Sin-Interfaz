using Shin_Megami_Tensei_View;
using Shin_Megami_Tensei.Skills.Effects;

namespace Shin_Megami_Tensei.Skills;

public class MultiHitPhysEffect : Effect
{
    private readonly int _min;
    private readonly int _max;
    private readonly Unit _unitAttacking;
    private readonly int _skillPower;
    private readonly View _view;
    private int _k;
    private bool _wasEffectApplied = true;

    public MultiHitPhysEffect(Unit unitAttacking, int min, int max, int skillPower, View view, int k) : base(unitAttacking)
    {
        _min = min;
        _max = max;
        _unitAttacking = unitAttacking;
        _skillPower = skillPower;
        _view = view;
        _k = k;
    }

    public override void Apply(List<Unit> oponentUnits, TurnsController turnsController)
    {
    
        MenusController menuController = new MenusController(_view);
        Unit target = menuController.SelectTarget(oponentUnits, _unitAttacking);
        if (target != null)
        {
            int hits = CalculateHits(_k);
            _view.WriteLine("----------------------------------------");
            double damage = Math.Sqrt(_unitAttacking.Strength * _skillPower);

            AffinitiesController affinitiesController = new AffinitiesController("Phys", damage, target, _unitAttacking, _view, turnsController);
            bool isReturnDamageAffinity = false;
            for (int i = 0; i < hits; i++)
            {

                _view.WriteLine($"{_unitAttacking.Name} ataca a {target.Name}");
            
                int damageWithAffinities = affinitiesController.ApplyAffinity();
                target.DiscountHp(damageWithAffinities);
                isReturnDamageAffinity= affinitiesController.IsReturnDamageAffinity();
                if (!isReturnDamageAffinity)
                {

                    if (damageWithAffinities > 0)
                    {
                        _view.WriteLine($"{target.Name} recibe {damageWithAffinities} de daño");

                    }
                
                }
            }
            if (!isReturnDamageAffinity)
            {

                _view.WriteLine($"{target.Name} termina con HP:{target.HP}/{target.maxHP}");

                
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

    private int CalculateHits(int k)
    {
        int range = _max - _min + 1;
        int offset = k % range;
        return _min + offset;
    }
    
    

}
