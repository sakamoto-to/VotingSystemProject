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
    public class VotingService : IVotingService
    {
        private readonly VotingDbContext _context;

        public VotingService(VotingDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CastVoteAsync(CastVoteRequest request)
        {
            // 投票可能かチェック
            if (!await ValidateVoteAsync(request))
                return false;

            // 既に投票済みかチェック
            if (await HasVotedAsync(request.ElectionId, request.VoterIdentifier))
                return false;

            var vote = new Vote
            {
                Id = Guid.NewGuid(),
                ElectionId = request.ElectionId,
                SelectedCandidate = request.SelectedCandidate,
                Timestamp = DateTime.Now
            };

            // 投票者情報も記録
            var voter = await _context.Voters
                .FirstOrDefaultAsync(v => v.Identifier == request.VoterIdentifier);
            
            if (voter == null)
            {
                voter = new Voter
                {
                    Id = Guid.NewGuid(),
                    Identifier = request.VoterIdentifier,
                    CreatedAt = DateTime.Now
                };
                _context.Voters.Add(voter);
            }

            _context.Votes.Add(vote);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> HasVotedAsync(Guid electionId, string voterIdentifier)
        {
            var voter = await _context.Voters
                .FirstOrDefaultAsync(v => v.Identifier == voterIdentifier);
            
            if (voter == null)
                return false;

            return await _context.Votes
                .AnyAsync(v => v.ElectionId == electionId);
        }

        public async Task<VoteResultsResponse> GetResultsAsync(Guid electionId)
        {
            var election = await _context.Elections.FindAsync(electionId);
            if (election == null)
                throw new InvalidOperationException("Election not found");

            var votes = await _context.Votes
                .Where(v => v.ElectionId == electionId)
                .ToListAsync();

            var totalVotes = votes.Count;
            var results = votes
                .GroupBy(v => v.SelectedCandidate)
                .Select(g => new CandidateResult
                {
                    CandidateName = g.Key,
                    VoteCount = g.Count(),
                    Percentage = totalVotes > 0 ? (decimal)g.Count() / totalVotes * 100 : 0
                })
                .ToList();

            return new VoteResultsResponse
            {
                ElectionId = electionId,
                ElectionTitle = election.Title,
                Results = results,
                TotalVotes = totalVotes,
                LastUpdated = DateTime.Now
            };
        }

        public async Task<bool> ValidateVoteAsync(CastVoteRequest request)
        {
            var election = await _context.Elections.FindAsync(request.ElectionId);
            if (election == null)
                return false;

            var now = DateTime.Now;
            if (now < election.StartDate || now > election.EndDate)
                return false;

            if (!election.Candidates.Contains(request.SelectedCandidate))
                return false;

            return true;
        }
    }
}
