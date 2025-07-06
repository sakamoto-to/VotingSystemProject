using VotingSystem.Domain.Blockchain;

namespace VotingSystem.Application.Repositories
{
    /// <summary>
    /// ブロックチェーン関連のリポジトリインターフェース
    /// </summary>
    public interface IBlockchainRepository
    {
        void AddBlock(Block block);
        List<Block> GetChain();
        Block? GetLatestBlock();
        int GetChainLength();
        void InitializeGenesisBlock();
    }
}