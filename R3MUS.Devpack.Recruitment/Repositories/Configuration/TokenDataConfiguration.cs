using R3MUS.Devpack.Recruitment.Repositories.Entities;
using System.Data.Entity.ModelConfiguration;

namespace R3MUS.Devpack.Recruitment.Repositories.Configuration
{
    public class TokenDataConfiguration : EntityTypeConfiguration<TokenData>
    {
        public TokenDataConfiguration()
        {
            ToTable("TokenData", "Application");

            HasKey(b => b.Id);
            HasRequired<Recruit>(h => h.Recruit)
                .WithMany(w => w.TokenData)
                .HasForeignKey(h => h.RecruitId);
        }
    }
}