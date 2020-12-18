﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Organize.Business;
using Organize.Shared.Contracts;
using Organize.Shared.Entities;
using Organize.Shared.Enums;
using Organize.WASM.ItemEdit;

namespace Organize.WASM.Component
{
    public partial class ItemEdit : ComponentBase,IDisposable
    {
        //[Inject]
        //private ItemEditService ItemEditService { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }


        [Inject]
        private ICurrentUserService CurrentUserService { get; set; }

        private BaseItem Item { get; set; }=new BaseItem();

        private int TotalNumber { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
           // ItemEditService.EditItemChanged += HandleEditItemChanged; 
            //if 1st item does not work
            //Item = ItemEditService;
            SetDataFromUri();
        }

        private void SetDataFromUri()
        {


            var uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);

            var segmentCount = uri.Segments.Length;
            if (segmentCount > 1 //changed for test from 2
                && Enum.TryParse(typeof(ItemTypeEnum), uri.Segments[segmentCount - 2].Trim('/'), out var typeEnum)
                && int.TryParse(uri.Segments[segmentCount - 1], out var id))
            {
                var userItem = CurrentUserService.CurrentUser
                    .UserItems
                    .SingleOrDefault(item => item.ItemTypeEnum == (ItemTypeEnum)typeEnum && item.Id == id);

                //Not found? Redirect to items
                if (userItem == null)
                {
                    NavigationManager.LocationChanged -= HandleLocationChanged;
                    NavigationManager.NavigateTo("/items");
                }
                else
                {
                    Item = userItem;
                    NavigationManager.LocationChanged += HandleLocationChanged;
                 StateHasChanged();
                }
            }
        }

        private void HandleLocationChanged(object sender, LocationChangedEventArgs e)
        {
          SetDataFromUri();
        }

        private void HandleEditItemChanged(object sender, ItemEditEventArgs e)
        {
            Item = e.Item;
            StateHasChanged();
        }

        public void Dispose()
        {
            NavigationManager.LocationChanged -= HandleLocationChanged;
        }
    }
}
