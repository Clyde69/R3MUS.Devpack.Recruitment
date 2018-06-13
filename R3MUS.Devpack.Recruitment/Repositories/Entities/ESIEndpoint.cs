using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace R3MUS.Devpack.Recruitment.Repositories.Entities
{
    public class ESIEndpoint
    {
        public int Id { get; set; }
        public string ClientId { get; set; }
        public string SecretKey { get; set; }
        public string CallbackUrl { get; set; }
        public string Name { get; set; }
    }
}