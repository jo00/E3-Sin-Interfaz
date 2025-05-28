public abstract class UnitData
{
    public string Name { get; protected set; }
    public int HP { get; set; }
    public int MP;
    public double Strength { get; set; }
    public double Skill { get; set; }
    public double Magic { get; set; }
    public int Speed { get; protected set; }
    public int Luck { get; protected set; }
    public int maxHP { get; protected set; }
    public int maxMP { get; protected set; }
    public bool active { get; set; }
    
    public double originalStrength { get; set; }
    public double originalSkill { get; set; }
    
    public double originalMagic { get; set; }
  
    public List<SkillData> Skills { get; protected set; } = new List<SkillData>();

    public Dictionary<string, string> Affinities { get; protected set; }
    
    public int speedForOrder { get; set; }

    public bool shouldDiscountAttackAfterApplyingIt;
    public bool incrementMagic=false;

    public int defensiveDegree = 0;
    public int offensiveDegree = 0;
    public Dictionary<int, double> defensiveMultipliers;
    public Dictionary<int, double> offensiveMultipliers;

    protected UnitData(string name, int hp, int mp, double strength, double skill, double magic, int speed, int luck, Dictionary<string, string> affinities)
    {
        Name = name;
        HP = hp;
        MP = mp;
        maxMP = mp;
        maxHP = hp;
        Strength = strength;
        originalStrength = strength;
        originalSkill = skill;
        Skill = skill;
        Magic = magic;
        originalMagic = magic;
        Speed = speed;
        speedForOrder = speed;
        Luck = luck;
        Affinities = affinities;
        shouldDiscountAttackAfterApplyingIt = false;
        defensiveMultipliers = new Dictionary<int, double>
        {
            {-3, 1.75},
            {-2, 1.5},
            {-1, 1.25},
            {0, 1},
            {1, 0.875},
            {2 ,0.75},
            {3, 0.625}

        };
        
        offensiveMultipliers = new Dictionary<int, double>
        {
            {-3, 0.625},
            {-2, 0.75},
            {-1, 0.875},
            {0, 1},
            {1, 1.25},
            {2 ,1.5},
            {3, 1.75}

        };
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