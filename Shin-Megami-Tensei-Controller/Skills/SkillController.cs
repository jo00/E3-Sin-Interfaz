using Shin_Megami_Tensei.Skills.Effects;
using Shin_Megami_Tensei.Skills.Effects.OfensiveEffects;
using Shin_Megami_Tensei.Skills.Effects.SuppportEffects;

namespace Shin_Megami_Tensei.Skills;

public class SkillController
{
    private SkillData _skillDataData;
    private List<Effect> _effects;
    private UnitData _unitDataAttacking;
    private TurnsController _turnsController;
    private bool _wasSkillApplied = true;
    private bool _wasDamageReturned = false;
    public SkillController(SkillData skillDataData, List<Effect> effects, UnitData unitDataAttacking, TurnsController turnsController)
    {
        _skillDataData = skillDataData;
        _effects = effects;
        _unitDataAttacking = unitDataAttacking;
        _turnsController = turnsController;
    }

    public void ApplySkillEffects(List<UnitData> oponentUnits)
    {
        foreach (Effect effect in _effects)
        {
            effect.Apply(oponentUnits, _turnsController);
            if (effect.WasEffectApplied())
            {
                _unitDataAttacking.MP -=_skillDataData.cost;

            }
        }
    }
    
    public bool WasSkillApplied()
    {
        foreach (Effect effect in _effects)
        {
            if (!effect.WasEffectApplied())
            {
                return false;
            }
        }

        return true;
    }

    public bool WasEffectCharge()
    {
        foreach (Effect effect in _effects)
        {
            if (effect is DoublePhysOrGunEffect)
            {
                return true;
            }
        }

        return false;
    }
    
    public bool WasEffectConcentrate()
    {
        foreach (Effect effect in _effects)
        {
            if (effect is DoublesMagicEffect)
            {
                return true;
            }
        }

        return false;
    }
    
    public bool WasEffectOffensiveMagic()
    {
        foreach (Effect effect in _effects)
        {
            if (effect is OffensiveMagicEffect)
            {
                return true;
            }
        }

        return false;
    }
    
    public bool WasEffectOffensivePhysOrGun()
    {
        foreach (Effect effect in _effects)
        {
            if ((effect is OffensivePhysEffect) || (effect is OffensiveGunEffect))
            {
                return true;
            }
        }

        return false;
    }


  
 
}