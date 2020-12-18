﻿using System;
using System.Collections.Generic;
using System.Text;
using Organize.Shared.Enitities;

namespace Organize.Shared.Entities
{
    public class BaseEntity : NotifyingObject
    {
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                if (_id == value)
                {
                    return;
                }

                _id = value;
                NotifyPropertyChanged();
            }
        }

        private int _id;
    }
}
