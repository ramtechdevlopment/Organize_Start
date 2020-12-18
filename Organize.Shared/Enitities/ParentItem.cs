using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Organize.Shared.Entities;

namespace Organize.Shared.Enitities
{
    public class ParentItem : BaseItem
    {
        public ObservableCollection<ChildItem> ChildItems
        {
            get => _childItems;
            set
            {
                if (value == _childItems)
                {
                    return;
                }

                _childItems = value;
                NotifyPropertyChanged();
            }
        }
        private ObservableCollection<ChildItem> _childItems;
    }
}
