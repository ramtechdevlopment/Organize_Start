﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Organize.Shared.Contracts;

namespace Organize.WASM.Shared
{
    public partial class MainLayout : LayoutComponentBase
    {

        [Inject] 
        private ICurrentUserService CurrentUserService { get; set; }
        protected void SignOut()
        {

        }
    }
}
