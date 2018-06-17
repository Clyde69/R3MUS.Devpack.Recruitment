using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;

namespace R3MUS.Devpack.Recruitment.Extensions
{
    public static class EnumExtensions
    {
        public static Dictionary<T, string> GetDisplayNames<T>()
            where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }
            var result = new Dictionary<T, string>();
            var values = Enum.GetValues(typeof(T));

            foreach (var value in values)
            {
                result.Add((T)value, ((Enum)value).GetDisplayName());
            }

            return result;
        }

        public static string GetDisplayName(this Enum enumValue)
        {
            if(enumValue.GetType().GetMember(enumValue.ToString()).First().GetCustomAttribute<DisplayAttribute>() != null)
            {
                return enumValue.GetType()
                                .GetMember(enumValue.ToString())
                                .First()
                                .GetCustomAttribute<DisplayAttribute>().Name;
            }
            return enumValue.ToString();
        }
    }
}