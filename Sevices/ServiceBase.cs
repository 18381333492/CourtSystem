using EFModel.MyModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sevices
{

    public  class ServiceBase
    {
  
        protected ExcuteBase excute = new ExcuteBase();
       
        protected QueryBase query = new QueryBase();

        /// <summary>
        /// 操作人
        /// </summary>
        public string sUserName
        {
            get;
            set;
        }

        /// <summary>
        /// Ip地址
        /// </summary>
        public string sIpAddress
        {
            get;
            set;
        }
        
    }
}
