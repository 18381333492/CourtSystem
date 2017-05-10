using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFModels.MyModels
{
    /// <summary>
    /// 分页结果
    /// </summary>
    /// <typeparam name="T">分页承载数据的类型</typeparam>
    public class PagingRet<T>
    {

        /// <summary>
        /// 当前页码数
        /// </summary>
        public int page { get; set; }

        /// <summary>
        /// 最大条数
        /// </summary>
        public int total { get; set; } = 0;

        /// <summary>
        /// 分页的结果
        /// </summary>
        public List<T> rows { get; set; }

        /// <summary>
        /// 转化为Json字符串
        /// </summary>
        /// <returns></returns>
        public string toJson()
        {
            return C_Json.toJson(this);
        }
    }
}
