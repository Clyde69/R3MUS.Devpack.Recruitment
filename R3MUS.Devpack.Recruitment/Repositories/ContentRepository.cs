using R3MUS.Devpack.Recruitment.Models.Content;
using R3MUS.Devpack.Recruitment.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace R3MUS.Devpack.Recruitment.Repositories
{
    public class ContentRepository : IContentRepository
    {
        private readonly IDatabaseContext _databaseContext;

        public ContentRepository(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public View GetPageContent(ContentRequest request)
        {
            return _databaseContext.Views.First(w => 
                w.Controller == request.Page 
                && w.Name == request.View);
        }
    }
}