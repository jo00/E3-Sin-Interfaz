using Shin_Megami_Tensei_View;
using Shin_Megami_Tensei.Skills.Effects;

namespace Shin_Megami_Tensei.Skills;

public class MultiHitPhysEffect : Effect
{
    private readonly int _min;
    private readonly int _max;
    private readonly UnitData _unitDataAttacking;
    private readonly int _skillPower;
    private readonly View _view;
    private int _k;
    private bool _wasEffectApplied = true;

    public MultiHitPhysEffect(UnitData unitDataAttacking, int min, int max, int skillPower, View view, int k) : base(unitDataAttacking)
    {
        _min = min;
        _max = max;
        _unitDataAttacking = unitDataAttacking;
        _skillPower = skillPower;
        _view = view;
        _k = k;
    }

    public override void Apply(List<UnitData> oponentUnits, TurnsController turnsController)
    {
    
        MenusController menuController = new MenusController(_view);
        UnitData target = menuController.SelectTarget(oponentUnits, _unitDataAttacking);
        if (target != null)
        {
            int hits = CalculateHits(_k);
            _view.WriteLine("----------------------------------------");
            double damage = Math.Sqrt(_unitDataAttacking.Strength * _skillPower);

            AffinitiesController affinitiesController = new AffinitiesController("Phys", damage, target, _unitDataAttacking, _view, turnsController);
            bool isReturnDamageAffinity = false;
            for (int i = 0; i < hits; i++)
            {

                _view.WriteLine($"{_unitDataAttacking.Name} ataca a {target.Name}");
            
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
                _view.WriteLine($"{_unitDataAttacking.Name} termina con HP:{_unitDataAttacking.HP}/{_unitDataAttacking.maxHP}");;

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
