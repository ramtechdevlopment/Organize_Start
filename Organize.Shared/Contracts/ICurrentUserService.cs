using System;
using System.Collections.Generic;
using System.Text;
using Organize.Shared.Entities;

namespace Organize.Shared.Contracts
{
   public interface ICurrentUserService
    {
        User CurrentUser { get; set; }


    }
}
