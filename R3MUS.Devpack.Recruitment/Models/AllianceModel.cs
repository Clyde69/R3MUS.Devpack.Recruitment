using System.ComponentModel.DataAnnotations;

namespace R3MUS.Devpack.Recruitment.Models
{
    [MetadataType(typeof(AllianceModelMetaData))]
    public class AllianceModel : EntityBaseModel
    {
        public static AllianceModel GetAllianceInfo(long? allianceId)
        {
            if (!allianceId.HasValue)
            {
                return new AllianceModel
                {
                    Id = -1,
                    Name = "N/A"
                };
            }
            var alliance = new ESI.Models.Alliance.Detail(allianceId.Value);
            return new AllianceModel
            {
                Id = allianceId.Value,
                Name = new ESI.Models.Alliance.Detail(allianceId.Value).Name
            };
        }
    }
    public class AllianceModelMetaData
    {
        [Display(Name = "Alliance")]
        public string Name { get; set; }
    }
}