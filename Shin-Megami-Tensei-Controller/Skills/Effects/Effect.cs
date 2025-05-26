using System.Security.Cryptography;
using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei.Skills.Effects;

public abstract class Effect

{

    protected View _view;
    protected Unit _target;
    protected Unit _unitAttacking;

    public Effect(Unit unitAttacking)
    {
        this._unitAttacking = unitAttacking;
       
    }

    

    public abstract void Apply(List<Unit> oponentUnits, TurnsController turnsController);

    public abstract bool WasEffectApplied();
}