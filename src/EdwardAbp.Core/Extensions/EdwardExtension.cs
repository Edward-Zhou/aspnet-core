using Abp.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace EdwardAbp.Extensions
{
    public static class EdwardExtension
    {
        public static T ToValue<T>(string displayName) 
        {
            var result = typeof(T).GetFields()
                            .Where(m => m.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName == displayName)
                            .FirstOrDefault()
                            ?.GetValue(null);
            if (result == null)
            {
                throw new UserFriendlyException($"There is no corresponding Enum Item with DisplayName {displayName}!");
            }
            return (T)result;
        }
        public static string ToDisplayName(this Enum enumValue)
        {
            return enumValue.GetAttribute<DisplayNameAttribute>()?.DisplayName;
        }
        public static TAttribute GetAttribute<TAttribute>(this Enum enumValue) where TAttribute : Attribute
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<TAttribute>();
        }
    }
}
