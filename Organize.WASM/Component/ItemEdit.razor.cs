using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
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

        [Inject]
        private IUserItemManager UserItemManager { get; set; }

        private BaseItem Item { get; set; }=new BaseItem();

        private int TotalNumber { get; set; }

        private System.Timers.Timer _debounceTimer { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            _debounceTimer = new System.Timers.Timer();
            _debounceTimer.Interval = 500;
            _debounceTimer.AutoReset = false;
            _debounceTimer.Elapsed += HandleDebounceTimerElapsed;


            // ItemEditService.EditItemChanged += HandleEditItemChanged; 
            //if 1st item does not work
            //Item = ItemEditService;
            SetDataFromUri();
        }

        private void HandleDebounceTimerElapsed(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("timer elaspsed");
            UserItemManager.UpdateAsync(Item);
        }

        private void SetDataFromUri()
        {

            if (Item != null)
            {

                Item.PropertyChanged -= HandleItemPropertyChanged;
            }

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
                    Item.PropertyChanged += HandleItemPropertyChanged;
                    NavigationManager.LocationChanged += HandleLocationChanged;
                 StateHasChanged();
                }
            }
        }

        private async void HandleItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (_debounceTimer != null)
            {
                _debounceTimer.Stop();
                _debounceTimer.Start();
            } 
            // we using the debouncer method to avoid too many calls to DB etc for update method
            // await UserItemManager.UpdateAsync(Item);
        }

        private void HandleLocationChanged(object sender, LocationChangedEventArgs e)
        {
          SetDataFromUri();
        }

        //if we going to use service approach *************** OPTION *************************
        //private void HandleEditItemChanged(object sender, ItemEditEventArgs e)
        //{
        //    if (Item != null)
        //    {
        //        Item.PropertyChanged -= HandleItemPropertyChanged;
        //    }
        //    Item = e.Item;
        //    Item.PropertyChanged += HandleItemPropertyChanged;
        //    StateHasChanged();
           
        //}

        public void Dispose()
        {
            _debounceTimer.Dispose();
            NavigationManager.LocationChanged -= HandleLocationChanged;
            Item.PropertyChanged -= HandleItemPropertyChanged;
        }
    }
}
