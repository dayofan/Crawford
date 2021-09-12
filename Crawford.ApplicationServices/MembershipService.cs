using Crawford.Domain;
using Crawford.Infrastructure;
using System.Linq;

namespace Crawford.ApplicationServices
{
    public class MembershipService : IMembershipService
    {
        private readonly IInterviewRepository _interviewRepository;

        public MembershipService(IInterviewRepository interviewRepository)
        {
            _interviewRepository = interviewRepository ?? throw new System.ArgumentNullException(nameof(interviewRepository));
        }

        public bool IsUserValid(UserProfile userProfile) =>
            _interviewRepository.GetUsers().Any(u => u.UserName == userProfile.UserName && u.Password == userProfile.Password && u.Active == true);
    }
}
