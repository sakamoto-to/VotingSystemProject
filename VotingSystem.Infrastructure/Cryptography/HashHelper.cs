
using System.Security.Cryptography;
using System.Text;

namespace VotingSystem.Infrastructure.Cryptography
{

    /// <summary>
    /// ハッシュ計算を行うヘルパークラス
    /// summary>
    /// <remarks>
    /// このクラスは、SHA256アルゴリズムを使用して文字列のハッシュ値を計算します。
    /// </remarks>
    public static class HashHelper
    {
        public static string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}