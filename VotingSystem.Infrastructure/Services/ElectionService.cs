using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VotingSystem.Application.DTOs;
using VotingSystem.Application.Services;
using VotingSystem.Domain.Entities;
using VotingSystem.Infrastructure.Data;

namespace VotingSystem.Infrastructure.Services
{
    public class ElectionService : IElectionService
    {
        private readonly VotingDbContext _context;

        public ElectionService(VotingDbContext context)
        {
            _context = context;
        }

        public async Task<ElectionResponse> CreateElectionAsync(CreateElectionRequest request)
        {
            var election = new Election
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Candidates = request.Candidates,
                StartDate = request.StartDate,
                EndDate = request.EndDate
            };

            _context.Elections.Add(election);
            await _context.SaveChangesAsync();

            return MapToResponse(election);
        }

        public async Task<List<ElectionResponse>> GetActiveElectionsAsync()
        {
            var now = DateTime.Now;
            var elections = await _context.Elections
                .Where(e => e.StartDate <= now && e.EndDate >= now)
                .ToListAsync();

            return elections.Select(MapToResponse).ToList();
        }

        public async Task<ElectionResponse?> GetElectionByIdAsync(Guid id)
        {
            var election = await _context.Elections.FindAsync(id);
            return election != null ? MapToResponse(election) : null;
        }

        public async Task<bool> ElectionExistsAsync(Guid id)
        {
            return await _context.Elections.AnyAsync(e => e.Id == id);
        }

        private ElectionResponse MapToResponse(Election election)
        {
            var now = DateTime.Now;
            var totalVotes = _context.Votes.Count(v => v.ElectionId == election.Id);

            return new ElectionResponse
            {
                Id = election.Id,
                Title = election.Title,
                Candidates = election.Candidates,
                StartDate = election.StartDate,
                EndDate = election.EndDate,
                IsActive = election.StartDate <= now && election.EndDate >= now,
                TotalVotes = totalVotes
            };
        }
    }
}
