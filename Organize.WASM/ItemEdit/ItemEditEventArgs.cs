using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Organize.Shared.Entities;

namespace Organize.WASM.ItemEdit
{
    public class ItemEditEventArgs  :EventArgs
    {
        public BaseItem Item { get; set; }
    }
}
