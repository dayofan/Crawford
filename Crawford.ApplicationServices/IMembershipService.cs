using Crawford.Domain;
using System.Collections.Generic;

namespace Crawford.ApplicationServices
{
    public interface IMembershipService
    {
        bool IsUserValid(UserProfile userProfile);
    }
}