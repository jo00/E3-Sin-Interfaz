using Shin_Megami_Tensei_View;
using Shin_Megami_Tensei.Configs;
using Shin_Megami_Tensei.Skills.Effects.OfensiveEffects;

namespace Shin_Megami_Tensei.Skills.Effects;

public class MultiTargetPhysEffect:OffensivePhysEffect

{
    private readonly int _min;
    private readonly int _max;
    private UnitData _unitDataAttacking;
    private int _skillPower;
    private ImplementedConsoleView _view;
    private bool _wasEffectApplied = true;
    private TurnsController _turnsController;
    private TeamController _teamController;
    private bool _wasAttackUnitAttacked = false;
    private List<string> _affinitiesApplied;
    private int _K;
    public MultiTargetPhysEffect(UnitData unitDataAttacking, int min, int max, int skillPower, ImplementedConsoleView view, int k, TeamController teamController) : base(unitDataAttacking)
    {
        _unitDataAttacking = unitDataAttacking;
        _skillPower = skillPower;
        _view = view;
        _teamController = teamController;
        _min = min;
        _max = max;
        _K = k;
        
    }

    public override void Apply(List<UnitData> oponentUnits, TurnsController turnsController)
    {
        _view.ShowLines();
        _turnsController= turnsController;
        List<UnitData> activeOpponentUnitsAlive = _teamController.GetActiveUnitsAlive(oponentUnits);
        MenusController menuController = new MenusController(_view);
        int hits = CalculateHits(_K);
        List<UnitData>targets= _teamController.GetMultiTargetUnits(activeOpponentUnitsAlive, _K, hits);
        foreach (UnitData target in targets)
        {
            if (target != null)
            {
                _view.AnounceAttackWithouLines(_unitDataAttacking, target);
                double damage = (Math.Sqrt(_unitDataAttacking.Strength * _skillPower));

                _affinitiesApplied.Add(target.Affinities["Phys"]);
                AffinitiesController affinitiesController = new AffinitiesController("Phys", damage, target, _unitDataAttacking, _view, turnsController,1);
                int damageWithAffinities = (int)affinitiesController.ApplyAffinity();
                target.DiscountHp(damageWithAffinities);
                if (!affinitiesController.IsReturnDamageAffinity())
                {
                    menuController.ShowEffectOfDamage(_unitDataAttacking, target, damageWithAffinities);

                }
        
                else
                {
                    _wasAttackUnitAttacked = true;

                }
            }
            else
            {
                _wasEffectApplied = false;
            }
        }

        if (_wasAttackUnitAttacked)
        {
            _view.AnounceHPFinalStateForUnit(_unitDataAttacking);
        }
        
        ChangeTurnsAccordingToActionsPriority();
        
        

    }

    public override bool WasEffectApplied()
    {
        return _wasEffectApplied;

    }
    private void ChangeTurnsAccordingToActionsPriority()
    {
        if (_affinitiesApplied.Contains("Rp") || _affinitiesApplied.Contains("Dr"))
        {
            _turnsController.ChangeTurnsStateForDrOrRepelAffinity();
        }
        else if (_affinitiesApplied.Contains("Null"))
        {
            _turnsController.ChangeTurnsStateForNullAffinity();
        }
        else if (_affinitiesApplied.Contains("Miss"))
        {
            _turnsController.ChangeTurnStateForMissNeutralOrResistAffinity();
        }
        else if (_affinitiesApplied.Contains("Weak"))
        {
            _turnsController.ChangeTurnsForWeakAffinity();
        }
        else if (_affinitiesApplied.Contains("Neutral") || _affinitiesApplied.Contains("Resit"))
        {
            _turnsController.ChangeTurnStateForMissNeutralOrResistAffinity();
        }
    }
    
    private int CalculateHits(int k)
    {
        int range = _max - _min + 1;
        int offset = k % range;
        return _min + offset;
    }

    
}