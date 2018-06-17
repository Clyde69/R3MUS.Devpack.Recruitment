using R3MUS.Devpack.ESI.Models.Mail;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace R3MUS.Devpack.Recruitment.Models
{
    [MetadataType(typeof(CharacterModelMetaData))]
    public class CharacterModel : EntityBaseModel
    {
        public DateTime Birthday { get; set; }
        public long SkillPoints { get; set; }

        public CorporationModel Corporation { get; set; }
        public AllianceModel Alliance { get; set; }
        public List<CharacterContactModel> Contacts { get; set; }
        public List<MailHeaderModel> Mails { get; set; }
        public List<CorporationModel> EmploymentHistory { get; set; }
        public List<ESI.Models.Corporation.Summary> AuthorisedCorporations { get; set; }

        public string AccountStatus { get; set; }
    }

    public class CharacterModelMetaData
    {
        [Display(Name = "Character Name")]
        public string Name { get; set; }
        [Display(Name = "Account Status")]
        public string AccountStatus { get; set; }
    }
}