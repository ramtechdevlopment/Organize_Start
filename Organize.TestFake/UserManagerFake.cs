using Organize.Shared.Contracts;
using Organize.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Organize.TestFake
{
    public class UserManagerFake : IUserManager
    {
        public Task InsertUserAsync(User user)
        {
            return Task.FromResult(true);
        }

        public Task<User> TrySignInAndGetUserAsync(User user)
        {
            Console.WriteLine("Hello From Fake");
            return Task.FromResult(new User());
        }
    }
}
