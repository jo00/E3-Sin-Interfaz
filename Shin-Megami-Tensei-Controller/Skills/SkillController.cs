using Shin_Megami_Tensei.Skills.Effects;

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

  
 
}