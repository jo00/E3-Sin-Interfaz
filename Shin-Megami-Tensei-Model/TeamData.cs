namespace Shin_Megami_Tensei.Configs;

public class TeamData
{
    public List<UnitData> team;
    public Samurai samurai;
    public List<UnitData> teamUnitsThatAlreadyPlayed;
    public string playerNumber;
    public List<UnitData> originalTeamOrder;
    public int abilitiesUsedCounter;
    
    public TeamData(List<UnitData > team, Samurai samurai, List<UnitData> teamUnitsThatAlreadyPlayed, string playerNumber)
    {
        this.team = team;
        this.samurai = samurai;
        this.teamUnitsThatAlreadyPlayed = teamUnitsThatAlreadyPlayed;
        this.playerNumber = playerNumber;
        originalTeamOrder =  new List<UnitData>(team);
        abilitiesUsedCounter = 0;
    }
}