using VotingSystem.Domain.Blockchain;

namespace VotingSystem.Application.Services
{
    /// <summary>
    /// ブロックチェーン関連のサービスインターフェース
    /// </summary>
    public interface IBlockchainService
    {
        Block CreateBlock(int index, List<VoteTransaction> transactions, string previousHash);
        bool ValidateBlock(Block block, Block previousBlock);
        bool ValidateChain(List<Block> chain);
        (bool IsValid, List<string> Errors) ValidateChainDetailed(List<Block> chain);
        VoteTransaction CreateVoteTransaction(string voterId, string candidateId, string signature);
    }
}