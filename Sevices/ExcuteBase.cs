using EFModel;
using Logs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sevices
{
    public class ExcuteBase
    {

        public Entities db;

        public ExcuteBase()
        {
            db = new Entities();
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entry"></param>
        public void Add<T>(T entry) where T:class,new()
        {
            this.db.Set<T>().Add(entry);
        }

        /// <summary>
        /// 编辑数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entry"></param>
        public void Edit<T>(T entry) where T : class, new()
        {
            this.db.Set<T>().Attach(entry);
            this.db.Entry<T>(entry).State = EntityState.Modified;//编辑
        }


        /// <summary>
        /// 物理删除数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entry"></param>
        public void Delete<T>(T entry) where T : class, new()
        {
            this.db.Set<T>().Add(entry);
            this.db.Entry<T>(entry).State = EntityState.Deleted;//删除
        }


        /// <summary>
        /// 逻辑删除数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Ids">主键Ids集合</param>
        public int Cancel<T>(string Ids, object services, string method) where T : class, new()
        {
            int res = 0;

            res = this.db.Database.
                ExecuteSqlCommand(string.Format(@"UPDATE
                                        [{0}] SET bIsDeleted = 1 
                                        WHERE ID IN({1})", typeof(T).Name, Ids));
            if (res > 0)
                Task.Factory.StartNew(() => LogHelper.OperateLog(services, method));
            return res;
        }

        /// <summary>
        /// 根据Sql语句执行
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">Sql语句</param>
        /// <param name="param"></param>
        /// <returns></returns>
        public int Excute(string sql, object services, string method, params object[] param)
        {
            int res = 0;
            res = this.db.Database.ExecuteSqlCommand(sql, param);
            if (res > 0)
                Task.Factory.StartNew(() => LogHelper.OperateLog(services, method));
            return res;
        }



        /// <summary>
        /// 提交操作
        /// </summary>
        /// <returns></returns>
        public int SaveChange(object services, string method)
        {
            int res = 0;
            res = db.SaveChanges();
            if (res > 0)
                Task.Factory.StartNew(()=> LogHelper.OperateLog(services, method));
            return res;
        }


        /// <summary>
        /// 无操作日志提交操作
        /// </summary>
        /// <returns></returns>
        public int SaveChange()
        {
            return db.SaveChanges();
        }
    }
}
