using R3MUS.Devpack.Recruitment.Repositories.Configuration;
using R3MUS.Devpack.Recruitment.Repositories.Entities;
using System.Data.Entity;

namespace R3MUS.Devpack.Recruitment.Repositories
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        public DbSet<Recruit> Recruits { get; set; }
        public DbSet<TokenData> Tokens { get; set; }
        public DbSet<TokenShare> TokenShares { get; set; }
        public DbSet<View> Views { get; set; }
        public DbSet<ViewArea> ViewAreas { get; set; }
        public DbSet<ESIEndpoint> ESIEndpoints { get; set; }

        public DatabaseContext() : base("ConnectionString")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new RecruitConfiguration());
            modelBuilder.Configurations.Add(new TokenDataConfiguration());
            modelBuilder.Configurations.Add(new TokenShareConfiguration());
            modelBuilder.Configurations.Add(new ViewConfiguration());
            modelBuilder.Configurations.Add(new ViewAreaConfiguration());
            modelBuilder.Configurations.Add(new ESIEndpointConfiguration());
        }
    }
}