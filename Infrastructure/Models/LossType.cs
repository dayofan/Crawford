using System;

#nullable disable

namespace Crawford.Infrastructure.Models
{
    public partial class LossType
    {
        public int LossTypeId { get; set; }
        public string LossTypeCode { get; set; }
        public string LossTypeDescription { get; set; }
        public bool? Active { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public int LastUpdatedId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedId { get; set; }
    }
}
