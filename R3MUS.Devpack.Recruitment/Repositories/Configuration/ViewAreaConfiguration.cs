using R3MUS.Devpack.Recruitment.Repositories.Entities;
using System.Data.Entity.ModelConfiguration;

namespace R3MUS.Devpack.Recruitment.Repositories.Configuration
{
    public class ViewAreaConfiguration : EntityTypeConfiguration<ViewArea>
    {
        public ViewAreaConfiguration()
        {
            ToTable("ViewArea", "Content");

            HasKey(h => h.Id);

            HasRequired<View>(r => r.View)
                .WithMany(w => w.ViewAreas)
                .HasForeignKey(k => k.ViewId);
        }
    }
}