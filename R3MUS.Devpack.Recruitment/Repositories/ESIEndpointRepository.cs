using R3MUS.Devpack.Recruitment.Repositories.Entities;
using System.Linq;

namespace R3MUS.Devpack.Recruitment.Repositories
{
    public class ESIEndpointRepository : IESIEndpointRepository
    {
        private readonly IDatabaseContext _databaseContext;

        public ESIEndpointRepository(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public ESIEndpoint GetByName(string endpointName)
        {
            return _databaseContext.ESIEndpoints.First(w => w.Name == endpointName);
        }
    }
}