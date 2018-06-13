using System;
using System.ComponentModel.DataAnnotations;

namespace R3MUS.Devpack.Recruitment.Models
{
    [MetadataType(typeof(CorporationModelMetaData))]
    public class CorporationModel : EntityBaseModel
    {
        public DateTime StartDate { get; set; }
    }
    public class CorporationModelMetaData
    {
        [Display(Name = "Corporation")]
        public string Name { get; set; }
    }
}