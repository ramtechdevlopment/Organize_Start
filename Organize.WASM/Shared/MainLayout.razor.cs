using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Organize.Shared.Contracts;

namespace Organize.WASM.Shared
{
    public partial class MainLayout : LayoutComponentBase
    {

        [Inject] 
        private ICurrentUserService CurrentUserService { get; set; }

        [Inject]
        private IJSRuntime JSRuntime { get; set; }



        protected void SignOut()
        {

        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            var width = await JSRuntime.InvokeAsync<int>("blazorDimension.getWidth");
            Console.WriteLine("width:" + width);
        }

        [JSInvokable]
        public static void OnResize()
        {
          //  Console.WriteLine("OnResize in C# .net");

        }
    }
}
