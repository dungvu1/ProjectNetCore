using Newtonsoft.Json;
using System.Runtime.Serialization.Formatters.Binary;

namespace DOAN.Extension
{
    public static class Extension
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            var jsonValue = JsonConvert.SerializeObject(value);
            session.SetString(key, jsonValue);
        }

        public static T Get<T>(this ISession session, string key)
        {
            var jsonValue = session.GetString(key);
            if (!string.IsNullOrEmpty(jsonValue))
            {
                return JsonConvert.DeserializeObject<T>(jsonValue);
            }
            return default(T);
        }
    }
}
