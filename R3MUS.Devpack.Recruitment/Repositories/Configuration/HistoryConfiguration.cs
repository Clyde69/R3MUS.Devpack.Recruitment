﻿using R3MUS.Devpack.Recruitment.Repositories.Entities;
using System.Data.Entity.ModelConfiguration;

namespace R3MUS.Devpack.Recruitment.Repositories.Configuration
{
    public class HistoryConfiguration : EntityTypeConfiguration<History>
    {
        public HistoryConfiguration()
        {
            ToTable("History", "Application");
            
            HasKey(b => b.Id);
            HasRequired<Recruit>(h => h.Recruit)
                .WithMany(w => w.History)
                .HasForeignKey(h => h.RecruitId);
        }
    }
}