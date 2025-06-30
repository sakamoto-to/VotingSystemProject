using System;
using System.Collections.Generic;

/// <summary>
/// 投票システムの投票結果応答DTO
/// </summary>
namespace VotingSystem.Application.DTOs
{
    public class VoteResultsResponse
    {
        public Guid ElectionId { get; set; }
        public string ElectionTitle { get; set; } = string.Empty;
        public List<CandidateResult> Results { get; set; } = new();
        public int TotalVotes { get; set; }
        public DateTime LastUpdated { get; set; }
    }

    public class CandidateResult
    {
        public string CandidateName { get; set; } = string.Empty;
        public int VoteCount { get; set; }
        public decimal Percentage { get; set; }
    }
}