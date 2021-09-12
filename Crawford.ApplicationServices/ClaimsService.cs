using Crawford.Domain;
using Crawford.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawford.ApplicationServices
{
    public class ClaimsService : IClaimsService
    {
        private readonly IInterviewRepository _interviewRepository;

        public ClaimsService(IInterviewRepository interviewRepository)
        {
            _interviewRepository = interviewRepository ?? throw new ArgumentNullException(nameof(interviewRepository));
        }

        public IEnumerable<LossTypeData> GetLossTypeData() =>
            _interviewRepository.GetLossTypes().Select(l => new LossTypeData
            {
                LossTypeId = l.LossTypeId,
                LossTypeCode = l.LossTypeCode,
                LossTypeDescription = l.LossTypeDescription
            });
    }
}
