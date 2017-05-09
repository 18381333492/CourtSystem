using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Common
{

    /// <summary>
    /// 导出报表的相关的封装
    /// </summary>
    public  class C_ExportExecl
    {

        /// <summary>
        /// 导出execl表格
        /// </summary>
        /// <param name="sTableHtml">Table内容</param>
        /// <param name="ExeclName">文件名称</param>
        public static void Export(string sTableHtml, string sExcelName)
        {
            HttpContext.Current.Response.ContentType = "application/force-download";
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + sExcelName + ".xls");
            HttpContext.Current.Response.Write("<html xmlns:x=\"urn:schemas-microsoft-com:office:excel\">");
            HttpContext.Current.Response.Write("<head>");
            HttpContext.Current.Response.Write("<META http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">");
            HttpContext.Current.Response.Write("<!--[if gte mso 9]><xml>");
            HttpContext.Current.Response.Write("<x:ExcelWorkbook>");
            HttpContext.Current.Response.Write("<x:ExcelWorksheets>");
            HttpContext.Current.Response.Write("<x:ExcelWorksheet>");
            HttpContext.Current.Response.Write("<x:Name>" + sExcelName + "</x:Name>");
            HttpContext.Current.Response.Write("<x:WorksheetOptions>");
            HttpContext.Current.Response.Write("<x:Print>");
            HttpContext.Current.Response.Write("<x:ValidPrinterInfo/>");
            HttpContext.Current.Response.Write("</x:Print>");
            HttpContext.Current.Response.Write("</x:WorksheetOptions>");
            HttpContext.Current.Response.Write("</x:ExcelWorksheet>");
            HttpContext.Current.Response.Write("</x:ExcelWorksheets>");
            HttpContext.Current.Response.Write("</x:ExcelWorkbook>");
            HttpContext.Current.Response.Write("</xml>");
            HttpContext.Current.Response.Write("<![endif]--> ");
            HttpContext.Current.Response.Write(sTableHtml.ToString());
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }
    }
}
