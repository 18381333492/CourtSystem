using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace DapperHelper
{
    public class SqlDbBase
    {

        protected string connectionStr = null;

        //获取数据库连接        
        protected SqlConnection GetSqlConnection()
        {
            try
            {
                if (connectionStr == null) connectionStr = C_Config.ReadConnString("DapperConnection");
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
    }
}
