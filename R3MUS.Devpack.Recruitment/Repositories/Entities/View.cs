using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace R3MUS.Devpack.Recruitment.Repositories.Entities
{
    public class View
    {
        public int Id { get; set; }
        public string Controller { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ViewArea> ViewAreas { get; set; }
    }
}