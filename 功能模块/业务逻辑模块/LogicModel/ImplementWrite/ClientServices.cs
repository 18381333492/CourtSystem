using LogicHandlerInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFModels;

namespace LogicHandlerModel
{

    /// <summary>
    /// 会员的相关的写操作
    /// </summary>
    public partial class ClientServices:IClient
    {

        /// <summary>
        /// 关注微信公众号注册会员
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public override int AddClient(ES_Client client)
        {
            client.ID = Guid.NewGuid();
            excute.Add<ES_Client>(client);
            return excute.SaveChange();
        }


        /// <summary>
        /// 再次关注修改会员关注状态
        /// </summary>
        /// <param name="sOpenId"></param>
        /// <returns></returns>
        public override int SubscribeEditClient(string sOpenId)
        {
            var client = excute.Context.ES_Client.FirstOrDefault(m => m.sOpenId == sOpenId);
            if (client != null)
            {
                client.iIsSubscribe =1;
                return excute.SaveChange();
            }
            return 0;
        }

        /// <summary>
        /// 取消关注修改会员关注状态
        /// </summary>
        /// <param name="sOpenId"></param>
        /// <returns></returns>
        public override int UnSubscribeEditClient(string sOpenId)
        {
            var client = excute.Context.ES_Client.FirstOrDefault(m => m.sOpenId == sOpenId);
            if (client != null)
            {
                client.iIsSubscribe = 0;
                client.dUnSubscribeTime = DateTime.Now;
                return excute.SaveChange();
            }
            return 0;
        }
    }
}
