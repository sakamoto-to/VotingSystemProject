using System;
using System.Collections.Generic;

// <summary>
//  投票システムの選挙応答DTO
// </summary>
namespace VotingSystem.Application.DTOs
{
    public class ElectionResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public List<string> Candidates { get; set; } = new();
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public int TotalVotes { get; set; }
    }
}