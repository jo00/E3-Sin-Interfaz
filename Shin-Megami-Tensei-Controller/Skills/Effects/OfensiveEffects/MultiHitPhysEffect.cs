using Shin_Megami_Tensei_View;
using Shin_Megami_Tensei.Skills.Effects;
using Shin_Megami_Tensei.Skills.Effects.OfensiveEffects;

namespace Shin_Megami_Tensei.Skills;

public class MultiHitPhysEffect : OffensivePhysEffect
{
   
    private readonly int _min;
    private readonly int _max;
    private readonly UnitData _unitDataAttacking;
    private readonly int _skillPower;
    private readonly ImplementedConsoleView _view;
    private readonly int _k;
    private bool _wasEffectApplied = true;

    private List<UnitData> _oponentUnits;
    private TurnsController _turnsController;
    private UnitData _target;
    private AffinitiesController _affinitiesController;
    private double _baseDamage;

    public MultiHitPhysEffect(UnitData unitDataAttacking, int min, int max, int skillPower, ImplementedConsoleView view, int k) : base(unitDataAttacking)
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
        _oponentUnits = oponentUnits;
        _turnsController = turnsController;

        if (!SelectTarget())
        {
            _wasEffectApplied = false;
            return;
        }

        PrepareAttack();
        ExecuteHits();
        AnnounceFinalHP();
    }

    private bool SelectTarget()
    {
        MenusController menuController = new MenusController(_view);
        _target = menuController.SelectTarget(_oponentUnits, _unitDataAttacking);
        return _target != null;
    }

    private void PrepareAttack()
    {
        _view.ShowSeparator();
        _baseDamage = Math.Sqrt(_unitDataAttacking.Strength * _skillPower);
        _affinitiesController = new AffinitiesController("Phys", _baseDamage, _target, _unitDataAttacking, _view, _turnsController);
    }

    private void ExecuteHits()
    {
        int hits = CalculateHits(_k);

        for (int i = 0; i < hits; i++)
        {
            _view.AnounceAttack(_unitDataAttacking, _target);

            int damageWithAffinities =(int) _affinitiesController.ApplyAffinity();
            _target.DiscountHp(damageWithAffinities);

            if (!_affinitiesController.IsReturnDamageAffinity() && damageWithAffinities > 0)
            {
                _view.AnounceDamageReceived(_target, damageWithAffinities);
            }
        }
    }

    private void AnnounceFinalHP()
    {
        if (_affinitiesController.IsReturnDamageAffinity())
        {
            _view.AnounceHPFinalStateForUnit(_unitDataAttacking);
        }
        else
        {
            _view.AnounceHPFinalStateForUnit(_target);
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
