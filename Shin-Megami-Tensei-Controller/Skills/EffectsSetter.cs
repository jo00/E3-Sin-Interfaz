using Shin_Megami_Tensei_View;
using Shin_Megami_Tensei.Configs;
using Shin_Megami_Tensei.Skills.Effects;
using Shin_Megami_Tensei.Skills.Effects.OfensiveEffects;
using Shin_Megami_Tensei.Skills.Effects.SuppportEffects;

namespace Shin_Megami_Tensei.Skills;

public class EffectsSetter
{

    private SkillData _skillData;
    private UnitData _unitDataAttacking;
    private ImplementedConsoleView _view;
    private TurnsController _turnsController;
    private TeamData _teamData;
    private SummonController _summonController;
    private TeamController _teamController;
    public EffectsSetter(SkillData skillData, UnitData unitDataAttacking, ImplementedConsoleView view, TurnsController turnsController, TeamData teamData, SummonController summonController, TeamController teamController)
    {
        _skillData = skillData;
        _unitDataAttacking = unitDataAttacking;
        _view = view;
        _turnsController = turnsController;
        _teamData = teamData;
        _summonController = summonController;
        _teamController = teamController;
    }
    public SkillController SetEffectsForSkill()
    {
        string skillName = _skillData.name;
        List<Effect> effects = new List<Effect>();
        switch (skillName)
        {
            case "Needle Shot":
                effects.Add(new GunEffect(_unitDataAttacking, _skillData.power, _view));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);
            case "Tathlum Shot":
                effects.Add(new GunEffect(_unitDataAttacking, _skillData.power, _view));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);
            case "Grand Tack":
                effects.Add(new GunEffect(_unitDataAttacking, _skillData.power, _view));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);

            case "Riot Gun":
                effects.Add(new GunEffect(_unitDataAttacking, _skillData.power, _view));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);

            case "Lunge":
                effects.Add(new PhysEffect(_unitDataAttacking, _skillData.power, _view));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);

            case "Oni-Kagura":
                effects.Add(new PhysEffect(_unitDataAttacking, _skillData.power, _view));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);

            case "Mortal Jihad":
                effects.Add(new PhysEffect(_unitDataAttacking, _skillData.power, _view));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);

            case "Gram Slice":
                effects.Add(new PhysEffect(_unitDataAttacking, _skillData.power, _view));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);

            case "Fatal Sword":
                effects.Add(new PhysEffect(_unitDataAttacking, _skillData.power, _view));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);

            case "Berserker God":
                effects.Add(new PhysEffect(_unitDataAttacking, _skillData.power, _view));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);

            case "Iron Judgement":
                effects.Add(new PhysEffect(_unitDataAttacking, _skillData.power, _view));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);

            case "Stigma Attack":
                effects.Add(new PhysEffect(_unitDataAttacking, _skillData.power, _view));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);

            case "Agi":
                effects.Add(new FireEffect(_unitDataAttacking, _skillData.power, _view));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);

            case "Agilao":
                effects.Add(new FireEffect(_unitDataAttacking, _skillData.power, _view));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);

            case "Agidyne":
                effects.Add(new FireEffect(_unitDataAttacking, _skillData.power, _view));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);

            case "Trisagion":
                effects.Add(new FireEffect(_unitDataAttacking, _skillData.power, _view));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);

            case "Bufu":
                effects.Add(new IceEffect(_unitDataAttacking, _skillData.power, _view));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);

            case "Bufula":
                effects.Add(new IceEffect(_unitDataAttacking, _skillData.power, _view));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);

            case "Bufudyne":
                effects.Add(new IceEffect(_unitDataAttacking, _skillData.power, _view));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);

            case "Zio":
                effects.Add(new ElecEffect(_unitDataAttacking, _skillData.power, _view));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);

            case "Zionga":
                effects.Add(new ElecEffect(_unitDataAttacking, _skillData.power, _view));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);

            case "Ziodyne":
                effects.Add(new ElecEffect(_unitDataAttacking, _skillData.power, _view));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);

            case "Zan":
                effects.Add(new ForceEffect(_unitDataAttacking, _skillData.power, _view));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);

            case "Zanma":
                effects.Add(new ForceEffect(_unitDataAttacking, _skillData.power, _view));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);

            case "Zandyne":
                effects.Add(new ForceEffect(_unitDataAttacking, _skillData.power, _view));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);

            case "Deadly Wind":
                effects.Add(new ForceEffect(_unitDataAttacking, _skillData.power, _view));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);

            case "Bouncing Claw":
                effects.Add(new MultiHitPhysEffect(_unitDataAttacking, 1, 3, 80, _view, _teamData.abilitiesUsedCounter));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);

            case "Damascus Claw":
                effects.Add(new MultiHitPhysEffect(_unitDataAttacking, 1, 3, 140, _view, _teamData.abilitiesUsedCounter));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);

            case "Nihil Claw":
                effects.Add(new MultiHitPhysEffect(_unitDataAttacking, 1, 3, 230, _view, _teamData.abilitiesUsedCounter));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);

            case "Dia":
                effects.Add(new HealEffect(_unitDataAttacking, _teamData,  _skillData.power, _turnsController, _view, _teamController));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);
            case "Diarama":
                effects.Add(new HealEffect(_unitDataAttacking, _teamData,  _skillData.power, _turnsController, _view, _teamController));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);
            case "Diarahan":
                effects.Add(new HealEffect(_unitDataAttacking, _teamData,  _skillData.power, _turnsController, _view, _teamController));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);

            case "Recarm":
                effects.Add(new ReviveEffect(_unitDataAttacking, _teamData, _skillData.power, _turnsController, _view, _teamController));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);

            case "Samarecarm":
                effects.Add(new ReviveEffect(_unitDataAttacking, _teamData,  _skillData.power, _turnsController, _view, _teamController));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);
            
            case "Axel Claw":
                effects.Add(new MultiHitPhysEffect(_unitDataAttacking, 1, 3, _skillData.power, _view, _teamData.abilitiesUsedCounter));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);
            
            case "Invitation":
                effects.Add(new SummonAndReviveEffect(_unitDataAttacking, _teamData, _turnsController, _summonController));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);
            
            case "Sabbatma":
                effects.Add(new SummonEffect(_unitDataAttacking, _teamData, _turnsController, _summonController));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);
            
            case "Charge":
                effects.Add(new ChargeEffect(_unitDataAttacking, _teamData, _skillData.power, _turnsController, _view, _teamController));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);
            
            case "Concentrate":
                effects.Add(new DoublesMagicEffect(_unitDataAttacking, _teamData, _skillData.power, _turnsController, _view, _teamController));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);
            
            case "Blood Ritual":
                effects.Add(new BloodRitual(_unitDataAttacking, _teamData, _skillData.power, _turnsController, _view, _teamController));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);
            case "Gather Spirit Energy":
                effects.Add(new DoublesMagicEffect(_unitDataAttacking, _teamData, _skillData.power, _turnsController, _view, _teamController));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);

        }

        throw new InvalidOperationException();
    }



}