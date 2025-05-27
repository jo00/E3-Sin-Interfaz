using Shin_Megami_Tensei_View;
using Shin_Megami_Tensei.Configs;

namespace Shin_Megami_Tensei.Skills.Effects.SuppportEffects;

public class ConcentrateEffect:Effect
{
    private UnitData _unitDataAttacking;
    private TeamData _teamData;
    private int _power;
    private TurnsController _turnsController;
    private bool _wasEffectApplied = true;
    private ImplementedConsoleView _view;
    private TeamController _teamController;
    private bool _shouldApplied;
    public ConcentrateEffect(UnitData unitDataAttacking, TeamData teamData,  int power, TurnsController turnsController, ImplementedConsoleView view, TeamController teamController) : base(unitDataAttacking)
    {
        _teamController = teamController;
        _view = view;
        _unitDataAttacking = unitDataAttacking;
        _teamData = teamData;
        _power = power;
        _turnsController = turnsController;
    }


    public override void Apply(List<UnitData> oponentUnits, TurnsController turnsController)
    {
        bool shouldAnounce = false;
        if (_unitDataAttacking.Magic == _unitDataAttacking.originalMagic)
        {
            _unitDataAttacking.Magic *= 2.5;

        }

        
        _view.AnounceConcentrate(_unitDataAttacking);

        
        _turnsController.ChangeTurnsForNonOffensiveAbilities();

        
        
    }

    public override bool WasEffectApplied()
    {
        return _wasEffectApplied;
    }
}