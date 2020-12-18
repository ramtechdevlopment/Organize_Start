using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Organize.Shared.Entities;

namespace Organize.WASM.ItemEdit
{
    public class ItemEditService
    {
        public event EventHandler<ItemEditEventArgs> EditItemChanged;

        private BaseItem _editItem;
        //handle when there is a change
        public BaseItem EditItem
        {
            get { return _editItem; }
            set
            {
                if (_editItem == value)
                {
                    return;
                }

                _editItem = value;
                var args = new ItemEditEventArgs();
                args.Item = _editItem;
                OnEditItemChanged(args);
            }
        }

        protected virtual void OnEditItemChanged(ItemEditEventArgs e)
        {
            EventHandler<ItemEditEventArgs> handler = EditItemChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
