using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei.Skills.Effects;

public class MultiTargetMultiHitPhysEffect:Effect
{
    private readonly int _min;
    private readonly int _max;
    private readonly Unit _unitAttacking;
    private readonly int _skillPower;
    private readonly View _view;
    private int _k;
 
    public MultiTargetMultiHitPhysEffect(Unit unitAttacking, int min, int max, int skillPower, View view, int k) : base(unitAttacking)
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

    public override bool WasEffectApplied()
    {
        return true;

    }

    public static List<Unit> GetMultiTargetUnits(List<Unit> activeUnits, int k, int hits)
    {
        List<Unit> targets = new List<Unit>();

        int A = activeUnits.Count;
        if (A == 0) return targets;

        int i = k % A;
        bool moveLeft = i % 2 == 0;  

        List<Unit> R = activeUnits;

        int index = i;

        for (int count = 0; count < hits; count++)
        {
            targets.Add(R[index]);

            if (moveLeft)
            {
                index--;
                if (index < 0)
                    index = A - 1; 
            }
            else
            {
                index++;
                if (index >= A)
                    index = 0; 
            }
        }

        return targets;
    }
    
    private int CalculateHits(int k)
    {
        int range = _max - _min + 1;
        int offset = k % range;
        return _min + offset;
    }

    
}