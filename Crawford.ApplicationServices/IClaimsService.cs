using Crawford.Domain;
using System.Collections.Generic;

namespace Crawford.ApplicationServices
{
    public interface IClaimsService
    {
        IEnumerable<LossTypeData> GetLossTypeData();
    }
}