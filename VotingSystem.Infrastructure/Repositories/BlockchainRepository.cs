using System.Collections.Generic;
using VotingSystem.Application.Repositories;
using VotingSystem.Domain.Blockchain;

namespace VotingSystem.Infrastructure.Repositories
{
    public class BlockchainRepository : IBlockchainRepository
    {
        private static readonly List<Block> _blockchain = new();
        private static readonly object _lock = new object();

        /// <summary>
        /// ブロックチェーンにブロックを追加
        /// /// </summary>
        /// <param name="block">追加するブロック</param>
        public void AddBlock(Block block)
        {
            lock (_lock)
            {
                _blockchain.Add(block);
            }
        }

        /// <summary>
        /// ブロックチェーン全体を取得
        /// </summary>
        public List<Block> GetChain()
        {
            lock (_lock)
            {
                return new List<Block>(_blockchain); // 防御的コピー
            }
        }

        /// <summary>
        /// 最新のブロックを取得
        /// </summary>
        public Block? GetLatestBlock()
        {
            lock (_lock)
            {
                return _blockchain.Count > 0 ? _blockchain[^1] : null;
            }
        }

        /// <summary>
        /// ブロックチェーンの長さを取得
        /// </summary>
        public int GetChainLength()
        {
            lock (_lock)
            {
                return _blockchain.Count;
            }
        }

        /// <summary>
        /// ジェネシスブロックを初期化
        /// </summary>
        public void InitializeGenesisBlock()
        {
            lock (_lock)
            {
                if (_blockchain.Count == 0)
                {
                    var genesis = new Block(0, new List<VoteTransaction>(), "0");
                    genesis.Hash = "genesis_hash"; // 仮のハッシュ
                    _blockchain.Add(genesis);
                }
            }
        }
    }
}