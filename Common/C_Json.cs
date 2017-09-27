using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;

namespace Common
{
    public class C_Json
    {

        /// <summary>
        /// json序列化为字符串
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static string toJson(object m)
        {
            try
            {
                return JsonConvert.SerializeObject(m, new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
            }
            catch
            {
                return string.Empty;
            }
        }


        /// <summary>
        /// 反序列化为对象T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string jsonString) where T:class, new()
        {
            if (!string.IsNullOrEmpty(jsonString))
            {
                return JsonConvert.DeserializeObject<T>(jsonString);
            }
            else
            {
                return default(T);
            }
        }


        /// <summary>
        /// 反序列化JArray
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static JArray Array(string sJson)
        {
            return JArray.Parse(sJson);
        }

        /// <summary>
        /// 反序列化成JObject
        /// </summary>
        /// <param name="sJson"></param>
        /// <returns></returns>
        public static JObject ParseObject(string sJson)
        {
            if (!string.IsNullOrEmpty(sJson))
            {
                return JObject.Parse(sJson);
            }
            else
            {
                return default(JObject);
            }
        }

        /// <summary>
        /// 根据传来的字典组装json数据 
        /// </summary>
        /// <param name="collect"></param>
        /// <returns></returns>
        public static string JsonString(Dictionary<string,object> collect)
        {
            JObject job = new JObject();
            foreach(var prop in collect)
            {
                job.Add(new JProperty(prop.Key, prop.Value));
            }
            return job.ToString();
        }
    }
}
