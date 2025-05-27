public class Samurai : UnitData
{

    public Samurai(string name, int hp, int mp, int strength, int skill, int magic, int speed, int luck, Dictionary<string, string> affinities)
        : base(name, hp, mp, strength, skill, magic, speed, luck, affinities)
    {

    }
    public override UnitData Clone()
    {
        Samurai clone = new Samurai(Name, maxHP, maxMP, Strength, Skill, Magic, Speed, Luck, new Dictionary<string, string>(Affinities));
        clone.SetSkills(new List<SkillData>(Skills)); 
        return clone;
    }

    
    public void SetSkills(List<SkillData> skills)
    {
        
        Skills = skills;
       
    }


}