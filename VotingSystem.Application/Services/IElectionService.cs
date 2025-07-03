using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VotingSystem.Application.DTOs;

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
