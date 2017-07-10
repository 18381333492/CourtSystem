using EFModels.MyModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemInterface;
using EFModels;
using Common;

namespace SystemModel
{
    /// <summary>
    ///微信关键字写操作相关
    /// </summary>
    public partial class WeChatKeyWordServices : IWeChatKeyWord
    {
        /// <summary>
        /// 添加微信关键字
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public override int Insert(CDELINK_WeChatKeyWord keyWord)
        {
            keyWord.ID = Guid.NewGuid();
            keyWord.dInsertTime = DateTime.Now;
            keyWord.dUpdateTime = keyWord.dInsertTime;
            excute.Add<CDELINK_WeChatKeyWord>(keyWord);
            return excute.SaveChange();
        }

        /// <summary>
        /// 编辑微信关键字
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public override int Update(CDELINK_WeChatKeyWord keyWord)
        { 
            keyWord.dUpdateTime = DateTime.Now;
            excute.Edit<CDELINK_WeChatKeyWord>(keyWord);
            return excute.SaveChange();
        }


        /// <summary>
        ///  根据关键字ID集合删除关键字
        /// </summary>
        /// <param name="sButtonId"></param>
        /// <returns></returns>
        public override int Cancel(string Ids)
        {
           return  excute.Delete<CDELINK_WeChatKeyWord>(Ids);
        }
    }
}
