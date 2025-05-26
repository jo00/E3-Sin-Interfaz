using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public class AffinitiesController
{
    private string _attackType;
    private double _baseDamage;
    private Unit _targetUnit;
    private Unit _unitAttacking;
    private View _view;
    private TurnsController _turnsController;
    private bool _isReturnDamageAffinity;
    private bool _wereTurnChangesAlreadyApplied;

    public AffinitiesController(string attackType, double baseDamage, Unit targetUnit, Unit unitAttacking, View view, TurnsController turnsController)
    {
        _attackType = attackType;
        _baseDamage = baseDamage;
        _targetUnit = targetUnit;
        _unitAttacking = unitAttacking;
        _view = view;
        _turnsController = turnsController;
        _wereTurnChangesAlreadyApplied = false;
    }
    public int ApplyAffinity()
    {
        if (_targetUnit.Affinities == null || !_targetUnit.Affinities.ContainsKey(_attackType))
        {
            return (int)_baseDamage; 
        }

        string affinity = _targetUnit.Affinities[_attackType];
        
        _isReturnDamageAffinity = false;

        switch (affinity)
        {
            case "Wk":
                _view.WriteLine($"{_targetUnit.Name} es débil contra el ataque de {_unitAttacking.Name}");
                if (!_wereTurnChangesAlreadyApplied)
                {
                    _turnsController.ChangeTurnsForWeakAffinity();
                    _wereTurnChangesAlreadyApplied = true;
                }
                return (int)(_baseDamage * 1.5);
            case "Rs":
                if (!_wereTurnChangesAlreadyApplied)
                {
                    _turnsController.ChangeTurnStateForNeutralOrResistAffinity();
                    _wereTurnChangesAlreadyApplied = true;
                }
                _view.WriteLine($"{_targetUnit.Name} es resistente el ataque de {_unitAttacking.Name}");
                return (int)(_baseDamage * 0.5);
            case "Nu":
                if (!_wereTurnChangesAlreadyApplied)
                {
                    _turnsController.ChangeTurnsStateForNullAffinity();
                    _wereTurnChangesAlreadyApplied = true;
                }
                _view.WriteLine($"{_targetUnit.Name} bloquea el ataque de {_unitAttacking.Name}");
                return 0;
            case "Dr":
                if (!_wereTurnChangesAlreadyApplied)
                {
                    _turnsController.ChangeTurnsStateForDrOrRepelAffinity();
                    _wereTurnChangesAlreadyApplied = true;
                }
                _targetUnit.HP = (int)Math.Min(_targetUnit.HP + _baseDamage, _targetUnit.maxHP);
                _view.WriteLine($"{_targetUnit.Name} absorbe {(int) _baseDamage} daño");
                return 0;
            case "-":
                if (!_wereTurnChangesAlreadyApplied)
                {
                    _turnsController.ChangeTurnStateForNeutralOrResistAffinity();
                    _wereTurnChangesAlreadyApplied = true;
                }
                return (int)_baseDamage;
            case "Rp":
                if (!_wereTurnChangesAlreadyApplied)
                {
                    _turnsController.ChangeTurnsStateForDrOrRepelAffinity();
                    _wereTurnChangesAlreadyApplied = true;
                }

                _isReturnDamageAffinity = true;
                _view.WriteLine($"{_targetUnit.Name} devuelve {(int)_baseDamage} daño a {_unitAttacking.Name}");
                _unitAttacking.DiscountHp((int)_baseDamage);
                return 0;

            default:
                return (int)_baseDamage;
        }
    }
    
    public bool IsReturnDamageAffinity()
    {
        return _isReturnDamageAffinity;
    }
}