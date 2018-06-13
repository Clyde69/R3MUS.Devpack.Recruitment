using R3MUS.Devpack.Recruitment.Repositories.Entities;
using System.Data.Entity.ModelConfiguration;

namespace R3MUS.Devpack.Recruitment.Repositories.Configuration
{
    public class ESIEndpointConfiguration : EntityTypeConfiguration<ESIEndpoint>
    {
        public ESIEndpointConfiguration()
        {
            ToTable("ESIEndpoint", "Security");

            HasKey(h => h.Id);
        }
    }
}