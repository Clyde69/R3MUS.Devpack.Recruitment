using AutoMapper;
using R3MUS.Devpack.Recruitment.Models.Content;
using R3MUS.Devpack.Recruitment.Repositories;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;

namespace R3MUS.Devpack.Recruitment.Services.SiteContent
{
    public class PageContentService : IPageContentService
    {
        private readonly IContentRepository _contentRepository;
        private readonly IMapper _mapper;

        private const string _default = "";

        public PageContentService(IContentRepository contentRepository, IMapper mapper)
        {
            _contentRepository = contentRepository;
            _mapper = mapper;
        }

        public ContentResponse GetContent([CallerMemberName] string viewName = _default,
            [CallerFilePath] string controllerName = _default)
        {
            controllerName = controllerName.Split('\\').ToList().Last().Replace("Controller.cs", string.Empty);

            return new ContentResponse
            {
                Areas = _mapper.Map<List<ContentAreaResponse>>(_contentRepository.GetPageContent(
                    new ContentRequest { Page = controllerName, View = viewName }).ViewAreas)
            };
        }
    }
}