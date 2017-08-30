using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using DapperHelper.Reading;
using EFModels.MyModels;

namespace DapperHelper
{
    public class SqlDbBase: ReadManage,IReading
    {
        protected string connectionStr = null;

        //获取数据库连接        
        protected SqlConnection GetSqlConnection()
        {
            try
            {
                if (connectionStr == null) SetconnectionStr();
                SqlConnection conn = new SqlConnection(connectionStr);
                conn.Open();
                return conn;
            }
            catch (Exception ex)
            {
                Logs.LogHelper.ErrorLog(ex);
                return null;
            }
        }


        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        /// <param name="conn"></param>
        protected void CloseConnect(SqlConnection conn)
        {
            try
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                Logs.LogHelper.ErrorLog(ex);
            }
        }

        /// <summary>
        /// 设置连接字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public void  SetconnectionStr(string str=null)
        {
            if (string.IsNullOrEmpty(str))
                connectionStr= C_Config.ReadConnString("DapperConnection");
            else
                connectionStr=str;
        }

        /// <summary>
        /// 根据主键ID获取实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ID"></param>
        /// <returns></returns>
        public T Find<T>(string ID) where T : new()
        {
            SqlConnection conn = null;
            try
            {
                conn = GetSqlConnection();
                if (conn == null) throw new ApplicationException("未获取到连接对象。");
                string sqlCommand = string.Format("SELECT * FROM {0} WHERE ID=@ID",typeof(T).Name);
                return DoSingleQuery<T>(conn, sqlCommand, new { ID=ID});
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnect(conn);
            }
        }

        /// <summary>
        /// 根据条件查询是否存在相应的数据
        /// </summary>
        /// <param name="sqlCommand">sql命令</param>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        public bool Any(string sqlCommand, Object parameter = null)
        {
            SqlConnection conn = null;
            try
            {
                conn = GetSqlConnection();
                if (conn == null) throw new ApplicationException("未获取到连接对象。");
                return DoAny(conn, sqlCommand, parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnect(conn);
            }
        }

        /// <summary>
        /// 单条查询
        /// </summary>
        /// <typeparam name="T">查询的对象类型</typeparam>
        /// <param name="sqlCommand">sql命令</param>
        /// <param name="parameter">参数</param>
        /// <returns>查询结果</returns>
        public T SingleQuery<T>(string sqlCommand, Object parameter)where T : new()
        {
            SqlConnection conn = null;
            try
            {
                conn = GetSqlConnection();
                if (conn == null) throw new ApplicationException("未获取到连接对象。");
                return DoSingleQuery<T>(conn, sqlCommand, parameter);
            }
            catch (Exception ex)
            {
               // Logs.GetLog().WriteErrorLog(ex);
                return default(T);
            }
            finally
            {
                CloseConnect(conn);
            }
        }


        /// <summary>
        /// 查询多条数据
        /// </summary>
        /// <typeparam name="T">查询的对象类型</typeparam>
        /// <param name="sqlCommand">sql命令</param>
        /// <param name="parameter">参数</param>
        /// <returns>查询结果</returns>
        public IList<T> QueryList<T>(string sqlCommand, Object parameter) where T : new()
        {
            SqlConnection conn = null;
            try
            {
                conn = GetSqlConnection();
                if (conn == null) throw new ApplicationException("未获取到连接对象。");
                return DoQueryList<T>(conn, sqlCommand, parameter);
            }
            catch (Exception ex)
            {
                // Logs.GetLog().WriteErrorLog(ex);
                return null;
            }
            finally
            {
                CloseConnect(conn);
            }
        }


        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sqlCommand">SQL语句</param>
        /// <param name="pageInfo">分页参数</param>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        public PagingRet<T> PageQuery<T>(string sqlCommand, PageInfo pageInfo, Object parameter) where T : new()
        {
            SqlConnection conn = null;
            try
            {
                conn = GetSqlConnection();
                if (conn == null) throw new ApplicationException("未获取到连接对象。");
                return DoPaginationQuery<T>(conn, sqlCommand, pageInfo, parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnect(conn);
            }
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sqlCommand">SQL语句</param>
        /// <param name="pageInfo">分页参数</param>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        public PagingRet PageQuery(string sqlCommand, PageInfo pageInfo, Object parameter)
        {
            SqlConnection conn = null;
            try
            {
                conn = GetSqlConnection();
                if (conn == null) throw new ApplicationException("未获取到连接对象。");
                return DoPaginationQuery(conn, sqlCommand, pageInfo, parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnect(conn);
            }
        }
    }
}
