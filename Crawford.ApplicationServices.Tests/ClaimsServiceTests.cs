using Crawford.Domain;
using Crawford.Infrastructure;
using Crawford.Infrastructure.Models;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Crawford.ApplicationServices.Tests
{
    public class ClaimsServiceTests
    {
        private readonly Mock<IInterviewRepository> _mockInterviewRepository;
        private readonly ClaimsService _sut;

        public ClaimsServiceTests()
        {
            _mockInterviewRepository = new Mock<IInterviewRepository>();
            _sut = new ClaimsService(_mockInterviewRepository.Object);
        }

        [Fact]
        public void GetLossTypeData_WhenCalled_ReturnLossTypeData()
        {
            var lossTypeData = new LossTypeData
            {
                LossTypeId = 1,
                LossTypeCode = "A",
                LossTypeDescription = "Desc"
            };

            var lossTypes = new List<LossType>
            {
                new LossType
                {
                    LossTypeId = lossTypeData.LossTypeId,
                    LossTypeCode = lossTypeData.LossTypeCode,
                    LossTypeDescription = lossTypeData.LossTypeDescription
                }
            };

            _mockInterviewRepository.Setup(r => r.GetLossTypes()).Returns(lossTypes);

            var result = _sut.GetLossTypeData().ToList();

            Assert.NotEmpty(result);
            Assert.Equal(result[0].LossTypeId, lossTypeData.LossTypeId);
            Assert.Equal(result[0].LossTypeCode, lossTypeData.LossTypeCode);
            Assert.Equal(result[0].LossTypeDescription, lossTypeData.LossTypeDescription);
        }
    }
}
