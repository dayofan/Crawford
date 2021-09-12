using Crawford.Infrastructure.Models;
using System.Collections.Generic;

namespace Crawford.Infrastructure
{
    public interface IInterviewRepository
    {
        IEnumerable<LossType> GetLossTypes();
        IEnumerable<User> GetUsers();
    }
}