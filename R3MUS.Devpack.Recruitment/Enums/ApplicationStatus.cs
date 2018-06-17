using System.ComponentModel.DataAnnotations;

namespace R3MUS.Devpack.Recruitment.Enums
{
    public enum ApplicationStatus
    {
        Applied,
        [Display(Name = "In Progress")]
        InProgress,
        Rejected,
        [Display(Name = "Director Review")]
        DirectorReview,
        [Display(Name = "Alliance Review")]
        AllianceReview,
        Accepted
    }
}