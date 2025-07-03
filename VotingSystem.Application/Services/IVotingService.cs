using VotingSystem.Application.DTOs;

/// <summary>
/// 投票システムの投票サービスインターフェース
/// </summary>
namespace VotingSystem.Application.Services
{
    public interface IVotingService
    {
        Task<bool> CastVoteAsync(CastVoteRequest request);
        Task<bool> HasVotedAsync(Guid electionId, string voterIdentifier);
        Task<VoteResultsResponse> GetResultsAsync(Guid electionId);
        Task<bool> ValidateVoteAsync(CastVoteRequest request);
    }
}
