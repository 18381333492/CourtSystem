using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperHelper
{
    public class QueryBase : SqlDbBase,IQuery
    {
        /// <summary>
        /// 查询数据列表
        /// </summary>
        /// <param name="sSql"></param>
        /// <param name="parameter"></param>
        public List<Dictionary<string, object>> Query(string sSql, object parameter)
        {
            using (SqlConnection conn = GetSqlConnection())
            {
                // conn.Query(sSql, param,)
                //获取查询结（DapperRow[类型是IEnumerable<dynamic>]）,并将其转换为字典
                var ret = conn.Query(sSql, parameter, null, true, null, CommandType.Text)
                                .Select(m => ((IDictionary<string, object>)m).ToDictionary(pi => pi.Key, pi => pi.Value))
                                .ToList();
                CloseConnect(conn);
                return ret;
            } 
        }
       
    }
}
