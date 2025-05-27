using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei.Configs;
using System.Text.Json;
using System.IO;

public class DataLoader()
{ 
    private List<Monster> _monsters = new List<Monster>();
    private List<Samurai> _samurais = new List<Samurai>();


    public  List<Monster> GetMonsters(string filePath)
    {
        
            string json = File.ReadAllText(filePath);


            List<MonsterData> monstersData = JsonSerializer.Deserialize<List<MonsterData>>(json);
            InstantiateMonsters(monstersData);
            return _monsters;

    }

    private void InstantiateMonsters(List<MonsterData> monstersData)
    {
        foreach (MonsterData data in monstersData)
        {
            Monster newMonster = new Monster(data.name, data.stats.HP, data.stats.MP, data.stats.Str, data.stats.Skl,data.stats.Mag, data.stats.Spd, data.stats.Lck, data.affinity);
            newMonster.SetSkillsNames(data.skills);                                                 
            _monsters.Add(newMonster);
        }

    }
    

    public  List<Samurai> GetSamurais(string filePath, List<SkillData> skillsList)
    {
        
        
            string json = File.ReadAllText(filePath);

            List<SamuraiData> samuraiData = JsonSerializer.Deserialize<List<SamuraiData>>(json);

            
            InstantiateSamurais(samuraiData, skillsList);
            

            return _samurais;
        
    }

    private void InstantiateSamurais(List<SamuraiData> allSamuraisData, List<SkillData> skillsList)
    {
        foreach (SamuraiData samuraiData in allSamuraisData)
        {

            _samurais.Add(new Samurai(samuraiData.name, samuraiData.stats.HP, samuraiData.stats.MP, samuraiData.stats.Str, samuraiData.stats.Skl, 
                samuraiData.stats.Mag, samuraiData.stats.Spd, samuraiData.stats.Lck, samuraiData.affinity));
        }
    }

    
    public  List<SkillData> LoadSkills(string filePath)
    {
      
            string json = File.ReadAllText(filePath);
            List<SkillData> skills = JsonSerializer.Deserialize<List<SkillData>>(json);
            return skills;


    }
}