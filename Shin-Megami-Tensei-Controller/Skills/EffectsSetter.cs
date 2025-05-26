using Shin_Megami_Tensei_View;
using Shin_Megami_Tensei.Configs;
using Shin_Megami_Tensei.Skills.Effects;
using Shin_Megami_Tensei.Skills.Effects.OfensiveEffects;

namespace Shin_Megami_Tensei.Skills;

public class EffectsSetter
{

    private SkillData _skillData;
    private Unit _unitAttacking;
    private View _view;
    private TurnsController _turnsController;
    private TeamData _teamData;
    private SummonController _summonController;
    public EffectsSetter(SkillData skillData, Unit unitAttacking, View view, TurnsController turnsController, TeamData teamData, SummonController summonController)
    {
        _skillData = skillData;
        _unitAttacking = unitAttacking;
        _view = view;
        _turnsController = turnsController;
        _teamData = teamData;
        _summonController = summonController;
    }
    public SkillController SetEffectsForSkill()
    {
        string skillName = _skillData.name;
        List<Effect> effects = new List<Effect>();
        switch (skillName)
        {
            case "Needle Shot":
                effects.Add(new GunEffect(_unitAttacking, _skillData.power, _view));
                return new SkillController(_skillData, effects, _unitAttacking, _turnsController);
            case "Tathlum Shot":
                effects.Add(new GunEffect(_unitAttacking, _skillData.power, _view));
                return new SkillController(_skillData, effects, _unitAttacking, _turnsController);
            case "Grand Tack":
                effects.Add(new GunEffect(_unitAttacking, _skillData.power, _view));
                return new SkillController(_skillData, effects, _unitAttacking, _turnsController);

            case "Riot Gun":
                effects.Add(new GunEffect(_unitAttacking, _skillData.power, _view));
                return new SkillController(_skillData, effects, _unitAttacking, _turnsController);

            case "Lunge":
                effects.Add(new PhysEffect(_unitAttacking, _skillData.power, _view));
                return new SkillController(_skillData, effects, _unitAttacking, _turnsController);

            case "Oni-Kagura":
                effects.Add(new PhysEffect(_unitAttacking, _skillData.power, _view));
                return new SkillController(_skillData, effects, _unitAttacking, _turnsController);

            case "Mortal Jihad":
                effects.Add(new PhysEffect(_unitAttacking, _skillData.power, _view));
                return new SkillController(_skillData, effects, _unitAttacking, _turnsController);

            case "Gram Slice":
                effects.Add(new PhysEffect(_unitAttacking, _skillData.power, _view));
                return new SkillController(_skillData, effects, _unitAttacking, _turnsController);

            case "Fatal Sword":
                effects.Add(new PhysEffect(_unitAttacking, _skillData.power, _view));
                return new SkillController(_skillData, effects, _unitAttacking, _turnsController);

            case "Berserker God":
                effects.Add(new PhysEffect(_unitAttacking, _skillData.power, _view));
                return new SkillController(_skillData, effects, _unitAttacking, _turnsController);

            case "Iron Judgement":
                effects.Add(new PhysEffect(_unitAttacking, _skillData.power, _view));
                return new SkillController(_skillData, effects, _unitAttacking, _turnsController);

            case "Stigma Attack":
                effects.Add(new PhysEffect(_unitAttacking, _skillData.power, _view));
                return new SkillController(_skillData, effects, _unitAttacking, _turnsController);

            case "Agi":
                effects.Add(new FireEffect(_unitAttacking, _skillData.power, _view));
                return new SkillController(_skillData, effects, _unitAttacking, _turnsController);

            case "Agilao":
                effects.Add(new FireEffect(_unitAttacking, _skillData.power, _view));
                return new SkillController(_skillData, effects, _unitAttacking, _turnsController);

            case "Agidyne":
                effects.Add(new FireEffect(_unitAttacking, _skillData.power, _view));
                return new SkillController(_skillData, effects, _unitAttacking, _turnsController);

            case "Trisagion":
                effects.Add(new FireEffect(_unitAttacking, _skillData.power, _view));
                return new SkillController(_skillData, effects, _unitAttacking, _turnsController);

            case "Bufu":
                effects.Add(new IceEffect(_unitAttacking, _skillData.power, _view));
                return new SkillController(_skillData, effects, _unitAttacking, _turnsController);

            case "Bufula":
                effects.Add(new IceEffect(_unitAttacking, _skillData.power, _view));
                return new SkillController(_skillData, effects, _unitAttacking, _turnsController);

            case "Bufudyne":
                effects.Add(new IceEffect(_unitAttacking, _skillData.power, _view));
                return new SkillController(_skillData, effects, _unitAttacking, _turnsController);

            case "Zio":
                effects.Add(new ElecEffect(_unitAttacking, _skillData.power, _view));
                return new SkillController(_skillData, effects, _unitAttacking, _turnsController);

            case "Zionga":
                effects.Add(new ElecEffect(_unitAttacking, _skillData.power, _view));
                return new SkillController(_skillData, effects, _unitAttacking, _turnsController);

            case "Ziodyne":
                effects.Add(new ElecEffect(_unitAttacking, _skillData.power, _view));
                return new SkillController(_skillData, effects, _unitAttacking, _turnsController);

            case "Zan":
                effects.Add(new ForceEffect(_unitAttacking, _skillData.power, _view));
                return new SkillController(_skillData, effects, _unitAttacking, _turnsController);

            case "Zanma":
                effects.Add(new ForceEffect(_unitAttacking, _skillData.power, _view));
                return new SkillController(_skillData, effects, _unitAttacking, _turnsController);

            case "Zandyne":
                effects.Add(new ForceEffect(_unitAttacking, _skillData.power, _view));
                return new SkillController(_skillData, effects, _unitAttacking, _turnsController);

            case "Deadly Wind":
                effects.Add(new ForceEffect(_unitAttacking, _skillData.power, _view));
                return new SkillController(_skillData, effects, _unitAttacking, _turnsController);

            case "Bouncing Claw":
                effects.Add(new MultiHitPhysEffect(_unitAttacking, 1, 3, 80, _view, _teamData.abilitiesUsedCounter));
                return new SkillController(_skillData, effects, _unitAttacking, _turnsController);

            case "Damascus Claw":
                effects.Add(new MultiHitPhysEffect(_unitAttacking, 1, 3, 140, _view, _teamData.abilitiesUsedCounter));
                return new SkillController(_skillData, effects, _unitAttacking, _turnsController);

            case "Nihil Claw":
                effects.Add(new MultiHitPhysEffect(_unitAttacking, 1, 3, 230, _view, _teamData.abilitiesUsedCounter));
                return new SkillController(_skillData, effects, _unitAttacking, _turnsController);

            case "Dia":
                effects.Add(new HealEffect(_unitAttacking, _teamData, _view, _skillData.power, _turnsController));
                return new SkillController(_skillData, effects, _unitAttacking, _turnsController);
            case "Diarama":
                effects.Add(new HealEffect(_unitAttacking, _teamData, _view, _skillData.power, _turnsController));
                return new SkillController(_skillData, effects, _unitAttacking, _turnsController);
            case "Diarahan":
                effects.Add(new HealEffect(_unitAttacking, _teamData, _view, _skillData.power, _turnsController));
                return new SkillController(_skillData, effects, _unitAttacking, _turnsController);

            case "Recarm":
                effects.Add(new ReviveEffect(_unitAttacking, _teamData, _view, _skillData.power, _turnsController));
                return new SkillController(_skillData, effects, _unitAttacking, _turnsController);

            case "Samarecarm":
                effects.Add(new ReviveEffect(_unitAttacking, _teamData, _view, _skillData.power, _turnsController));
                return new SkillController(_skillData, effects, _unitAttacking, _turnsController);
            
            case "Axel Claw":
                effects.Add(new MultiHitPhysEffect(_unitAttacking, 1, 3, _skillData.power, _view, _teamData.abilitiesUsedCounter));
                return new SkillController(_skillData, effects, _unitAttacking, _turnsController);
            
            case "Invitation":
                effects.Add(new SummonAndReviveEffect(_unitAttacking, _teamData, _view, _turnsController, _summonController));
                return new SkillController(_skillData, effects, _unitAttacking, _turnsController);
            
            case "Sabbatma":
                effects.Add(new SummonEffect(_unitAttacking, _teamData, _view, _turnsController, _summonController));
                return new SkillController(_skillData, effects, _unitAttacking, _turnsController);

        }

        throw new InvalidOperationException();
    }



}