namespace VotingSystem.Domain.Entities;

public class Election
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public List<string> Candidates { get; set; } = new();
    public int Votes { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}