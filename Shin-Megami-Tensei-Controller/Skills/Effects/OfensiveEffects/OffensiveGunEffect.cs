namespace Shin_Megami_Tensei.Skills.Effects.OfensiveEffects;

public class OffensiveGunEffect:Effect
{
    public OffensiveGunEffect(UnitData unitDataAttacking) : base(unitDataAttacking)
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