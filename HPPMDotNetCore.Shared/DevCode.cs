using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Xml;
using Formatting = Newtonsoft.Json.Formatting;

namespace HPPMDotNetCore
{
    public static class DevCode
    {
        public static Dictionary<string, object> ToDictonary(this object obj)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            if (obj != null)
            {
                Type type = obj.GetType();
                PropertyInfo[] properties = type.GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    string name = property.Name;
                    object value = property.GetValue(obj);
                    result.Add(name, value);
                }
            }
            return result;
        }

        public static IDictionary<string, object> AddData(this IDictionary<string, object> values, string key, object value)
        {
            values.Add(key, value);
            return values;
        }

        public static string ToJson(this object obj, bool isFormat = false)
        {
            return JsonConvert.SerializeObject(obj,
                isFormat ?
                Newtonsoft.Json.Formatting.Indented :
                Formatting.None);
        }

        public static T ToObject<T>(this string str)
        {
            return JsonConvert.DeserializeObject<T>(str);
        }

        public static void ToLog(this object obj)
        {
            Console.WriteLine(JsonConvert.SerializeObject(obj, Formatting.Indented));
        }

        public static IQueryable<TSource> ToPagination<TSource>(this IQueryable<TSource> source, int pageNo, int pageSize) where TSource : class
        {
            int skipRowCount = (pageNo - 1) * pageSize;
            return source.Skip(skipRowCount).Take(pageSize);
        }
        
        public static bool IsNullOrEmpty(this string str)
        {
            return str == null || string.IsNullOrEmpty(str);
        }
    }
}
