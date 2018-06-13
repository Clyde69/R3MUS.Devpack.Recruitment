using System.Collections.Generic;

namespace R3MUS.Devpack.Recruitment.Repositories.Entities
{
    public class Recruit
    {
        public int Id { get; set; }
        public long CharacterId { get; set; }
        public ICollection<History> History { get; set; }
        public virtual ICollection<TokenData> TokenData { get; set; }
        public virtual ICollection<TokenShare> TokenShare { get; set; }
    }
}