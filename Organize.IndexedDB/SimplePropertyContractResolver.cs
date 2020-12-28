﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Organize.IndexedDB
{
    class SimplePropertyContractResolver  : DefaultContractResolver
    {
        public SimplePropertyContractResolver()
        {
                NamingStrategy = new CamelCaseNamingStrategy();
        }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            

            var property = base.CreateProperty(member, memberSerialization);

            var propertyType = property.PropertyType;
            var isSimpleProperty = propertyType == typeof(int) || propertyType == typeof(string) ||
                                   propertyType == typeof(decimal) || propertyType == typeof(float) ||
                                   propertyType == typeof(double) || propertyType == typeof(bool)
                                   || propertyType.IsEnum;

            if (isSimpleProperty)
            {
                property.ShouldSerialize = instance => true;
            }
            else
            {
                //do not serialize lists , array etc...
                property.ShouldSerialize = instance => false;
            }
            return property;
        }
    }
}
