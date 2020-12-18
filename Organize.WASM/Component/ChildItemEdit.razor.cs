using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Organize.Business;
using Organize.Shared.Contracts;
using Organize.Shared.Enitities;

namespace Organize.WASM.Component
{


    public partial class ChildItemEdit  :ComponentBase
    {

        [Inject]
        private IUserItemManager UserItemManager { get; set; }

        [Parameter]
        public  ParentItem ParentItem { get; set; }

        private async void AddNewChildToParentAsync()
        {
            await UserItemManager.CreateNewChildItemAndAddItToParentItemAsync(ParentItem);
        }
    }
}
