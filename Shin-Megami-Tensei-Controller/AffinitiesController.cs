using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public class AffinitiesController
{
    private string _attackType;
    private double _baseDamage;
    private UnitData _targetUnitData;
    private UnitData _unitDataAttacking;
    private View _view;
    private TurnsController _turnsController;
    private bool _isReturnDamageAffinity;
    private bool _wereTurnChangesAlreadyApplied;

    public AffinitiesController(string attackType, double baseDamage, UnitData targetUnitData, UnitData unitDataAttacking, View view, TurnsController turnsController)
    {
        _attackType = attackType;
        _baseDamage = baseDamage;
        _targetUnitData = targetUnitData;
        _unitDataAttacking = unitDataAttacking;
        _view = view;
        _turnsController = turnsController;
        _wereTurnChangesAlreadyApplied = false;
    }
    public int ApplyAffinity()
    {
        if (_targetUnitData.Affinities == null || !_targetUnitData.Affinities.ContainsKey(_attackType))
        {
            return (int)_baseDamage; 
        }

        string affinity = _targetUnitData.Affinities[_attackType];
        
        _isReturnDamageAffinity = false;

        switch (affinity)
        {
            case "Wk":
                _view.WriteLine($"{_targetUnitData.Name} es débil contra el ataque de {_unitDataAttacking.Name}");
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
                _view.WriteLine($"{_targetUnitData.Name} es resistente el ataque de {_unitDataAttacking.Name}");
                return (int)(_baseDamage * 0.5);
            case "Nu":
                if (!_wereTurnChangesAlreadyApplied)
                {
                    _turnsController.ChangeTurnsStateForNullAffinity();
                    _wereTurnChangesAlreadyApplied = true;
                }
                _view.WriteLine($"{_targetUnitData.Name} bloquea el ataque de {_unitDataAttacking.Name}");
                return 0;
            case "Dr":
                if (!_wereTurnChangesAlreadyApplied)
                {
                    _turnsController.ChangeTurnsStateForDrOrRepelAffinity();
                    _wereTurnChangesAlreadyApplied = true;
                }
                _targetUnitData.HP = (int)Math.Min(_targetUnitData.HP + _baseDamage, _targetUnitData.maxHP);
                _view.WriteLine($"{_targetUnitData.Name} absorbe {(int) _baseDamage} daño");
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
                _view.WriteLine($"{_targetUnitData.Name} devuelve {(int)_baseDamage} daño a {_unitDataAttacking.Name}");
                _unitDataAttacking.DiscountHp((int)_baseDamage);
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