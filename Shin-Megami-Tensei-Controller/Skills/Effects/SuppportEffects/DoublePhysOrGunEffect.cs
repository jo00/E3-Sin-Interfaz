using Shin_Megami_Tensei_View;
using Shin_Megami_Tensei.Configs;

namespace Shin_Megami_Tensei.Skills.Effects.SuppportEffects;

public class DoublePhysOrGunEffect:Effect
{
    private UnitData _unitDataAttacking;
    private TeamData _teamData;
    private int _power;
    private TurnsController _turnsController;
    private bool _wasEffectApplied = true;
    private ImplementedConsoleView _view;
    private TeamController _teamController;
    private bool _shouldApplied;
    public DoublePhysOrGunEffect(UnitData unitDataAttacking, TeamData teamData,  int power, TurnsController turnsController, ImplementedConsoleView view, TeamController teamController) : base(unitDataAttacking)
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
        double increment = 2.5;
        if (_unitDataAttacking.Strength == _unitDataAttacking.originalStrength)
        {
            _unitDataAttacking.Strength *= increment;

        }

        if (_unitDataAttacking.Skill == _unitDataAttacking.originalSkill)
        {
            _unitDataAttacking.Skill *= increment;

        }

     
        _view.AnounceCharge(_unitDataAttacking);

        
        _turnsController.ChangeTurnsForNonOffensiveAbilities();

        
        
    }

    public override bool WasEffectApplied()
    {
        return _wasEffectApplied;
    }
}