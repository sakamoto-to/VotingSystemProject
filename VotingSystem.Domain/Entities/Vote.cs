using System;

namespace VotingSystem.Domain.Entities
{
    public class Vote
    {
        public Guid Id { get; set; }
        public Guid ElectionId { get; set; }
        public string SelectedCandidate { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
    }
}