using Shin_Megami_Tensei_View;
using Shin_Megami_Tensei.Configs;

namespace Shin_Megami_Tensei.Skills.Effects;

public class SummonEffect : Effect
{
    private readonly Unit _unitAttacking;
    private readonly TeamData _teamData;
    private readonly View _view;
    private readonly TurnsController _turnsController;
    private readonly SummonController _summonController;
    private bool _wasEffectApplied = true;

    public SummonEffect(Unit unitAttacking, TeamData teamData, View view, TurnsController turnsController, SummonController summonController)
        : base(unitAttacking)
    {
        _unitAttacking = unitAttacking;
        _teamData = teamData;
        _view = view;
        _turnsController = turnsController;
        _summonController = summonController;
    }

    public override void Apply(List<Unit> oponentUnits, TurnsController turnsController)
    {
        if (!_summonController.SummonFromAbility(_unitAttacking, _teamData,
                () => { turnsController.ChangeTurnsForNonOffensiveAbilities(); }))
        {
            _wasEffectApplied = false;
        }
    }
    
    public override bool WasEffectApplied()
    {
        return _wasEffectApplied;
    }
}