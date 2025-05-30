using Shin_Megami_Tensei_View;
using Shin_Megami_Tensei.Configs;

namespace Shin_Megami_Tensei.Skills.Effects;

public class SummonEffect : Effect
{
    private readonly UnitData _unitDataAttacking;
    private readonly TeamData _teamData;
    private readonly TurnsController _turnsController;
    private readonly SummonController _summonController;
    private bool _wasEffectApplied = true;

    public SummonEffect(UnitData unitDataAttacking, TeamData teamData, TurnsController turnsController, SummonController summonController)
        : base(unitDataAttacking)
    {
        _unitDataAttacking = unitDataAttacking;
        _teamData = teamData;
        _turnsController = turnsController;
        _summonController = summonController;
    }

    public override void Apply(List<UnitData> oponentUnits, TurnsController turnsController)
    {
        if (!_summonController.SummonFromAbility(_unitDataAttacking, _teamData,
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