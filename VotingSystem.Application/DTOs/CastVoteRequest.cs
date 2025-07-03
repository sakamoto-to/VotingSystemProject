using System;
using System.ComponentModel.DataAnnotations;

namespace VotingSystem.Application.DTOs
{
    /// <summary>
    /// 投票システムの投票リクエストDTO
    /// </summary>
    public class CastVoteRequest
    {
        [Required]
        public Guid ElectionId { get; set; }

        [Required]
        public string SelectedCandidate { get; set; } = string.Empty;

        public string VoterIdentifier { get; set; } = string.Empty;
    }
}
