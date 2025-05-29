using Shin_Megami_Tensei_View;
using Shin_Megami_Tensei.Configs;
using Shin_Megami_Tensei.Skills.Effects;
using Shin_Megami_Tensei.Skills.Effects.NonOffensiveEffects.HealEffects;
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
                effects.Add(new DoublePhysOrGunEffect(_unitDataAttacking, _teamData, _skillData.power, _turnsController, _view, _teamController));
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
            case "Dark Energy":
                effects.Add(new DoublePhysOrGunEffect(_unitDataAttacking, _teamData, _skillData.power, _turnsController, _view, _teamController));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);
            case "Hama":
                effects.Add(new InstaKillEffect(_unitDataAttacking, _skillData.power, _view, "Light"));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);
            case "Hamaon":
                effects.Add(new InstaKillEffect(_unitDataAttacking, _skillData.power, _view, "Light"));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);
            case "Mudo":
                effects.Add(new InstaKillEffect(_unitDataAttacking, _skillData.power, _view, "Dark"));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);
            case "Mudoon":
                effects.Add(new InstaKillEffect(_unitDataAttacking, _skillData.power, _view, "Dark"));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);
            case "Mahama":
                effects.Add(new AllTargetInstaKillEffect(_unitDataAttacking, _skillData.power, _view, "Light", _teamController));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);
            case "Mahamaon":
                effects.Add(new AllTargetInstaKillEffect(_unitDataAttacking, _skillData.power, _view, "Light", _teamController));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);

            case "Judgement Light":
                effects.Add(new AllTargetInstaKillEffect(_unitDataAttacking, _skillData.power, _view, "Light", _teamController));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);

            case "Mamudo":
                effects.Add(new AllTargetInstaKillEffect(_unitDataAttacking, _skillData.power, _view, "Dark", _teamController));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);

            case "Mamudoon":
                effects.Add(new AllTargetInstaKillEffect(_unitDataAttacking, _skillData.power, _view, "Dark", _teamController));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);

            case "Die for Me!":
                effects.Add(new AllTargetInstaKillEffect(_unitDataAttacking, _skillData.power, _view, "Dark", _teamController));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);

            case "Critical Wave":
                effects.Add(new AllTargetPhysEffect(_unitDataAttacking, _skillData.power, _view, _teamController));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);
            
            case "Megaton Press":
                effects.Add(new AllTargetPhysEffect(_unitDataAttacking, _skillData.power, _view, _teamController));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);
            
            case "Titanomachia":
                effects.Add(new AllTargetPhysEffect(_unitDataAttacking, _skillData.power, _view, _teamController));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);
            
            case "Heat Wave":
                effects.Add(new AllTargetPhysEffect(_unitDataAttacking, _skillData.power, _view, _teamController));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);
            
            case "Javelin Rain":
                effects.Add(new AllTargetPhysEffect(_unitDataAttacking, _skillData.power, _view, _teamController));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);
            
            case "Hades Blast":
                effects.Add(new AllTargetPhysEffect(_unitDataAttacking, _skillData.power, _view, _teamController));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);
            
            case "Scratch Dance":
                effects.Add((new MultiTargetPhysEffect(_unitDataAttacking, 1, 3, _skillData.power, _view, _teamData.abilitiesUsedCounter, _teamController)));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);
            case "Madness Nails":
                effects.Add((new MultiTargetPhysEffect(_unitDataAttacking, 1, 3, _skillData.power, _view, _teamData.abilitiesUsedCounter, _teamController)));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);
            case "Bar Toss":
                effects.Add((new MultiTargetPhysEffect(_unitDataAttacking, 1, 3, _skillData.power, _view, _teamData.abilitiesUsedCounter, _teamController)));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);
            
            case "Maragi":
                effects.Add((new AllTargetMagicEffect(_unitDataAttacking, _skillData.power, _view, _teamController, "Fire")));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);

            case "Maragion":
                effects.Add(new AllTargetMagicEffect(_unitDataAttacking, _skillData.power, _view, _teamController, "Fire"));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);

            case "Maragidyne":
                effects.Add(new AllTargetMagicEffect(_unitDataAttacking, _skillData.power, _view, _teamController, "Fire"));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);

            case "Mabufu":
                effects.Add(new AllTargetMagicEffect(_unitDataAttacking, _skillData.power, _view, _teamController, "Ice"));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);

            case "Mabufula":
                effects.Add(new AllTargetMagicEffect(_unitDataAttacking, _skillData.power, _view, _teamController, "Ice"));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);

            case "Mabufudyne":
                effects.Add(new AllTargetMagicEffect(_unitDataAttacking, _skillData.power, _view, _teamController, "Ice"));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);

            case "Mazio":
                effects.Add(new AllTargetMagicEffect(_unitDataAttacking, _skillData.power, _view, _teamController, "Elec"));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);

            case "Mazionga":
                effects.Add(new AllTargetMagicEffect(_unitDataAttacking, _skillData.power, _view, _teamController, "Elec"));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);

            case "Maziodyne":
                effects.Add(new AllTargetMagicEffect(_unitDataAttacking, _skillData.power, _view, _teamController, "Elec"));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);

            case "Mazan":
                effects.Add(new AllTargetMagicEffect(_unitDataAttacking, _skillData.power, _view, _teamController, "Force"));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);

            case "Mazanma":
                effects.Add(new AllTargetMagicEffect(_unitDataAttacking, _skillData.power, _view, _teamController, "Force"));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);

            case "Mazandyne":
                effects.Add(new AllTargetMagicEffect(_unitDataAttacking, _skillData.power, _view, _teamController, "Force"));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);
            case "Rapid Needle":
                effects.Add(new AllTargetGunEffect(_unitDataAttacking, _skillData.power, _view, _teamController));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);

            case "Blast Arrow":
                effects.Add(new AllTargetGunEffect(_unitDataAttacking, _skillData.power, _view, _teamController));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);

            case "Heaven's Bow":
                effects.Add(new AllTargetGunEffect(_unitDataAttacking, _skillData.power, _view, _teamController));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);
            case "Fire Breath":
                effects.Add(new MultiTargetMagicEffect(_unitDataAttacking, 1, 4, _skillData.power, _view, _teamController, "Fire", _teamData.abilitiesUsedCounter));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);

            case "Ragnarok":
                effects.Add(new MultiTargetMagicEffect(_unitDataAttacking, 1, 4, _skillData.power, _view, _teamController, "Fire", _teamData.abilitiesUsedCounter));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);

            case "Ice Breath":
                effects.Add(new MultiTargetMagicEffect(_unitDataAttacking, 1, 4, _skillData.power, _view, _teamController, "Ice", _teamData.abilitiesUsedCounter));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);

            case "Glacial Blast":
                effects.Add(new MultiTargetMagicEffect(_unitDataAttacking, 1, 4, _skillData.power, _view, _teamController, "Ice", _teamData.abilitiesUsedCounter));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);

            case "Breath":
                effects.Add(new MultiTargetMagicEffect(_unitDataAttacking, 1, 5, _skillData.power, _view, _teamController, "Ice", _teamData.abilitiesUsedCounter));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);

            case "Refrigerate":
                effects.Add(new MultiTargetMagicEffect(_unitDataAttacking, 1, 8, _skillData.power, _view, _teamController, "Ice", _teamData.abilitiesUsedCounter));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);

            case "Shock":
                effects.Add(new MultiTargetMagicEffect(_unitDataAttacking, 1, 4, _skillData.power, _view, _teamController, "Elec", _teamData.abilitiesUsedCounter));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);

            case "Plasma Discharge":
                effects.Add(new MultiTargetMagicEffect(_unitDataAttacking, 1, 8, _skillData.power, _view, _teamController, "Elec", _teamData.abilitiesUsedCounter));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);

            case "Wind Breath":
                effects.Add(new MultiTargetMagicEffect(_unitDataAttacking, 1, 4, _skillData.power, _view, _teamController, "Force", _teamData.abilitiesUsedCounter));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);

            case "Floral Gust":
                effects.Add(new MultiTargetMagicEffect(_unitDataAttacking, 1, 4, _skillData.power, _view, _teamController, "Force", _teamData.abilitiesUsedCounter));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);
            
            case "Myriad Arrows":
                effects.Add((new MultiTargetGunEffect(_unitDataAttacking, 1, 3, _skillData.power, _view, _teamData.abilitiesUsedCounter, _teamController)));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);
            
            case "Media":
                effects.Add(new AllTargetHealEffect(_unitDataAttacking,  _skillData.power,  _view, _teamController,_teamData));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);
            case "Mediarama":
                effects.Add(new AllTargetHealEffect(_unitDataAttacking,  _skillData.power,  _view, _teamController,_teamData));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);
            case "Mediarahan":
                effects.Add(new AllTargetHealEffect(_unitDataAttacking,  _skillData.power,  _view, _teamController,_teamData));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);
            case "Recarmdra":
                effects.Add(new AllTargetHealEffect(_unitDataAttacking,  _skillData.power,  _view, _teamController,_teamData));
                return new SkillController(_skillData, effects, _unitDataAttacking, _turnsController);

            






        }

        throw new InvalidOperationException();
    }



}