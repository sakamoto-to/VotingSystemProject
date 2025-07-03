using Microsoft.EntityFrameworkCore;
using VotingSystem.Application.DTOs;
using VotingSystem.Application.Services;
using VotingSystem.Domain.Entities;
using VotingSystem.Infrastructure.Data;

namespace VotingSystem.Infrastructure.Services
{
    /// <summary>
    /// 投票システムの投票サービス実装
    /// </summary>
    public class VotingService : IVotingService
    {
        private readonly VotingDbContext _context;

        public VotingService(VotingDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 投票を行うメソッド
        /// </summary>
        /// <param name="request">投票リクエスト</param>
        /// <returns>投票が成功したかどうか</returns>
        /// <exception cref="InvalidOperationException">選挙が見つからない場合</exception>
        /// <exception cref="ArgumentException">投票者識別子が無効な場合</exception>
        /// <exception cref="InvalidOperationException">投票の妥当性が検証できない場合</exception>    
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


        /// <summary>
        /// 投票者が指定された選挙に投票済み確認メソッド
        /// </summary>
        /// <param name="electionId"></param>
        /// <param name="voterIdentifier"></param>
        /// <returns></returns>
        public async Task<bool> HasVotedAsync(Guid electionId, string voterIdentifier)
        {
            var voter = await _context.Voters
                .FirstOrDefaultAsync(v => v.Identifier == voterIdentifier);

            if (voter == null)
                return false;

            return await _context.Votes
                .AnyAsync(v => v.ElectionId == electionId);
        }

        /// <summary>
        /// 指定された選挙の投票結果を取得するメソッド
        /// </summary>
        /// <param name="electionId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 投票の妥当性を検証するメソッド
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
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
