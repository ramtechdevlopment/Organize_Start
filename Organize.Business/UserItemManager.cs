using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Organize.Shared.Contracts;
using Organize.Shared.Enitities;
using Organize.Shared.Entities;
using Organize.Shared.Enums;

namespace Organize.Business
{
    public class UserItemManager:IUserItemManager
    {
        public async Task<ChildItem> CreateNewChildItemAndAddItToParentItemAsync(ParentItem parent)
        {
            var childItem = new ChildItem();
            childItem.ParentId = parent.Id;
            childItem.ItemTypeEnum = ItemTypeEnum.Child;

            parent.ChildItems.Add(childItem);
            return await Task.FromResult(childItem) ;
        }

        public async Task<BaseItem> CreateNewUserItemAndAddItToUserAsync(User user, ItemTypeEnum typeEnum)
        {
            BaseItem item = null;
            switch (typeEnum)
            {
                case ItemTypeEnum.Text:
                    item = await CreateAndInsertItemAsync<TextItem>(user, typeEnum);
                    break;
                case ItemTypeEnum.Url:
                    item = await CreateAndInsertItemAsync<UrlItem>(user, typeEnum);
                    break;
                case ItemTypeEnum.ParentItem:
                    var parent = await CreateAndInsertItemAsync<ParentItem>(user, typeEnum);
                    parent.ChildItems = new ObservableCollection<ChildItem>();
                    item = parent;
                    break;
            }

            user.UserItems.Add(item);
            return item;
        }


        private async Task<T> CreateAndInsertItemAsync<T>(
            User user,
            ItemTypeEnum typeEnum) where T : BaseItem, new()
        {
            var item = new T();
            item.ItemTypeEnum = typeEnum;
            item.Position = user.UserItems.Count + 1;
            item.ParentId = user.Id;

          //  await _itemDataAccess.InsertItemAsync(item);

            return await Task.FromResult(item);
        }
    }
}
