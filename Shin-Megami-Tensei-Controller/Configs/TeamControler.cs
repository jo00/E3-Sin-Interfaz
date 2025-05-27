using Shin_Megami_Tensei_View;
using System.Linq;
using System.Collections.Generic;

namespace Shin_Megami_Tensei.Configs;

public class TeamController(ImplementedConsoleView implementedConsoleView, List<Samurai> samurais, List<Monster> monsters, List<SkillData> skills)
{
    private ImplementedConsoleView _implementedConsoleView = implementedConsoleView;
    private Samurai _samurai;
    private List<UnitData> _firstTeam = new List<UnitData>();
    private List<UnitData> _secondTeam = new List<UnitData>();
    private string[] _teamFilesSorted;
    private bool _isSecondTeam = false;
    private List<Samurai> _samurais = samurais;
    private List<Monster> _monsters = monsters;
    private List<SkillData> _skills = skills;
    private int _counterOfUnitsInTeam;
    private string _abecedary = "ABCD";


    public void SelectTeam(string teamsFolder)
    {
        _teamFilesSorted = _implementedConsoleView.ShowTeamsToSelect(teamsFolder);
        int choice = _implementedConsoleView.GetNumericSelectedOptionFromUser();
        string selectedTeamFile = _teamFilesSorted[choice];
        LoadTeamsFromFile(selectedTeamFile);
    }
    

    
    private void LoadTeamsFromFile(string filePath)
    {
        string[] lines = File.ReadAllLines(filePath);


        foreach (string line in lines)
        {
            IsSecondPlayerTeam(line);
            UnitData unitData = CreateUnitFromName(line);
            AddUnitToCorrespondingTeam(unitData);
            
        }

    }

    private void IsSecondPlayerTeam(string line)
    {
         if (line.Contains("Player 2 Team"))
         {
             _isSecondTeam = true;
         }
    }

    private void AddUnitToCorrespondingTeam(UnitData unitData)
    {
        if (unitData != null)
        {
            if (_isSecondTeam)
            {
                _secondTeam.Add(unitData);
                ActiveUnitIfNeeded(_secondTeam);

            }
            else
            {
                _firstTeam.Add(unitData);
                ActiveUnitIfNeeded(_firstTeam);

            }
        }
    }

    private void ActiveUnitIfNeeded(List<UnitData> team)
    {
        if (team.Count < 5)
        {
            UnitData lastUnitDataAdded = team[^1];
            lastUnitDataAdded.active = true;

        }
    }
    
   
    private UnitData CreateUnitFromName(string name)
    {
        if (name.StartsWith("[Samurai]"))
        {
            Samurai original = CreateSamuraiFromName(name);
            return original.Clone();
        }

        Monster foundMonster = _monsters.FirstOrDefault(m => m.Name == name);
        if (foundMonster != null)
        {
            
            foundMonster.SetSkills(GetSkillsFromNames(foundMonster.skillsNames.ToArray(), _skills));

            return foundMonster.Clone();
        }

        return null;
    }

    private Samurai CreateSamuraiFromName(string name)
    {
        string[] parts = name.Split(new[] { '[', ']', '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
        string samuraiName = parts[1].Trim();
        string[] skillNames = GetSamuraiSkillsNames(parts);

        Samurai foundSamurai = _samurais.FirstOrDefault(samurai => samurai.Name == samuraiName);
        foundSamurai.SetSkills(GetSkillsFromNames(skillNames, _skills));

        return foundSamurai;
    }

    private string[] GetSamuraiSkillsNames(string[] parts)
    {
        string[] skillNames;
        if (parts.Length > 2)
        {
            skillNames = parts[2].Split(',');
        }
        else
        {
            skillNames = Array.Empty<string>();
        }

        return skillNames;
    }
    
    private List<SkillData> GetSkillsFromNames(string[] skillNames, List<SkillData> allSkills)
    {
        var selectedSkills = new List<SkillData>();
        foreach (string skillName in skillNames)
        {
            string trimmedName = skillName.Trim();
            SkillData foundSkillData = allSkills.FirstOrDefault(skill => skill.name == trimmedName);
        
            if (foundSkillData != null)
            {
                
                selectedSkills.Add(foundSkillData);
                
            }
        }

        return selectedSkills;
    }


    public bool AreBoothTeamsValid()
    {
        if (IsTeamValid(_firstTeam) && IsTeamValid(_secondTeam))
        {
            return true;
        }

        
        _implementedConsoleView.AnounceThatTeamIsInvalid();
        return false;
        
    }

    private bool IsTeamValid(List<UnitData> units)
    {
        List<string> namesOfUnitsInTeam = units.Select(unit => unit.Name).ToList();

        if (units.Count > 8)
        {
            return false;   
        }
        
        if (units.Count(unit => unit is Samurai) != 1)
        {
            return false;

        }
        
        if (namesOfUnitsInTeam.Count != namesOfUnitsInTeam.Distinct().Count())
        {
            return false;
        }
        
        _samurai = units.FirstOrDefault(unit => unit is Samurai) as Samurai;

        if (_samurai != null)
        {
            return IsSamuraiValid();
        }
        
        return true;
    }

    private bool IsSamuraiValid()
    {
        if (!HasSamuraiDuplicateSkills(_samurai))
        {
            return false;
        }

        if (_samurai.Skills.Count > 8)
        {
            return false;
        }

        return true;
    }

    
    
    private bool HasSamuraiDuplicateSkills(Samurai samurai)
    {
        return !samurai.Skills
            .GroupBy(skill => skill.name) 
            .Any(group => group.Count() > 1); 
    }
    
    public List<UnitData> GetFirstTeam()
    {
        return _firstTeam;
    }
    
    public List<UnitData> GetSecondTeam()
    {
        return _secondTeam;
    }
    
    public Samurai GetSamuraiFromFirstTeam()
    {
        return _firstTeam.FirstOrDefault(unit => unit is Samurai) as Samurai;
    }

    public Samurai GetSamuraiFromSecondTeam()
    {
        return _secondTeam.FirstOrDefault(unit => unit is Samurai) as Samurai;
    }
   




    
    private int CalculateHowManyUnitsShouldBePrinted(List<UnitData> team)
    {
        if (team.Count < 4)
        {
            return team.Count;
        }

        return 4;
    }

  

    public List<UnitData> GetActiveUnitsAlive(List<UnitData> team)
    {
        List<UnitData> activeUnitsAlive = new List<UnitData>();
        foreach (UnitData unit in team)
        {
            if (unit.active && unit.HP>0)
            {
                activeUnitsAlive.Add(unit);
            }
        }

        return activeUnitsAlive;
    }
    
    public List<UnitData> GetDeadUnits(List<UnitData> team)
    {
        List<UnitData> deadUnits = new List<UnitData>();
        foreach (UnitData unit in team)
        {
            if (unit.HP<=0)
            {
                Console.WriteLine(unit.Name);
                deadUnits.Add(unit);
            }
        }

        return deadUnits;
    }
    
    public List<UnitData> GetActiveUnits(List<UnitData> team)
    {
        List<UnitData> activeUnits = new List<UnitData>();
        foreach (UnitData unit in team)
        {
            if (unit.active)
            {
                activeUnits.Add(unit);
            }
        }

        return activeUnits;
    }

    public List<UnitData> GetActiveUnitsInOrderOfAction(List<UnitData> activeUnits, List<UnitData> unitsThatAlreadyPlayed)
    {
       

        List<UnitData> sortedUnits = activeUnits.OrderByDescending(unit => unit.speedForOrder).ToList();
    
        foreach (UnitData unit in unitsThatAlreadyPlayed)
        {
            sortedUnits.Remove(unit);
            if (unit.active && unit.HP>0)
            {
                sortedUnits.Add(unit);

            }
        }

        

        return sortedUnits;

    }
    
    
    public bool CanTeamKeepPlaying(List<UnitData> team)
    {
        foreach (UnitData unit in GetActiveUnitsAlive(team))
        {
            if (unit.HP > 0)
            {
                return true;
            }
        }

        return false;
    }

    public UnitData GetMonsterToSummonFromBench(List<UnitData> team)
    {
        return _implementedConsoleView.SelectMonsterToSummonMenu(team);

    }

    public UnitData GetMonsterToSummonFromBenchWhenItCanBeDead(List<UnitData> team)
    {
        return _implementedConsoleView.GetMonsterToSummonFromBenchWhenItCanBeDead(team);
    }

    public void ChangeUnitsWhenSummonIsMade(UnitData unitDataThatMadeTheSummon, UnitData unitDataToGetOut, UnitData unitDataToGetIn, TeamData teamData)
    {
        int unitToGetOutIndex = FindUnitIndexInTeam(unitDataToGetOut, teamData.team);
        int unitToGetInIndex = FindUnitIndexInTeam(unitDataToGetIn, teamData.team);


        unitDataToGetOut.active = false;
        unitDataToGetOut.speedForOrder = unitDataToGetOut.Speed;
        teamData.team[unitToGetOutIndex] = unitDataToGetIn;
        teamData.team[unitToGetInIndex] = unitDataToGetOut;
        teamData.team[unitToGetOutIndex].active = true;
        teamData.team[unitToGetOutIndex].speedForOrder = unitDataToGetOut.speedForOrder;
        if (unitDataToGetOut.HP <= 0 || unitDataToGetOut == unitDataToGetIn|| unitDataThatMadeTheSummon==unitDataToGetOut)
        {
            teamData.team[unitToGetOutIndex].speedForOrder = 0;
            unitDataToGetIn.speedForOrder = 0;
            teamData.teamUnitsThatAlreadyPlayed.Add(teamData.team[unitToGetOutIndex]);
        }

        if(teamData.teamUnitsThatAlreadyPlayed.Contains(unitDataToGetOut))
        {
            int indexInPlayedUnits= FindUnitIndexInTeam(unitDataToGetOut, teamData.teamUnitsThatAlreadyPlayed);
            teamData.teamUnitsThatAlreadyPlayed[indexInPlayedUnits]= unitDataToGetIn;
        }
        teamData.team = ReOrderTeamAfterSummon(teamData.team, teamData.originalTeamOrder);
        
    }
    
    
    public int FindUnitIndexInTeam(UnitData unitData, List<UnitData> team)
    {
        for (int i = 0; i < team.Count; i++)
        {
            if (team[i] == unitData)
            {
                return i;
            }
        }

        return -1; 
    }

   
    
    public List<UnitData> ReOrderTeamAfterSummon(List<UnitData> team, List<UnitData> originalTeamOrder)
    {
        List<UnitData> ordered = new List<UnitData>();

        UnitData samurai = team.FirstOrDefault(unit => unit is Samurai);
        ordered.Add(samurai);
        

        ordered.AddRange(
            team.Where(unit => unit.active && !(unit is Samurai))
        );

        ordered.AddRange(
            originalTeamOrder.Where(unit => !unit.active && !(unit is Samurai))
        );

        
        return ordered;
    }
    
    
    
    public List<string> GetTeamDisplayLines(TeamData teamData)
    {
        var lines = new List<string>();
        string lineToAdd = $"Equipo de {teamData.samurai.Name} (J{teamData.playerNumber})";

        lines.Add(lineToAdd);

        lines.AddRange(GetTeamLines(teamData.team));

        return lines;
    }

    private List<string> GetTeamLines(List<UnitData> team)
    {
        _counterOfUnitsInTeam = 0;

        var lines = new List<string>();

        lines.AddRange(GetUnitLines(team));
        lines.AddRange(GetEmptySpaceLines());

        return lines;
    }

    private List<string> GetUnitLines(List<UnitData> team)
    {
        var lines = new List<string>();
        int maxNumberOfUnitsToShow = CalculateHowManyUnitsShouldBePrinted(team);

        for (int i = 0; i < maxNumberOfUnitsToShow; i++)
        {
            UnitData unitData = team[i];

            if ((unitData.HP > 0 && unitData.active) || unitData is Samurai)
            {
                lines.Add($"{_abecedary[i]}-{unitData.Name} HP:{unitData.HP}/{unitData.maxHP} MP:{unitData.MP}/{unitData.maxMP}");
            }
            else
            {
                lines.Add($"{_abecedary[i]}-");
            }

            _counterOfUnitsInTeam += 1;
        }

        return lines;
    }

    private List<string> GetEmptySpaceLines()
    {
        var lines = new List<string>();

        for (int i = _counterOfUnitsInTeam; i < 4; i++)
        {
            lines.Add($"{_abecedary[i]}-");
        }

        return lines;
    }


    
        
    
    

}