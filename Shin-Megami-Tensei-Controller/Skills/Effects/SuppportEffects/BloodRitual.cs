using Shin_Megami_Tensei_View;
using Shin_Megami_Tensei.Configs;

namespace Shin_Megami_Tensei.Skills.Effects.SuppportEffects;

public class BloodRitual:Effect
{
    private UnitData _unitDataAttacking;
    private TeamData _teamData;
    private int _power;
    private TurnsController _turnsController;
    private bool _wasEffectApplied = true;
    private ImplementedConsoleView _view;
    private TeamController _teamController;
    private bool _shouldApplied;
    public BloodRitual(UnitData unitDataAttacking, TeamData teamData,  int power, TurnsController turnsController, ImplementedConsoleView view, TeamController teamController) : base(unitDataAttacking)
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


        int statsDelta = 1;
        _unitDataAttacking.HP = statsDelta;
        _unitDataAttacking.ChangeDefensiveDegree(statsDelta);
        _unitDataAttacking.ChangeOffensiveDegree(statsDelta);

        _view.AnounceBloodRitual(_unitDataAttacking);
        _turnsController.ChangeTurnsForNonOffensiveAbilities();

        
        
    }

    public override bool WasEffectApplied()
    {
        return _wasEffectApplied;
    }
}