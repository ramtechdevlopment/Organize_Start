using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Organize.Shared.Contracts;
using Organize.Shared.Entities;
using Organize.WASM.ItemEdit;

namespace Organize.WASM.Component
{
    public partial class ItemsList:ComponentBase, IDisposable
    {
        [Inject]
        private ICurrentUserService CurrentUserService {get; set; }

        [Inject]
        private IUserItemManager userItemManager { get; set; }

        [Inject]
        private ItemEditService ItemEditedService { get; set; }
        private ObservableCollection<BaseItem> UserItems { get; set; } = new ObservableCollection<BaseItem>();



        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            await userItemManager.RetrieveAllUserItemsOfUserAndSetToUserAsync(CurrentUserService.CurrentUser);
            UserItems = CurrentUserService.CurrentUser.UserItems;
            UserItems.CollectionChanged += HandleUserItemsCollectionChanged;

        }

        private void HandleUserItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            StateHasChanged();
        }

        private void OnBackgroundClicked()
        {

            ItemEditedService.EditItem = null;

        }

        public void Dispose()
        {
            UserItems.CollectionChanged -= HandleUserItemsCollectionChanged;
        }
    }
}
