using System.Security.Cryptography;
using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei.Skills.Effects;

public abstract class Effect

{

    protected View _view;
    protected UnitData _target;
    protected UnitData UnitDataAttacking;

    public Effect(UnitData unitDataAttacking)
    {
        this.UnitDataAttacking = unitDataAttacking;
       
    }

    

    public abstract void Apply(List<UnitData> oponentUnits, TurnsController turnsController);

    public abstract bool WasEffectApplied();
}