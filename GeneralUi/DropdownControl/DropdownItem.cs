using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralUi.DropdownControl
{
    public class DropdownItem<T>
    {
        [Parameter]
        public string Displaytext { get; set; }

        [Parameter]
        public T ItemObject { get; set; }
        
    }
}
