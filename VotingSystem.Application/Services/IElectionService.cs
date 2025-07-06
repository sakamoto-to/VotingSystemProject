using VotingSystem.Application.DTOs;

/// <summary>
/// 選挙関連のサービスインターフェース
/// </summary>
namespace VotingSystem.Application.Services
{
    public interface IElectionService
    {
        Task<ElectionResponse> CreateElectionAsync(CreateElectionRequest request);
        Task<List<ElectionResponse>> GetActiveElectionsAsync();
        Task<ElectionResponse?> GetElectionByIdAsync(Guid id);
        Task<bool> ElectionExistsAsync(Guid id);
    }
}
