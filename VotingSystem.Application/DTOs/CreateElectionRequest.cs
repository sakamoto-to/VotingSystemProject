using System.ComponentModel.DataAnnotations;

namespace VotingSystem.Application.DTOs
{
    /// <summary>
    ///  投票システムの選挙作成リクエストDTO
    /// </summary>
    public class CreateElectionRequest
    {
        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public List<string> Candidates { get; set; } = new();

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }
    }
}