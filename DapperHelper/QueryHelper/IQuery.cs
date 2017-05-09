using EFModel.MyModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperHelper
{
    /// <summary>
    /// 查询接口
    /// </summary>
    public interface IQuery
    {
        /// <summary>
        /// 分页查询数据列表
        /// </summary>
        /// <param name="sSql"></param>
        /// <param name="parameter"></param>
        List<Dictionary<string, object>> Query(string sSql, object parameter);
    }
}
