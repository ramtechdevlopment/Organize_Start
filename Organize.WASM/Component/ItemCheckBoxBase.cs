using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Organize.Shared.Entities;

namespace Organize.WASM.Component
{
    public class ItemCheckBoxBase  :ComponentBase
    {
        [Parameter]
        public BaseItem Item { get; set; }

        [CascadingParameter]
        public string ColorPrefix { get; set; }

       
    }
}
