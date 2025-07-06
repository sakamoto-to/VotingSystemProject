namespace VotingSystem.Application.Services
{
    /// <summary>
    /// ハッシュ計算を行うサービスインターフェース
    /// </summary>
    public interface IHashService
    {
        string ComputeSha256Hash(string input);
    }
}