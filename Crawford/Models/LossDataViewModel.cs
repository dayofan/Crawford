using System.ComponentModel.DataAnnotations;

namespace Crawford.Web.Models
{
    public class LossDataViewModel
    {
        [Display(Name = "Loss Type Id")]
        public int LossTypeId { get; set; }
        [Display(Name = "Loss Type Code")]
        public string LossTypeCode { get; set; }
        [Display(Name = "Loss Type Description")]
        public string LossTypeDescription { get; set; }
    }
}
