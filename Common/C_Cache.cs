using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// 缓存相关封装
    /// </summary>
    public  class C_Cache
    {

        /// <summary>
        /// 设置缓存
        /// 如果设置相同的key的会覆盖原来的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="seconds"></param>
        public static void SetCache(string key,string value,int seconds)
        {
            MemoryCache.Default.Set(key, value, DateTimeOffset.UtcNow.AddSeconds(seconds));
        }


        /// <summary>
        /// 添加缓存
        /// 如果设置相同的key的不会覆盖原来的值（直到缓存失效）
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="seconds"></param>
        public static void AddCache(string key, string value, int seconds)
        {
            MemoryCache.Default.Add(key, value, DateTimeOffset.UtcNow.AddSeconds(seconds));
        }


        /// <summary>
        /// 获取指定键缓存的值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object GetCache(string key)
        {
            return  MemoryCache.Default.Get(key);
        }

        /// <summary>
        /// 删除指定键的缓存
        /// </summary>
        /// <param name="key"></param>
        public static void RemoveCache(string key)
        {
            MemoryCache.Default.Remove(key);
        }

    }
}
