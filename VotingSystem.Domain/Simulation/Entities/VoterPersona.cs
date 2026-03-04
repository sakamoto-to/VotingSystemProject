namespace VotingSystem.Domain.Simulation.Entities;

/// <summary>
/// 有権者（NPC）のペルソナ定義
/// 政策に対する「ウェイト（重要度・反発度）」を保持する
/// </summary>
public class VoterPersona
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    
    // 政策に対するウェイト（正の数は支持、負の数は反発）
    public double EconomyWeight { get; set; }
    public double EducationWeight { get; set; }
    public double WelfareWeight { get; set; }
    public double SecurityWeight { get; set; }
}
