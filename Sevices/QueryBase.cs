using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using EFModel.MyModels;
using Logs;
using EFModel;
using System.Data.Entity.Infrastructure;
using Newtonsoft.Json.Linq;



namespace Sevices
{
    public class QueryBase
    {
        public Entities db;

        public QueryBase()
        {
            db = new Entities();
        }

        /// <summary>
        /// 根据sql语句查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public List<T> Query<T>(string sql, params object[] param) where T : class, new()
        {
            //注意使用EF SQL语句查询的时候必须要返回类型必须是实体类型  并且只能是属性不能是字段.
            return this.db.Database.SqlQuery<T>(sql, param).ToList();
        }


        /// <summary>
        /// 通过Sql语句查询数据
        /// </summary>
        /// <param name="sSql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public List<Dictionary<string, object>> QueryBySql(string sSql, object param)
        {
            DapperHelper.QueryBase dapper = new DapperHelper.QueryBase();//Dapper查询
            var entry = dapper.Query(sSql, param);
            return entry;
        }


        /// <summary>
        /// 通过Sql语句分页查询
        /// </summary>
        /// <typeparam name="T">必需是实体模型</typeparam>
        /// <param name="sql"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public string QueryPage(string sql, PageInfo info, object param)
        {

            string sSql = string.Format(@"DECLARE @rows int 
                                                SELECT @rows=COUNT(*) FROM({0}) as entry 
                                                SELECT  TOP 
                                                {1} *,@rows MaxRows FROM
                                                (SELECT  ROW_NUMBER() OVER(ORDER BY {2} {3}) AS Number,*
                                                FROM ({0}) AS query) AS entry 
                                                WHERE  Number>{1}*({4}-1) ", sql,
                                            info.rows,
                                            info.sort,
                                            info.order.ToString(),
                                            info.page);

            DapperHelper.QueryBase dapper = new DapperHelper.QueryBase();//Dapper查询

            var entry = dapper.Query(sSql, param);

            JObject job = new JObject();
            if (entry != null&&entry.Count>0)
            {
                job.Add(new JProperty("rows", C_Json.Array(C_Json.toJson(entry))));
                job.Add(new JProperty("total", entry[0]["MaxRows"].toInt32()));
            }
            else
            {
                job.Add(new JProperty("rows", new JArray()));
                job.Add(new JProperty("total", 0));
            }
            return job.ToString();
        }


    }
}
