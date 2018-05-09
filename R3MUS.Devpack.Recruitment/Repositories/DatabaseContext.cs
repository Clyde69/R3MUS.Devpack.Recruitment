using R3MUS.Devpack.Recruitment.Repositories.Configuration;
using R3MUS.Devpack.Recruitment.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace R3MUS.Devpack.Recruitment.Repositories
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Recruit> Recruits { get; set; }
        public DbSet<History> Historys { get; set; }

        public DatabaseContext() : base("ConnectionString")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new RecruitConfiguration());
            modelBuilder.Configurations.Add(new HistoryConfiguration());
        }
    }
}