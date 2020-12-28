using GeneralUi.DropdownControl;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using Organize.Shared.Entities;
using Organize.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Organize.Shared.Contracts;

namespace Organize.WASM.Pages
{
    public class SignUpBase : SignBase
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; }

        protected IList<DropdownItem<GenderTypeEnum>> GenderTypeDropDownItems { get; } = new List<DropdownItem<GenderTypeEnum>>();

        protected DropdownItem<GenderTypeEnum>SelectedGenderTypeDropDownItem { get; set; }

       [Inject]
       private IUserManager UserManager { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            var male = new DropdownItem<GenderTypeEnum>
            {
                ItemObject = GenderTypeEnum.Male,
                Displaytext = "Male"
            };

            var female = new DropdownItem<GenderTypeEnum>
            {
                ItemObject = GenderTypeEnum.Female,
                Displaytext = "Female"
            };

            var neutral = new DropdownItem<GenderTypeEnum>
            {
                ItemObject = GenderTypeEnum.Neutral,
                Displaytext = "Other"
            };

            GenderTypeDropDownItems.Add(male);
            GenderTypeDropDownItems.Add(female);
            GenderTypeDropDownItems.Add(neutral);

            SelectedGenderTypeDropDownItem = female;
            
            TryGetUserNameFromUri();
          //  User.UserName = Username;
        }

        private void TryGetUserNameFromUri()
        {
            var uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);
            StringValues sv;
            if(QueryHelpers.ParseQuery(uri.Query).TryGetValue("username", out sv))
            {
                User.UserName = sv;
            }
        }

        protected async void OnValidSubmit()
        {
            await UserManager.InsertUserAsync(User);
            NavigationManager.NavigateTo("signin");
        }

    }
}

