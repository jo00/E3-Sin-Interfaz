using Shin_Megami_Tensei_View;
using Shin_Megami_Tensei.Skills.Effects.OfensiveEffects;

namespace Shin_Megami_Tensei.Skills.Effects;

public class InstaKillEffect:OffensiveMagicEffect
{
    private UnitData _unitDataAttacking;
    private int _skillPower;
    private ImplementedConsoleView _view;
    private bool _wasEffectApplied = true;
    private string _type;
    private bool _wasEffective=false;
    private bool _wasMissed = false;
    private bool _wasBlocked = false;

    public InstaKillEffect(UnitData unitDataAttacking, int skillPower, ImplementedConsoleView view, string type) : base(unitDataAttacking)
    {
        _unitDataAttacking = unitDataAttacking;
        _skillPower = skillPower;
        _view = view;
        _type = type;
    }

    public override void Apply(List<UnitData> oponentUnits, TurnsController turnsController)
    {
        MenusController menuController = new MenusController(_view);
        UnitData target = menuController.SelectTarget(oponentUnits, _unitDataAttacking);
        if (target != null)
        {
            _view.ShowLines();
            AnounceCorrespondingAttack(target);
            string affinity = target.Affinities[_type];
            CheckEffectivness(affinity, target, turnsController);
            ApplyCorrespondingEffectivness(target);
            _view.AnounceHPFinalStateForUnit(target);

            
        }
        else
        {
            _wasEffectApplied= false;
        }

    
    }
    private void AnounceCorrespondingAttack(UnitData target)
    {
        if (_type == "Light")
        {
            _view.AnounceLightAttack(_unitDataAttacking,target);
        }
        else
        {
            _view.AnounceDarkAttack(_unitDataAttacking,target);
        }
    }

    private void CheckEffectivness(string affinity, UnitData target, TurnsController turnsController)
    {
        if (affinity == "-")
        {
            CheckEffectivnessForNeutralAffinity(target);
            turnsController.ChangeTurnStateForMissNeutralOrResistAffinity();
        }

        if (affinity == "Wk")
        {
            _view.AnounceThatTargetUnitIsWeak(target, _unitDataAttacking);
            turnsController.ChangeTurnsForWeakAffinity();
            _wasEffective = true;
        }

        if (affinity == "Rs")
        {
            turnsController.ChangeTurnStateForMissNeutralOrResistAffinity();
            CheckEffectivnessForResistAffinity(target);
        }
        
        if(affinity == "Nu")
        {
            turnsController.ChangeTurnsStateForNullAffinity();
            _wasBlocked = true;
        }
    }

    private void CheckEffectivnessForNeutralAffinity(UnitData target)
    {
        if (_unitDataAttacking.Luck + _skillPower >= target.Luck)
        {
            _wasEffective = true;
        }
        else
        {
            _wasMissed = true;
        }
    }

    private void CheckEffectivnessForResistAffinity(UnitData target)
    {
        if(_unitDataAttacking.Luck+_skillPower>=2* target.Luck)
        {
            _view.AnounceThatTargetIsRessistent(target,_unitDataAttacking);

            _wasEffective = true;
        }
        else
        {

            _wasMissed = true;
        }
    }

    private void ApplyCorrespondingEffectivness(UnitData target)
    {
        if (_wasBlocked)
        {
            _view.AnounceThatTargetBlockedAttack(target, _unitDataAttacking);
        }
        if(_wasMissed)
        {
            _view.AnounceThatInstaKillFailed(_unitDataAttacking);
        }
        if(_wasEffective)
        {
            _view.AnounceThatTargetHasBeenEliminated(target);
            target.DiscountHp(target.HP);

        }
    }
    public override bool WasEffectApplied()
    {
        return _wasEffectApplied;
    }

    
}