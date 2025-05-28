using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public class AffinitiesController
{
    private string _attackType;
    private double _baseDamage;
    private UnitData _targetUnitData;
    private UnitData _unitDataAttacking;
    private ImplementedConsoleView _view;
    private TurnsController _turnsController;
    private bool _isReturnDamageAffinity;
    private bool _wereTurnChangesAlreadyApplied;
    private double _increment;
    private double offensiveMultiplierUnitAttacking;
    private double defensiveMultiplierTargetUnit;

    public AffinitiesController(string attackType, double baseDamage, UnitData targetUnitData, UnitData unitDataAttacking, ImplementedConsoleView view, TurnsController turnsController, double increment)
    {
        _attackType = attackType;
        _baseDamage = baseDamage;
        _targetUnitData = targetUnitData;
        _unitDataAttacking = unitDataAttacking;
        _view = view;
        _turnsController = turnsController;
        _wereTurnChangesAlreadyApplied = false;
        _increment = increment;
        offensiveMultiplierUnitAttacking = _unitDataAttacking.offensiveMultipliers[_unitDataAttacking.offensiveDegree];
        defensiveMultiplierTargetUnit = _targetUnitData.defensiveMultipliers[_targetUnitData.defensiveDegree];


    }
    public double ApplyAffinity()
    {
        if (_targetUnitData.Affinities == null || !_targetUnitData.Affinities.ContainsKey(_attackType))
        {
            return _baseDamage*_increment*offensiveMultiplierUnitAttacking * defensiveMultiplierTargetUnit; 
        }

        string affinity = _targetUnitData.Affinities[_attackType];
        
        _isReturnDamageAffinity = false;

        switch (affinity)
        {
            case "Wk":
                _view.AnounceThatTargetUnitIsWeak(_targetUnitData, _unitDataAttacking);
                if (!_wereTurnChangesAlreadyApplied)
                {
                    _turnsController.ChangeTurnsForWeakAffinity();
                    _wereTurnChangesAlreadyApplied = true;
                }
                return (_baseDamage * 1.5*_increment*offensiveMultiplierUnitAttacking * defensiveMultiplierTargetUnit);
            case "Rs":
                if (!_wereTurnChangesAlreadyApplied)
                {
                    _turnsController.ChangeTurnStateForNeutralOrResistAffinity();
                    _wereTurnChangesAlreadyApplied = true;
                }
                _view.AnounceThatTargetIsRessistent(_targetUnitData, _unitDataAttacking);
                return (_baseDamage * 0.5*_increment*offensiveMultiplierUnitAttacking * defensiveMultiplierTargetUnit);
            case "Nu":
                if (!_wereTurnChangesAlreadyApplied)
                {
                    _turnsController.ChangeTurnsStateForNullAffinity();
                    _wereTurnChangesAlreadyApplied = true;
                }
                _view.AnounceThatTargetBlockedAttack(_targetUnitData, _unitDataAttacking);
                return 0;
            case "Dr":
                if (!_wereTurnChangesAlreadyApplied)
                {
                    _turnsController.ChangeTurnsStateForDrOrRepelAffinity();
                    _wereTurnChangesAlreadyApplied = true;
                }
                _targetUnitData.HP = (int)Math.Min(_targetUnitData.HP + _baseDamage, _targetUnitData.maxHP);
                _view.AnounceThatTargetUnitAbsorbedDamage(_targetUnitData, (int)(_baseDamage* _increment));
                return 0;
            case "-":
                if (!_wereTurnChangesAlreadyApplied)
                {
                    _turnsController.ChangeTurnStateForNeutralOrResistAffinity();
                    _wereTurnChangesAlreadyApplied = true;
                }
                return _baseDamage*_increment*offensiveMultiplierUnitAttacking * defensiveMultiplierTargetUnit;
            case "Rp":
                if (!_wereTurnChangesAlreadyApplied)
                {
                    _turnsController.ChangeTurnsStateForDrOrRepelAffinity();
                    _wereTurnChangesAlreadyApplied = true;
                }

                _isReturnDamageAffinity = true;
                _view.AnounceThatTargetUnitReflectedDamage(_targetUnitData, _unitDataAttacking, (int)(_baseDamage*_increment));
                _unitDataAttacking.DiscountHp((int)(_baseDamage*_increment));
                return 0;

            default:
                return _baseDamage*_increment*offensiveMultiplierUnitAttacking * defensiveMultiplierTargetUnit;
        }
    }
    
    public bool IsReturnDamageAffinity()
    {
        return _isReturnDamageAffinity;
    }
}