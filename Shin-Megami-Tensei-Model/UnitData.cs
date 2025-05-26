public abstract class UnitData
{
    public string Name { get; protected set; }
    public int HP { get; set; }
    public int MP;
    public int Strength { get; protected set; }
    public int Skill { get; protected set; }
    public int Magic { get; protected set; }
    public int Speed { get; protected set; }
    public int Luck { get; protected set; }
    public int maxHP { get; protected set; }
    public int maxMP { get; protected set; }
    public bool active { get; set; }
    public List<SkillData> Skills { get; protected set; } = new List<SkillData>();

    public Dictionary<string, string> Affinities { get; protected set; }
    
    public int speedForOrder { get; set; }


    protected UnitData(string name, int hp, int mp, int strength, int skill, int magic, int speed, int luck, Dictionary<string, string> affinities)
    {
        Name = name;
        HP = hp;
        MP = mp;
        maxMP = mp;
        maxHP = hp;
        Strength = strength;
        Skill = skill;
        Magic = magic;
        Speed = speed;
        speedForOrder = speed;
        Luck = luck;
        Affinities = affinities;
    }
    
    public void DiscountHp(int damage)
    {
        if(HP-damage <= 0)
        {
            HP = 0;

        }
        else
        {
            HP -= damage;

        }
    }

    public List<SkillData> GetUsableSkills()
    {
        List<SkillData> usableSkills = new List<SkillData>();
        foreach (SkillData skill in Skills)
        {
            if (skill.cost <= MP)
            {
                usableSkills.Add(skill);
            }
        }

        return usableSkills;
    }
    
    public abstract UnitData Clone();

    
}