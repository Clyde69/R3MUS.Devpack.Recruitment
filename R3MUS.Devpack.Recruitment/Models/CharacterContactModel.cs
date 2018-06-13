using R3MUS.Devpack.ESI.Models.Character;
using System.ComponentModel.DataAnnotations;

namespace R3MUS.Devpack.Recruitment.Models
{
    public class CharacterContactModel : ContactModel
    {
        private ESI.Models.Character.Detail _character;

        public ESI.Models.Character.Detail Character
        {
            get
            {
                if (_character == null)
                {
                    _character = new ESI.Models.Character.Detail(ContactId);
                }

                return _character;
            }
        }
    }
}