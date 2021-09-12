using Crawford.Domain;
using Crawford.Infrastructure;
using Crawford.Infrastructure.Models;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Crawford.ApplicationServices.Tests
{
    public class MembershipServiceTests
    {
        private readonly Mock<IInterviewRepository> _mockInterviewRepository;
        private readonly MembershipService _sut;

        public MembershipServiceTests()
        {
            _mockInterviewRepository = new Mock<IInterviewRepository>();
            _sut = new MembershipService(_mockInterviewRepository.Object);
        }

        [Fact]
        public void IsValid_WhenUserExistsAndIsActive_ReturnTrue()
        {
            var userProfile = new UserProfile
            {
                UserName = "userName",
                Password = "password",
                Active = true
            };

            var users = new List<User>
            {
                new User
                {
                    UserName = userProfile.UserName,
                    Password = userProfile.Password,
                    Active = userProfile.Active
                }
            };

            _mockInterviewRepository.Setup(r => r.GetUsers()).Returns(users);

            var isUserValid = _sut.IsUserValid(userProfile);

            Assert.True(isUserValid);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(null)]
        public void IsValid_WhenUserExistsAndIsNotActive_ReturnFalse(bool? active)
        {
            var userProfile = new UserProfile
            {
                UserName = "userName",
                Password = "password",
                Active = active
            };

            var users = new List<User>
            {
                new User
                {
                    UserName = userProfile.UserName,
                    Password = userProfile.Password,
                    Active = userProfile.Active
                }
            };

            _mockInterviewRepository.Setup(r => r.GetUsers()).Returns(users);

            var isUserValid = _sut.IsUserValid(userProfile);

            Assert.False(isUserValid);
        }

        [Fact]
        public void IsValid_WhenUserDoesNotExist_ReturnFalse()
        {
            var userProfile = new UserProfile
            {
                UserName = "userName",
                Password = "password",
                Active = true
            };

            var isUserValid = _sut.IsUserValid(userProfile);

            Assert.False(isUserValid);
        }
    }
}
