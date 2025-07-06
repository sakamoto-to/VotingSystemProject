using System;

namespace VotingSystem.Domain.Blockchain
{
    public class VoteTransaction
    {
        public string TransactionId { get; set; }
        public string VoterId { get; set; }
        public string CandidateId { get; set; }
        public DateTime Timestamp { get; set; }
        public string Signature { get; set; }

            
        // コンストラクタ
        public VoteTransaction(string voterId, string candidateId, string signature)
        {
            TransactionId = Guid.NewGuid().ToString();
            VoterId = voterId;
            CandidateId = candidateId;
            Timestamp = DateTime.UtcNow;
            Signature = signature;
        }

        // ハッシュ計算用の文字列生成
        public string GetHashInput()
        {
            return $"{TransactionId}{VoterId}{CandidateId}{Timestamp:yyyy-MM-ddTHH:mm:ss.fffffffZ}{Signature}";
        }
    }
}