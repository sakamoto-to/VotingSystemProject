using System;
using System.Collections.Generic;
using System.Linq;

namespace VotingSystem.Domain.Blockchain
{
    public class Block
    {
        public int Index { get; set; }
        public DateTime Timestamp { get; set; }
        public List<VoteTransaction> Transactions { get; set; } = new();
        public string PreviousHash { get; set; } = string.Empty;
        public string Hash { get; set; } = string.Empty;

        public Block(int index, List<VoteTransaction> transactions, string previousHash)
        {
            Index = index;
            Timestamp = DateTime.UtcNow;
            Transactions = transactions;
            PreviousHash = previousHash;
            // ハッシュは外部サービスで計算
        }

        // ハッシュ計算用の文字列生成
        public string GetHashInput()
        {
            var transactionHashes = string.Join("", Transactions.Select(t => t.GetHashInput()));
            return $"{Index}{Timestamp:yyyy-MM-ddTHH:mm:ss.fffffffZ}{PreviousHash}{transactionHashes}";
        }
    }
}