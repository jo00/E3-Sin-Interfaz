namespace Shin_Megami_Tensei.Skills.Effects.OfensiveEffects;

public class OffensiveMagicEffect:Effect
{
    protected OffensiveMagicEffect(UnitData unitDataAttacking) : base(unitDataAttacking)
    {
    }

    public override void Apply(List<UnitData> oponentUnits, TurnsController turnsController)
    {
        throw new NotImplementedException();
    }

    public override bool WasEffectApplied()
    {
        throw new NotImplementedException();
    }
}