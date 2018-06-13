using R3MUS.Devpack.Recruitment.Repositories.Entities;
using System.Data.Entity.ModelConfiguration;

namespace R3MUS.Devpack.Recruitment.Repositories.Configuration
{
    public class ViewConfiguration : EntityTypeConfiguration<View>
    {
        public ViewConfiguration()
        {
            ToTable("View", "Content");

            HasKey(b => b.Id);
            Property(p => p.Name).HasColumnName("View");

            HasMany<ViewArea>(h => h.ViewAreas)
                .WithRequired(r => r.View)
                .HasForeignKey(f => f.ViewId);
        }
    }
}