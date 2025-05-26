namespace Shin_Megami_Tensei.Configs;

public class TeamData
{
    public List<Unit> team;
    public Samurai samurai;
    public List<Unit> teamUnitsThatAlreadyPlayed;
    public string playerNumber;
    public List<Unit> originalTeamOrder;
    public int abilitiesUsedCounter;
    public TeamController teamController;
    
    public TeamData(List<Unit > team, Samurai samurai, List<Unit> teamUnitsThatAlreadyPlayed, string playerNumber, TeamController teamController)
    {
        this.team = team;
        this.samurai = samurai;
        this.teamUnitsThatAlreadyPlayed = teamUnitsThatAlreadyPlayed;
        this.playerNumber = playerNumber;
        originalTeamOrder =  new List<Unit>(team);
        abilitiesUsedCounter = 0;
        this.teamController = teamController;
    }
}