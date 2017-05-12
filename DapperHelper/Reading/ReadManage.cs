using Common;
using Dapper;
using EFModels.MyModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperHelper.Reading
{
    public class ReadManage
    {
        /// <summary>
        /// 查询单条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="conn">数据库连接字符串对象</param>
        /// <param name="sqlCommand">Sql语句</param>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        protected T DoSingleQuery<T>(SqlConnection conn, string sqlCommand, Object parameter=null) where T : new()
        {
            //如果泛型类型是字典，结果就是字典，否则就是设定的泛型类型
            var interfaceType = typeof(T).GetInterface("IDictionary");
            if (interfaceType != null)
            {
                var ret =
                            // 从数据查询结果集
                            conn.Query(
                                    sqlCommand,
                                    parameter,
                                    null,
                                    true,
                                    null,
                                CommandType.Text).
                              // 从结果集
                              Select(
                                    m => ((IDictionary<string, object>)m).
                                                            ToDictionary(
                                                                    pi => pi.Key,
                                                                    pi => pi.Value)).
                              FirstOrDefault<IDictionary<string, object>>();
                if (ret == default(IDictionary<string, object>)) return default(T);
                return (T)ret;
            }
            else
            {
                return conn.QueryFirstOrDefault<T>(sqlCommand, parameter, null, null, CommandType.Text);
            }
        }

        /// <summary>
        /// 查询多条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="conn">数据库连接字符串对象</param>
        /// <param name="sqlCommand">Sql语句</param>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        protected IList<T> DoQueryList<T>(SqlConnection conn, string sqlCommand, Object parameter=null)
        {
            //如果泛型类型是字典，结果就是字典，否则就是设定的泛型类型
            var interfaceType = typeof(T).GetInterface("IDictionary");
            if (interfaceType != null)
            {
                var ret = conn.Query(sqlCommand, parameter, null, true, null, CommandType.Text).
                                    Select(m => ((IDictionary<string, object>)m).
                                    ToDictionary(pi => pi.Key, pi => pi.Value)).
                                    ToList<IDictionary<string, object>>();
                return ret.Select(m => (T)m).ToList();
            }
            else
            {
                var ret = conn.Query<T>(sqlCommand, parameter, null, true, null, CommandType.Text);
                if (ret == null) return default(IList<T>);
                return ret.ToList();
            }
        }

        /// <summary>
        /// 分页获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="conn">数据库连接字符串对象</param>
        /// <param name="sqlCommand">Sql语句</param>
        /// <param name="pageInfo">分页参数</param>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        protected PagingRet<T> DoPaginationQuery<T>(SqlConnection conn, string sqlCommand, PageInfo pageInfo, Object parameter = null)
        {
            string sSql = string.Format(@"DECLARE @rows int 
                                                SELECT @rows=COUNT(*) FROM({0}) as entry 
                                                SELECT  TOP 
                                                {1} *,@rows MaxRows FROM
                                                (SELECT  ROW_NUMBER() OVER(ORDER BY {2} {3}) AS Number,*
                                                FROM ({0}) AS query) AS entry 
                                                WHERE  Number>{1}*({4}-1) ", sqlCommand,
                                          pageInfo.rows,
                                          pageInfo.sort,
                                          pageInfo.order.ToString(),
                                          pageInfo.page);

            var result = new PagingRet<T>();
            result.page = pageInfo.page;//当前页码数

            //获取查询结果（DapperRow[类型是IEnumerable<dynamic>]）,并将其转换为字典
            var ret = conn.Query(sSql, parameter, null, true, null, CommandType.Text).
                                Select(m => ((IDictionary<string, object>)m).ToDictionary(pi => pi.Key, pi => pi.Value)).
                                 ToList<IDictionary<string, object>>();
            if (ret.Count > 0)
            {
                //随意抽一条做最大条数记录
                result.total = ret[0]["MaxRows"].toInt32();
                //如果泛型类型是字典，分页结果里面的泛型集合就是字典集合，否则就是设定的泛型类型集合
                var interfaceType = typeof(T).GetInterface("IDictionary");
                if (typeof(T).Name.IndexOf("IDictionary") >= 0 || interfaceType != null)
                {
                    result.rows = ret.Select(m => (T)m).ToList();
                }
                else
                {
                    result.rows = ret.Select(kv =>
                    {
                        var properties = typeof(T).GetProperties();
                        var item = (T)(typeof(T).GetConstructor(new Type[] { }).Invoke(default(Type[])));
                        //反射给泛型对象赋值
                        //TODO:现在没得时间，有时间了这里不会用反射的方式来赋值，有时间改为表达式树来生成赋值的方法委托然后调用
                        Parallel.ForEach(properties, property =>
                        {
                            object value = null;
                            if (kv.TryGetValue(property.Name, out value))
                            {
                                property.SetValue(item, value);
                            }
                        });
                        return item;
                    }).ToList<T>();
                }
            }
            else
            {
                result.rows = new List<T>();
            }
            return result;
        }
        
    }
}
