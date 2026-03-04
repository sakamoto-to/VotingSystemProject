using System;
using System.Collections.Generic;
using System.Linq;
using VotingSystem.Domain.Simulation.Entities;

namespace VotingSystem.Application.Simulation.Services;

public class SupportCalculationService
{
    /// <summary>
    /// 候補者のマニフェスト（ポイント配分）と現在のトレンドに基づき、
    /// 各NPCペルソナからの支持度（スコア）を計算する。
    /// </summary>
    /// <returns>NPC IDと支持スコアのディクショナリ</returns>
    public Dictionary<int, double> CalculateSupportScore(
        CandidateManifesto manifesto, 
        List<VoterPersona> personas, 
        TrendEvent currentTrend)
    {
        var supportScores = new Dictionary<int, double>();

        if (!manifesto.IsValid())
        {
            throw new ArgumentException("ポイントの合計が100を超えています。");
        }

        foreach (var persona in personas)
        {
            // 計算式: 各政策の (ポイント × ペルソナのウェイト × トレンド乗数) の合計
            double economyScore   = manifesto.EconomyPoints   * persona.EconomyWeight   * currentTrend.EconomyMultiplier;
            double educationScore = manifesto.EducationPoints * persona.EducationWeight * currentTrend.EducationMultiplier;
            double welfareScore   = manifesto.WelfarePoints   * persona.WelfareWeight   * currentTrend.WelfareMultiplier;
            double securityScore  = manifesto.SecurityPoints  * persona.SecurityWeight  * currentTrend.SecurityMultiplier;

            // ベースとなる支持スコア
            double totalScore = economyScore + educationScore + welfareScore + securityScore;

            supportScores.Add(persona.Id, totalScore);
        }

        return supportScores;
    }

    /// <summary>
    /// 特定のペルソナがどの候補者に投票するかを決定するロジック（絶対評価・相対評価）
    /// </summary>
    public int DetermineVote(int personaId, Dictionary<int, CandidateManifesto> candidateManifestos, List<VoterPersona> personas, TrendEvent currentTrend)
    {
        var persona = personas.FirstOrDefault(p => p.Id == personaId);
        if (persona == null) return -1; // ペルソナが存在しない場合

        int bestCandidateId = -1;
        double highestScore = double.MinValue;
        double threshold = 20.0; // 投票に行くための最低期待値（例: 全く支持できない場合は棄権）

        foreach (var candidate in candidateManifestos)
        {
            var scores = CalculateSupportScore(candidate.Value, new List<VoterPersona> { persona }, currentTrend);
            double score = scores[persona.Id];

            // 最もスコアが高い候補者を選ぶ
            if (score > highestScore && score >= threshold)
            {
                highestScore = score;
                bestCandidateId = candidate.Key;
            }
        }

        // thresholdを超えた候補がいなければ -1 (棄権/白票) を返す
        return bestCandidateId;
    }
}
