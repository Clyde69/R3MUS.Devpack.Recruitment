using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace R3MUS.Devpack.Recruitment.Repositories.Entities
{
    public class ViewArea
    {
        public int Id { get; set; }
        public int ViewId { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public string ClientCorporationTicker { get; set; }

        public virtual View View { get; set; }
    }
}