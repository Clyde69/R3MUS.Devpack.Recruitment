using System.Data.Entity;
using R3MUS.Devpack.Recruitment.Repositories.Entities;

namespace R3MUS.Devpack.Recruitment.Repositories
{
    public interface IDatabaseContext
    {
        DbSet<Recruit> Recruits { get; set; }
        DbSet<TokenData> Tokens { get; set; }
        DbSet<View> Views { get; set; }
        DbSet<ViewArea> ViewAreas { get; set; }
        DbSet<ESIEndpoint> ESIEndpoints { get; set; }
    }
}