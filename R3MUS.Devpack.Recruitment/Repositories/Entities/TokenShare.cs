using R3MUS.Devpack.Recruitment.Enums;

namespace R3MUS.Devpack.Recruitment.Repositories.Entities
{
    public class TokenShare
    {
        public int Id { get; set; }
        public long CorporationId { get; set; }
        public long? AllianceId { get; set; }
        public int RecruitId { get; set; }
        public virtual Recruit Recruit { get; set; }
        public int StatusInt { get; set; }
        public ApplicationStatus Status
        {
            get
            {
                return (ApplicationStatus)StatusInt;
            }
            set
            {
                StatusInt = (int)value;
            }
        }
    }
}