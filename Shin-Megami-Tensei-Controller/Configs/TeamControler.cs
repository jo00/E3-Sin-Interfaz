using Shin_Megami_Tensei_View;
using System.Linq;
using System.Collections.Generic;

namespace Shin_Megami_Tensei.Configs;

public class TeamController(View view, List<Samurai> samurais, List<Monster> monsters, List<SkillData> skills)
{
    private View _view = view;
    private Samurai _samurai;
    private List<Unit> _firstTeam = new List<Unit>();
    private List<Unit> _secondTeam = new List<Unit>();
    private string[] _teamFilesSorted;
    private bool _isSecondTeam = false;
    private List<Samurai> _samurais = samurais;
    private List<Monster> _monsters = monsters;
    private List<SkillData> _skills = skills;
    private int _counterOfUnitsInTeam;
    private string _abecedary = "ABCD";


    public void SelectTeam(string teamsFolder)
    {
        _view.WriteLine("Elige un archivo para cargar los equipos");
        ShowTeamOptions(teamsFolder);
        int choice = int.Parse(_view.ReadLine());
        string selectedTeamFile = _teamFilesSorted[choice];
        LoadTeamsFromFile(selectedTeamFile);
    }
    private void ShowTeamOptions(string teamsFolder)
    {
        string[] teamFiles = Directory.GetFiles(teamsFolder, "*.txt");
        
        List<Tuple<string, int>> sortedTeamFiles = SortTeamFiles(teamFiles);
        sortedTeamFiles = sortedTeamFiles.OrderBy(t => t.Item2).ToList();

        _teamFilesSorted = sortedTeamFiles.Select(t => t.Item1).ToArray();

        ShowFilesSorted();
    }

    private List<Tuple<string, int>> SortTeamFiles(string[] teamFiles)
    {
        List<Tuple<string, int>> sortedTeamFiles = new List<Tuple<string, int>>();

        foreach (string filePath in teamFiles)
        {
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            int number;
            int.TryParse(fileName, out number);
            sortedTeamFiles.Add(new Tuple<string, int>(filePath, number));

        }

        return sortedTeamFiles;
    }

    private void ShowFilesSorted()
    {
        for (int i = 0; i < _teamFilesSorted.Length; i++)
        {
            _view.WriteLine($"{i}: {Path.GetFileName(_teamFilesSorted[i])}");
        }
    }

    
    private void LoadTeamsFromFile(string filePath)
    {
        string[] lines = File.ReadAllLines(filePath);


        foreach (string line in lines)
        {
            IsSecondPlayerTeam(line);
            Unit unit = CreateUnitFromName(line);
            AddUnitToCorrespondingTeam(unit);
            
        }

    }

    private void IsSecondPlayerTeam(string line)
    {
         if (line.Contains("Player 2 Team"))
         {
             _isSecondTeam = true;
         }
    }

    private void AddUnitToCorrespondingTeam(Unit unit)
    {
        if (unit != null)
        {
            if (_isSecondTeam)
            {
                _secondTeam.Add(unit);
                ActiveUnitIfNeeded(_secondTeam);

            }
            else
            {
                _firstTeam.Add(unit);
                ActiveUnitIfNeeded(_firstTeam);

            }
        }
    }

    private void ActiveUnitIfNeeded(List<Unit> team)
    {
        if (team.Count < 5)
        {
            Unit lastUnitAdded = team[^1];
            lastUnitAdded.active = true;

        }
    }
    
   
    private Unit CreateUnitFromName(string name)
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

        else
        {
            _view.WriteLine("Archivo de equipos inválido");
            return false;

        }
        
    }

    private bool IsTeamValid(List<Unit> units)
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
    
    public List<Unit> GetFirstTeam()
    {
        return _firstTeam;
    }
    
    public List<Unit> GetSecondTeam()
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
    
    public void ShowTeams(TeamData teamData)
    {

        _view.WriteLine($"Equipo de {teamData.samurai.Name} (J{teamData.playerNumber})");

        ShowTeam(teamData.team);

    }

    private void ShowTeam(List<Unit> team)
    {
        
        _counterOfUnitsInTeam = 0;
        ShowUnits(team);
        ShowEmptySpaces();
        
       
        
    }

    private void ShowUnits(List<Unit> team)
    {
        int maxNumberOfUnitsToShow = CalculateHowManyUnitsShouldBePrinted(team);
        for (int i = 0; i < maxNumberOfUnitsToShow; i++)
        {
            Unit unit = team[i];
            if ((unit.HP > 0) && unit.active || unit is Samurai)
            {
                _view.WriteLine($"{_abecedary[i]}-{unit.Name} HP:{unit.HP}/{unit.maxHP} MP:{unit.MP}/{unit.maxMP}");

            }
            else
            {
                _view.WriteLine($"{_abecedary[i]}-");
            }
            _counterOfUnitsInTeam += 1;
        }

    }
    
    private int CalculateHowManyUnitsShouldBePrinted(List<Unit> team)
    {
        if (team.Count < 4)
        {
            return team.Count;
        }

        return 4;
    }

    private void ShowEmptySpaces()
    {
        if (_counterOfUnitsInTeam < 4)
        {
            for (int i = _counterOfUnitsInTeam; i < 4; i++)
            {
                _view.WriteLine($"{_abecedary[i]}-");
            }
        }
    }

    public List<Unit> GetActiveUnitsAlive(List<Unit> team)
    {
        List<Unit> activeUnitsAlive = new List<Unit>();
        foreach (Unit unit in team)
        {
            if (unit.active && unit.HP>0)
            {
                activeUnitsAlive.Add(unit);
            }
        }

        return activeUnitsAlive;
    }
    
    public List<Unit> GetDeadUnits(List<Unit> team)
    {
        List<Unit> deadUnits = new List<Unit>();
        foreach (Unit unit in team)
        {
            if (unit.HP<=0)
            {
                Console.WriteLine(unit.Name);
                deadUnits.Add(unit);
            }
        }

        return deadUnits;
    }
    
    public List<Unit> GetActiveUnits(List<Unit> team)
    {
        List<Unit> activeUnits = new List<Unit>();
        foreach (Unit unit in team)
        {
            if (unit.active)
            {
                activeUnits.Add(unit);
            }
        }

        return activeUnits;
    }

    public List<Unit> GetActiveUnitsInOrderOfAction(List<Unit> activeUnits, List<Unit> unitsThatAlreadyPlayed)
    {
       

        List<Unit> sortedUnits = activeUnits.OrderByDescending(unit => unit.speedForOrder).ToList();
    
        foreach (Unit unit in unitsThatAlreadyPlayed)
        {
            sortedUnits.Remove(unit);
            if (unit.active && unit.HP>0)
            {
                sortedUnits.Add(unit);

            }
        }

        

        return sortedUnits;

    }
    
    
    public bool CanTeamKeepPlaying(List<Unit> team)
    {
        foreach (Unit unit in GetActiveUnitsAlive(team))
        {
            if (unit.HP > 0)
            {
                return true;
            }
        }

        return false;
    }

    public Unit GetMonsterToSummonFromBench(List<Unit> team)
    {
        _view.WriteLine("----------------------------------------");
        _view.WriteLine("Seleccione un monstruo para invocar");
        int counter = 1;
        List<Unit> unitsToSummon = new List<Unit>();
        foreach (Unit unit in team)
        {
            if (!unit.active && unit.HP > 0 && unit is not Samurai)

            {
                _view.WriteLine($"{counter}-{unit.Name} HP:{unit.HP}/{unit.maxHP} MP:{unit.MP}/{unit.maxMP}");
                unitsToSummon.Add(unit);
                counter++;
            }

        }
        _view.WriteLine($"{counter}-Cancelar");
        int selectedMonsterIndex = Convert.ToInt32(_view.ReadLine());
        if (selectedMonsterIndex == counter)
        {
            return null;
        }
        return unitsToSummon[selectedMonsterIndex - 1];

    }

    public Unit GetMonsterToSummonFromBenchWhenItCanBeDead(List<Unit> team)
    {
        _view.WriteLine("----------------------------------------");
        _view.WriteLine("Seleccione un monstruo para invocar");
        int counter = 1;
        List<Unit> unitsToSummon = new List<Unit>();
        foreach (Unit unit in team)
        {
            if ((!unit.active || unit.HP<=0) && unit is not Samurai)
            {
                _view.WriteLine($"{counter}-{unit.Name} HP:{unit.HP}/{unit.maxHP} MP:{unit.MP}/{unit.maxMP}");
                unitsToSummon.Add(unit);
                counter++;
            }

        }
        _view.WriteLine($"{counter}-Cancelar");
        int selectedMonsterIndex = Convert.ToInt32(_view.ReadLine());
        if (selectedMonsterIndex == counter)
        {
            return null;
        }
        return unitsToSummon[selectedMonsterIndex - 1];
    }

    public void ChangeUnitsWhenSummonIsMade(Unit unitThatMadeTheSummon, Unit unitToGetOut, Unit unitToGetIn, TeamData teamData)
    {
        int unitToGetOutIndex = FindUnitIndexInTeam(unitToGetOut, teamData.team);
        int unitToGetInIndex = FindUnitIndexInTeam(unitToGetIn, teamData.team);


        unitToGetOut.active = false;
        unitToGetOut.speedForOrder = unitToGetOut.Speed;
        teamData.team[unitToGetOutIndex] = unitToGetIn;
        teamData.team[unitToGetInIndex] = unitToGetOut;
        teamData.team[unitToGetOutIndex].active = true;
        teamData.team[unitToGetOutIndex].speedForOrder = unitToGetOut.speedForOrder;
        if (unitToGetOut.HP <= 0 || unitToGetOut == unitToGetIn|| unitThatMadeTheSummon==unitToGetOut)
        {
            teamData.team[unitToGetOutIndex].speedForOrder = 0;
            unitToGetIn.speedForOrder = 0;
            teamData.teamUnitsThatAlreadyPlayed.Add(teamData.team[unitToGetOutIndex]);
        }

        if(teamData.teamUnitsThatAlreadyPlayed.Contains(unitToGetOut))
        {
            int indexInPlayedUnits= FindUnitIndexInTeam(unitToGetOut, teamData.teamUnitsThatAlreadyPlayed);
            teamData.teamUnitsThatAlreadyPlayed[indexInPlayedUnits]= unitToGetIn;
        }
        teamData.team = ReOrderTeamAfterSummon(teamData.team, teamData.originalTeamOrder);
        
        
        
    }
    
    public int FindUnitIndexInTeam(Unit unit, List<Unit> team)
    {
        for (int i = 0; i < team.Count; i++)
        {
            if (team[i] == unit)
            {
                return i;
            }
        }

        return -1; 
    }

   
    
    public List<Unit> ReOrderTeamAfterSummon(List<Unit> team, List<Unit> originalTeamOrder)
    {
        List<Unit> ordered = new List<Unit>();

        Unit samurai = team.FirstOrDefault(unit => unit is Samurai);
        ordered.Add(samurai);
        

        ordered.AddRange(
            team.Where(unit => unit.active && !(unit is Samurai))
        );

        ordered.AddRange(
            originalTeamOrder.Where(unit => !unit.active && !(unit is Samurai))
        );

        
        return ordered;
    }

    
        
    
    

}