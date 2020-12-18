using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Organize.Shared.Contracts;
using Organize.Shared.Enitities;
using Organize.Shared.Entities;
using Organize.Shared.Enums;

namespace Organize.TestFake
{
   public class TestData
    {
        public static User TestUser { get; private set; }

        public static void CreateTestUser(
            IUserItemManager userItemManager = null,
            IUserManager userManager = null)
        {
            var user = new User();
            user.Id = 123;
            user.UserName = "RB";
            user.FirstName = "Raghbir";
            user.LastName = "Badhan";
            user.Password = "test";
            user.GenderType = GenderTypeEnum.Male;
            user.UserItems = new ObservableCollection<BaseItem>();
            
            var textItem = new TextItem();
            user.UserItems.Add(textItem);
            textItem.ParentId = user.Id;
            textItem.Id = 1;
            textItem.Title = "Buy Apples";
            textItem.SubTitle = "Red | 5";
            textItem.Detail = "From New Zealand preferred";
            textItem.ItemTypeEnum = ItemTypeEnum.Text;
            textItem.Position = 1;



            var urlItem = new UrlItem();
            user.UserItems.Add(urlItem);
            urlItem.ParentId = user.Id;
            urlItem.Id = 2;
            urlItem.Title = "Buy Sunflowers";
            urlItem.Url = "https://drive.google.com/file/d/1NXiNFLEUGUiNtkyzdHDtf-HDocFh3OJ0/view?usp=sharing";
            urlItem.ItemTypeEnum = ItemTypeEnum.Url;
            urlItem.Position = 2;


           var parentItem = new ParentItem();
            user.UserItems.Add(parentItem);
            parentItem.ParentId = user.Id;
            parentItem.Id = 3;
            parentItem.Title = "Make Birthday Present";
            parentItem.ItemTypeEnum = ItemTypeEnum.ParentItem;
            parentItem.Position = 3;
            parentItem.ChildItems = new ObservableCollection<ChildItem>();


           var childItem = new ChildItem();
            parentItem.ChildItems.Add(childItem);
            childItem.ParentId = parentItem.Id;
            childItem.Id = 4;
            childItem.ItemTypeEnum = ItemTypeEnum.Child;
            childItem.Position = 1;
            childItem.Title = "Cut";

            TestUser = user;
        }
    }


}

