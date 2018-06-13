using R3MUS.Devpack.Recruitment.Repositories.Entities;

namespace R3MUS.Devpack.Recruitment.Repositories
{
    public interface IESIEndpointRepository
    {
        ESIEndpoint GetByName(string endpointName);
    }
}