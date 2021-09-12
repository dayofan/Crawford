using Crawford.Infrastructure.Models;
using System.Collections.Generic;

namespace Crawford.Infrastructure
{
    public class InterviewRepository : IInterviewRepository
    {
        private readonly InterviewDbContext _context;

        public InterviewRepository(InterviewDbContext context)
        {
            _context = context ?? throw new System.ArgumentNullException(nameof(context));
        }

        public IEnumerable<User> GetUsers() => _context.Users;

        public IEnumerable<LossType> GetLossTypes() => _context.LossTypes;
    }
}
