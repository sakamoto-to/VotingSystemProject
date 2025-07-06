using VotingSystem.Application.Services;
using VotingSystem.Infrastructure.Cryptography;

namespace VotingSystem.Infrastructure.Services
{
    /// <summary>
    /// ハッシュ計算を行うサービスクラス
    /// </summary>
    public class HashService : IHashService
    {
        /// <summary>
        /// SHA256アルゴリズムを使用して文字列のハッシュ値を計算します。
        /// </summary>
        /// <param name="input">ハッシュ化する文字列</param>
        /// <returns>ハッシュ値</returns>
        public string ComputeSha256Hash(string input)
        {
            return HashHelper.ComputeSha256Hash(input);
        }
    }
}