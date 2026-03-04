namespace VotingSystem.Domain.Simulation.Entities;

/// <summary>
/// 被選挙人（候補者）の公約（ポイント配分）
/// </summary>
public class CandidateManifesto
{
    public int Id { get; set; }
    public int CandidateId { get; set; } // ユーザーまたはNPC候補者のID
    public int ElectionId { get; set; }
    
    // 合計100ポイントを割り振る
    public int EconomyPoints { get; set; }
    public int EducationPoints { get; set; }
    public int WelfarePoints { get; set; }
    public int SecurityPoints { get; set; }

    public bool IsValid() => (EconomyPoints + EducationPoints + WelfarePoints + SecurityPoints) <= 100;
}
