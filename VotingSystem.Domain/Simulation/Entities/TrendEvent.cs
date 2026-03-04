namespace VotingSystem.Domain.Simulation.Entities;

/// <summary>
/// 世間のトレンド（ターンごとに変動する外部要因）
/// </summary>
public class TrendEvent
{
    public int Id { get; set; }
    public string EventName { get; set; } = string.Empty; // 例: "不況の波", "少子化ニュース"
    public int TurnNumber { get; set; }
    
    // トレンド乗数（1.0が基準、1.5なら注目度1.5倍）
    public double EconomyMultiplier { get; set; } = 1.0;
    public double EducationMultiplier { get; set; } = 1.0;
    public double WelfareMultiplier { get; set; } = 1.0;
    public double SecurityMultiplier { get; set; } = 1.0;
}
