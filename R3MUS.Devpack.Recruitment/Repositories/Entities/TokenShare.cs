namespace R3MUS.Devpack.Recruitment.Repositories.Entities
{
    public class TokenShare
    {
        public int Id { get; set; }
        public long CorporationId { get; set; }
        public int RecruitId { get; set; }
        public virtual Recruit Recruit { get; set; }
    }
}