using EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFBaseHelper
{
    /// <summary>
    /// EF写操作的接口
    /// </summary>
    public interface IWriting
    {
        CourtEntities Context
        {
            get;
        }
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entry"></param>
        void Add<T>(T entry) where T : class, new();

        /// <summary>
        /// 编辑数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entry"></param>
        void Edit<T>(T entry) where T : class, new();

        /// <summary>
        /// 物理删除数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entry"></param>
        void Delete<T>(T entry) where T : class, new();

        /// <summary>
        /// 逻辑删除数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Ids">主键Ids集合</param>
        int Cancel<T>(string Ids, object services, string method) where T : class, new();

        /// <summary>
        /// 根据Sql语句执行
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">Sql语句</param>
        /// <param name="param"></param>
        /// <returns></returns>
        int ExcuteBySql(string sql, object services, string method, params object[] param);

        /// <summary>
        /// 无操作日志提交操作
        /// </summary>
        /// <returns></returns>
        int SaveChange();

    }
}
