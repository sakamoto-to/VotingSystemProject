using System;

namespace VotingSystem.Domain.Entities
{
    public class Voter
    {
        public Guid Id { get; set; }
        public string Identifier { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}