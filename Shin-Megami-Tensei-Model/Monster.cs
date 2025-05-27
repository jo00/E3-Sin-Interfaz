public class Monster : UnitData
{
    public List<string> skillsNames;
    public Monster(string name, int hp, int mp, double strength, double skill, double magic, int speed, int luck, Dictionary<string, string> affinities)
        : base(name, hp, mp, strength, skill, magic, speed, luck, affinities) { }
    
    public override UnitData Clone()
    {
        Monster newMonster = new Monster(Name, maxHP, maxMP, Strength, Skill, Magic, Speed, Luck, new Dictionary<string, string>(Affinities));
        newMonster.SetSkills(new List<SkillData>(Skills));
        return newMonster;
    }
    
    public void SetSkillsNames(List<string> skillsNames)
    {
        this.skillsNames = skillsNames;
    }

 

    public void SetSkills(List<SkillData> skills)
    {
        Skills = skills;
    }
    

}
