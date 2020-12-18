using System;
using System.Collections.Generic;
using System.Text;
using Organize.Shared.Contracts;
using Organize.Shared.Entities;

namespace Organize.Business
{
    public class CurrentUserService : ICurrentUserService
    {
        public User CurrentUser { get; set; }
    }
}
