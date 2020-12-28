using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Organize.Shared.Entities;

namespace Organize.Shared.Contracts
{
   public interface IUserDataAccess
    {
        Task<bool> IsUserWithNameAvailableAsync(User user);
        Task InsertUserAsync(User user);
        Task<User> AuthenticateAndGetUserAsync(User user);
    }
}
