using Shin_Megami_Tensei.Skills.Effects;

namespace Shin_Megami_Tensei.Skills;

public class SkillController
{
    private SkillData _skillDataData;
    private List<Effect> _effects;
    private Unit _unitAttacking;
    private TurnsController _turnsController;
    private bool _wasSkillApplied = true;
    private bool _wasDamageReturned = false;
    public SkillController(SkillData skillDataData, List<Effect> effects, Unit unitAttacking, TurnsController turnsController)
    {
        _skillDataData = skillDataData;
        _effects = effects;
        _unitAttacking = unitAttacking;
        _turnsController = turnsController;
    }

    public void ApplySkillEffects(List<Unit> oponentUnits)
    {
        foreach (Effect effect in _effects)
        {
            effect.Apply(oponentUnits, _turnsController);
            if (effect.WasEffectApplied())
            {
                _unitAttacking.MP -=_skillDataData.cost;

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

  
 
}