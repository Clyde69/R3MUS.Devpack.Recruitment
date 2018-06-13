using R3MUS.Devpack.Recruitment.Models.Content;
using R3MUS.Devpack.Recruitment.Repositories.Entities;

namespace R3MUS.Devpack.Recruitment.Repositories
{
    public interface IContentRepository
    {
        View GetPageContent(ContentRequest request);
    }
}