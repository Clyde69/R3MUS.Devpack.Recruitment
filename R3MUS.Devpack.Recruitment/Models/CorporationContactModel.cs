using R3MUS.Devpack.ESI.Models.Character;

namespace R3MUS.Devpack.Recruitment.Models
{
    public class CorporationContactModel : ContactModel
    {
        private ESI.Models.Corporation.Detail _corporation;

        public ESI.Models.Corporation.Detail Corporation
        {
            get
            {
                if (_corporation == null)
                {
                    _corporation = new ESI.Models.Corporation.Detail(ContactId);
                }

                return _corporation;
            }
        }
    }
}