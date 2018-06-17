using R3MUS.Devpack.ESI.Models.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace R3MUS.Devpack.Recruitment.Models
{
    public class AllianceContactModel : ContactModel
    {
        private ESI.Models.Alliance.Detail _alliance;

        public ESI.Models.Alliance.Detail Alliance
        {
            get
            {
                if (_alliance == null)
                {
                    _alliance = new ESI.Models.Alliance.Detail(ContactId);
                }

                return _alliance;
            }
        }
    }
}