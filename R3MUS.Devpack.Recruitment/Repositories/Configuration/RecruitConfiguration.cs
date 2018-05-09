using R3MUS.Devpack.Recruitment.Repositories.Entities;
using System.Data.Entity.ModelConfiguration;

namespace R3MUS.Devpack.Recruitment.Repositories.Configuration
{
    public class RecruitConfiguration : EntityTypeConfiguration<Recruit>
    {
        public RecruitConfiguration()
        {
            ToTable("Recruit", "Application");

            HasKey(b => b.Id);

            HasMany<History>(h => h.History)
                .WithRequired(w => w.Recruit)
                .HasForeignKey(h => h.RecruitId);

            HasMany<TokenData>(h => h.TokenData)
                .WithRequired(w => w.Recruit)
                .HasForeignKey(h => h.RecruitId);
        }
    }
}