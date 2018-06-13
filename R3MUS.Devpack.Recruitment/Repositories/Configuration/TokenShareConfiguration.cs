using R3MUS.Devpack.Recruitment.Repositories.Entities;
using System.Data.Entity.ModelConfiguration;

namespace R3MUS.Devpack.Recruitment.Repositories.Configuration
{
    public class TokenShareConfiguration : EntityTypeConfiguration<TokenShare>
    {
        public TokenShareConfiguration()
        {
            ToTable("SharedWith", "Application");

            HasKey(b => b.Id);
            HasRequired<Recruit>(h => h.Recruit)
                .WithMany(w => w.TokenShare)
                .HasForeignKey(h => h.RecruitId);
        }
    }
}