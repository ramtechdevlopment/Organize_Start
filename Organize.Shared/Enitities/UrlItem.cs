﻿using System;
using System.Collections.Generic;
using System.Text;
using Organize.Shared.Entities;

namespace Organize.Shared.Enitities
{
    public class UrlItem:BaseItem
    {
        public string Url
        {
            get => _url;
            set => SetProperty(ref _url, value);
        }
        private string _url;
    }
}
