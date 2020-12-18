using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using System.Text;
using System.Xml.Serialization;
using Organize.Shared.Enitities;

namespace Organize.Shared.Enums
{
   public enum ItemTypeEnum
    {
            Text = 1,
            Url =2,
            ParentItem =3,
            Child=4,
            
    }
}
