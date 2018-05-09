namespace R3MUS.Devpack.Recruitment.Repositories.Entities
{
    public class TokenData
    {
        public int Id { get; set; }
        public int RecruitId { get; set; }
        public string RefreshToken { get; set; }
        public virtual Recruit Recruit { get; set; }
    }
}