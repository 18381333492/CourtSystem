using EFModels.MyModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperHelper
{
    /// <summary>
    /// 查询接口
    /// </summary>
    public interface IReading
    {
        /// <summary>
        /// 查询一条数据
        /// </summary>
        /// <typeparam name="T">映射类型</typeparam>
        /// <param name="sqlCommand">sql</param>
        /// <param name="parameter">参数</param>
        /// <returns>查询结果</returns>
        T SingleQuery<T>(string sqlCommand, Object parameter=null) where T : new();

        /// <summary>
        /// 查询多条数据
        /// </summary>
        /// <typeparam name="T">映射类型</typeparam>
        /// <param name="sqlCommand">sql</param>
        /// <param name="parameter">参数</param>
        /// <returns>查询结果</returns>
        IList<T> QueryList<T>(string sqlCommand, Object parameter=null) where T : new();


        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T">映射类型</typeparam>
        /// <param name="sqlCommand">sql</param>
        /// <param name="parameter">参数</param>
        /// <returns>查询结果</returns>
        PagingRet<T> PageQuery<T>(string sqlCommand, PageInfo pageInfo, Object parameter=null) where T : new();
    }
}
