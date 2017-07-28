using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
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
            
            
        }


        /// <summary>
        /// 写错误日志
        /// </summary>
        public static void ErrorLog(Exception ex,string sUrl=null)
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
                w.WriteLine("-------------  异常信息   ---------------------------------------------------------------");
                w.WriteLine("发生时间：" + DateTime.Now.ToString());
                w.WriteLine("发生异常页：" + sUrl);
                w.WriteLine("异常信息：" + ex.Message);
                if (ex.InnerException != null)
                    w.WriteLine("InnerException:{0}", ex.InnerException.Message);
                w.WriteLine("TargetSite.Name:{0}", ex.TargetSite.Name);
                w.WriteLine("错误源：" + ex.Source);
                w.WriteLine("堆栈信息：" + ex.StackTrace);
                w.WriteLine("-----------------------------------------------------------------------------------------");
                w.WriteLine("-----------------------------------------------------------------------------------------");
                w.Flush();
                w.Close();
            }
        }
    }
}
