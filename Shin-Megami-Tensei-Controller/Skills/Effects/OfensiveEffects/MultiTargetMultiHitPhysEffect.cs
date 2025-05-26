using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei.Skills.Effects;

public class MultiTargetMultiHitPhysEffect:Effect
{
    private readonly int _min;
    private readonly int _max;
    private readonly UnitData _unitDataAttacking;
    private readonly int _skillPower;
    private readonly View _view;
    private int _k;
 
    public MultiTargetMultiHitPhysEffect(UnitData unitDataAttacking, int min, int max, int skillPower, View view, int k) : base(unitDataAttacking)
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

    public override bool WasEffectApplied()
    {
        return true;

    }

    public static List<UnitData> GetMultiTargetUnits(List<UnitData> activeUnits, int k, int hits)
    {
        List<UnitData> targets = new List<UnitData>();

        int A = activeUnits.Count;
        if (A == 0) return targets;

        int i = k % A;
        bool moveLeft = i % 2 == 0;  

        List<UnitData> R = activeUnits;

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