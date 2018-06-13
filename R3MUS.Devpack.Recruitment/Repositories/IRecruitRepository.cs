namespace R3MUS.Devpack.Recruitment.Repositories
{
    public interface IRecruitRepository
    {
        void AddOrUpdateToken(string token, long characterId);
        string GetRefreshTokenForApplicant(long characterId);
    }
}