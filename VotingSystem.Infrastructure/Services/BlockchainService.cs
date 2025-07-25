using System.Collections.Generic;
using System.Linq;
using VotingSystem.Application.Services;
using VotingSystem.Domain.Blockchain;

namespace VotingSystem.Infrastructure.Services
{
    public class BlockchainService : IBlockchainService
    {
        private readonly IHashService _hashService;

        public BlockchainService(IHashService hashService)
        {
            _hashService = hashService;
        }

        /// <summary>
        /// 新しいブロックを作成
        /// </summary>
        /// <param name="index">ブロックのインデックス</param>
        /// <param name="transactions">ブロックに含まれるトランザクションのリスト</param>
        /// <param name="previousHash">前のブロックのハッシュ値</param>
        public Block CreateBlock(int index, List<VoteTransaction> transactions, string previousHash)
        {
            var block = new Block(index, transactions, previousHash);
            block.Hash = _hashService.ComputeSha256Hash(block.GetHashInput());
            return block;
        }

        public bool ValidateBlock(Block block, Block previousBlock)
        {
            // 基本的な検証
            if (block.Index != previousBlock.Index + 1)
                return false;

            if (block.PreviousHash != previousBlock.Hash)
                return false;

            // ハッシュの再計算と検証
            var calculatedHash = _hashService.ComputeSha256Hash(block.GetHashInput());
            if (block.Hash != calculatedHash)
                return false;

            // タイムスタンプ検証（前のブロックより後である必要がある）
            if (block.Timestamp <= previousBlock.Timestamp)
                return false;

            return true;
        }

        /// <summary>
        /// ブロックチェーン全体の検証（詳細情報付き）
        /// </summary>
        /// <param name="chain"></param>
        /// <returns></returns>
        public bool ValidateChain(List<Block> chain)
        {
            if (chain == null || chain.Count == 0)
                return false;

            // ジェネシスブロック（最初のブロック）の検証
            var genesisBlock = chain[0];
            if (genesisBlock.Index != 0 || genesisBlock.PreviousHash != "0")
                return false;

            // チェーン全体の検証
            for (int i = 1; i < chain.Count; i++)
            {
                if (!ValidateBlock(chain[i], chain[i - 1]))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// ブロックチェーンの詳細検証（エラー情報付き）
        /// </summary>
        /// <param name="chain"></param>
        /// <returns></returns>
        public (bool IsValid, List<string> Errors) ValidateChainDetailed(List<Block> chain)
        {
            var errors = new List<string>();

            if (chain == null || chain.Count == 0)
            {
                errors.Add("ブロックチェーンが空です");
                return (false, errors);
            }

            // ジェネシスブロック検証
            var genesisBlock = chain[0];
            if (genesisBlock.Index != 0)
                errors.Add($"ジェネシスブロックのインデックスが無効です: {genesisBlock.Index}");
            
            if (genesisBlock.PreviousHash != "0")
                errors.Add($"ジェネシスブロックの前ハッシュが無効です: {genesisBlock.PreviousHash}");

            // 各ブロックの検証
            for (int i = 1; i < chain.Count; i++)
            {
                var currentBlock = chain[i];
                var previousBlock = chain[i - 1];

                // インデックス連続性
                if (currentBlock.Index != previousBlock.Index + 1)
                    errors.Add($"ブロック #{currentBlock.Index}: インデックスが連続していません");

                // 前ハッシュ整合性
                if (currentBlock.PreviousHash != previousBlock.Hash)
                    errors.Add($"ブロック #{currentBlock.Index}: 前ブロックハッシュが一致しません");

                // ハッシュ値検証
                var calculatedHash = _hashService.ComputeSha256Hash(currentBlock.GetHashInput());
                if (currentBlock.Hash != calculatedHash)
                    errors.Add($"ブロック #{currentBlock.Index}: ハッシュ値が改ざんされています");

                // タイムスタンプ検証
                if (currentBlock.Timestamp <= previousBlock.Timestamp)
                    errors.Add($"ブロック #{currentBlock.Index}: タイムスタンプが無効です");

                // トランザクション検証
                foreach (var transaction in currentBlock.Transactions)
                {
                    if (string.IsNullOrEmpty(transaction.TransactionId))
                        errors.Add($"ブロック #{currentBlock.Index}: 無効なトランザクションIDがあります");
                    
                    if (string.IsNullOrEmpty(transaction.VoterId))
                        errors.Add($"ブロック #{currentBlock.Index}: 無効な投票者IDがあります");
                    
                    if (string.IsNullOrEmpty(transaction.CandidateId))
                        errors.Add($"ブロック #{currentBlock.Index}: 無効な候補者IDがあります");
                }
            }

            return (errors.Count == 0, errors);
        }

        /// <summary>
        /// 投票トランザクションの作成
        /// </summary>
        /// <param name="voterId">投票者のID</param>
        /// <param name="candidateId">候補者のID</param>
        /// <param name="signature">署名</param>
        /// <returns>作成された投票トランザクション</returns>
        /// <exception cref="ArgumentException">不正な引数が渡された場合</exception>
        /// <exception cref="InvalidOperationException">トランザクションの作成に失敗した場合</exception>
        public VoteTransaction CreateVoteTransaction(string voterId, string candidateId, string signature)
        {
            return new VoteTransaction(voterId, candidateId, signature);
        }
    }
}