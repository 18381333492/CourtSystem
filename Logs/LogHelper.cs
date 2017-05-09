using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using EFModel;
using System.IO;
using System.Globalization;

namespace Logs
{
    /// <summary>
    /// 日志帮助类
    /// </summary>
    public class LogHelper
    {


        /// <summary>
        /// 写操作日志
        /// </summary>
        /// <param name="method">方法名</param>
        /// <param name="parameter">参数</param>
        public static void OperateLog(object services,string method)
        {
            
            //反射获取用户信息和Ip地址
            string sUserName=services.GetType().GetProperty("sUserName").GetValue(services, null).ToString();
            string sIpAddress=services.GetType().GetProperty("sIpAddress").GetValue(services, null).ToString();

            MethodInfo p = services.GetType().GetMethod(method);
            var log=p.GetCustomAttribute(typeof(LogAttribute)) as LogAttribute;
            string sContent = log.type.ToString()+log.sModuleName;
            
            using (var db=new Entities())
            {
               db.OperateLog.Add(new OperateLog()
                {
                    ID = Guid.NewGuid(),
                    dInsertTime = DateTime.Now,
                    sCity = string.Empty,
                    sUserName = sUserName,
                    sIpAddress = sIpAddress,
                    sContent= sContent
               });
                db.SaveChanges();
            }      
        }


        /// <summary>
        /// 写错误日志
        /// </summary>
        public static void ErrorLog(Exception e)
        {
            string sDirectoryPath = AppDomain.CurrentDomain.BaseDirectory +"ErrorLogs\\Error\\";//文件夹路径
            if (!Directory.Exists(sDirectoryPath))
            {//检查文件夹是否存在
                Directory.CreateDirectory(sDirectoryPath);
            }
            string path = sDirectoryPath + DateTime.Now.ToString("yyyyMMdd") + ".txt";
            if (!File.Exists(path))
            {//检查文件是否存在
                File.Create(path).Close();
            }
            using (StreamWriter w = File.AppendText(path))
            {
                w.WriteLine("\r\nLog Entry : ");
                w.WriteLine("{0}", DateTime.Now.ToString(CultureInfo.InvariantCulture));
                w.WriteLine("Message:{0}",e.Message);
                if (e.InnerException != null) 
                w.WriteLine("InnerException:{0}", e.InnerException.Message);
                w.WriteLine("TargetSite.Name:{0}", e.TargetSite.Name);
                w.WriteLine("Source:{0}", e.Source);
                w.WriteLine("StackTrace:{0}", e.StackTrace);
                w.WriteLine("_____________________________________________________________________________________________________");
                w.WriteLine("_____________________________________________________________________________________________________");
                w.Flush();
                w.Close();
            }
        }
    }
}
