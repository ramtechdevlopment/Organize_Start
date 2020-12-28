using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Organize.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Organize.Shared.Contracts;

namespace Organize.WASM.Pages
{
    public class SignInBase : SignBase
    {
        protected string Day { get; } = DateTime.Now.DayOfWeek.ToString();

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        private IUserManager UserManager { get; set; }


        //[Inject]
        //private IModalService ModalService { get; set; }


        //[Inject]
        //private BusyOverlayService BusyOverlayService { get; set; }

        [Inject]
        private ICurrentUserService CurrentUserService { get; set; }

        //[Inject]
        //private IAuthenticationStateProvider AuthenticationStateProvider { get; set; }

        public bool ShowPassword { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            User = new User
            {
                FirstName = "X",
                LastName = "X",
                PhoneNumber = "123"
            };

            EditContext = new EditContext(User);
        }

        protected async void OnSubmit()
        {
            if (!EditContext.Validate())
            {
                return;
            }

            var foundUser = await UserManager.TrySignInAndGetUserAsync(User);

            if (foundUser != null)
            {
                CurrentUserService.CurrentUser = foundUser;
                NavigationManager.NavigateTo("items");
            }

        }

        //protected string Username { get; set; } = "Ben";

        //protected void HandleUserNameChanged(ChangeEventArgs eventArgs)
        //{
        //    Username = eventArgs.Value.ToString();
        //}

        //protected void HandleUserNameValueChanged(string value)
        //{
        //    Username = value;
        //}
    }
}

