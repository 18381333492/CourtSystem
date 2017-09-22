using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;
using System.Xml.Serialization;
using WeiXin.Base.Message;
using WeiXin.Base.Message.EventModels;
using WeiXin.Base.Message.ReceiveModels;
using WeiXin.Base.Message.SendModels;
using Unity;
using SystemInterface;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using EFModels;
using LogicHandlerInterface;
using WeiXin.Tool;
using System.Threading.Tasks;

namespace Web.App_Start.WeiXinMessage
{
    /// <summary>
    /// 需要处理那些消息就重写
    /// </summary>
    public class AutoHandleMessage : IBaseAction
    {
        private static object _lock = new object();
        
        /// <summary>
        /// 处理微信关键字回复
        /// </summary>
        /// <returns></returns>
        public override string HandleText(TextMessage message)
        {
            //匹配关键字
            var keyWord =DIEntity.Instance.GetImpl<IWeChatKeyWord>().GetByKeyWord(message.Content);
            if (keyWord != null)
            {
                if (keyWord.bIsOpen)
                {
                    if (keyWord.iRePlyType == 0)
                    {//回复问本消息
                        return MessageHelper.Text(keyWord.sContent, message);
                    }
                    else
                    {//回复图文消息
                                    //获取图文借口
                        var WeChatNewsData = DIEntity.Instance.GetImpl<IWeChatNews>().GetNews(keyWord.sWeChatNewsNameId.ToString()) as JObject;
                        List<CDELINK_WeChatNews> array = JsonConvert.DeserializeObject<List<CDELINK_WeChatNews>>(WeChatNewsData["newsList"].ToString());
                        //组装数据
                        List<item> Articles = new List<item>();
                        foreach (var m in array)
                        {
                            Articles.Add(new item()
                            {
                                Title = m.sTitle,
                                Description = m.sDescribe,
                                PicUrl = m.sPictureUrl,
                                Url = m.sDataUrl
                            });
                        }
                        return MessageHelper.News(Articles, message);
                    }
                }
                else
                    return base.HandleText(message);
            }
            else
            {
                return base.HandleText(message);
            }
        }

        /// <summary>
        /// 处理用户关注事件
        /// </summary>
        /// <returns></returns>
        public override string HandleSubscribe(SubscribeEvent message)
        {
            Task.Factory.StartNew(() =>
            {
                /*关注的时候注册用户*/
                var clientDomin = DIEntity.Instance.GetImpl<IClient>();//会员接口的实现
                if (!clientDomin.IsExistByOpenId(message.FromUserName))
                {//不存在注册
                    var weChat = DIEntity.Instance.GetImpl<IWeChat>().GetWeChat();
                    access_token token = new access_token(weChat.sAppId, weChat.sAppSecret);
                    //获取用户信息
                    var userInfo = UserInfoHelper.GetUserInfo(message.FromUserName, token.Get());
                    //注册会员
                    if (!string.IsNullOrEmpty(userInfo.openid))
                    {
                        clientDomin.AddClient(new ES_Client
                        {
                            sOpenId = userInfo.openid,
                            sNickName = userInfo.nickname,
                            iSex = userInfo.sex,
                            iIsSubscribe = userInfo.subscribe,
                            sHeadPicture = userInfo.headimgurl,
                            sCity = userInfo.city,
                            sProvince = userInfo.province,
                            sCountry = userInfo.country,
                            dSubscribeTime = DateTime.Now,
                            iState = 0,
                            iIntegral = 0,
                        });
                    }
                }
                else
                {//存在修改关注状态
                    clientDomin.SubscribeEditClient(message.FromUserName);
                }
            });

            //获取微信关注设置
            var WeChatConcern = DIEntity.Instance.GetImpl<IWeChatConcern>().Get();
            if (WeChatConcern.bIsConcernOn)
            {//关注回复功能开启
                if (WeChatConcern.iConcernType == 0)
                {//回复文本
                    string resStr = MessageHelper.Text(WeChatConcern.sContent, message);
                    return resStr;
                }
                else
                {//回复图文
                 //获取图文借口
                    var WeChatNewsData = DIEntity.Instance.GetImpl<IWeChatNews>().GetNews(WeChatConcern.sWeChatNewsNameId.ToString()) as JObject;
                    //数据组装
                    List<item> Articles = new List<item>();
                    List<CDELINK_WeChatNews> array = JsonConvert.DeserializeObject<List<CDELINK_WeChatNews>>(WeChatNewsData["newsList"].ToString());
                    foreach (var m in array)
                    {
                        Articles.Add(new item()
                        {
                            Title = m.sTitle,
                            Description = m.sDescribe,
                            PicUrl = m.sPictureUrl,
                            Url = m.sDataUrl
                        });
                    }
                    return MessageHelper.News(Articles, message);
                }
            }
            else
            {//关闭
                return base.HandleSubscribe(message);
            }
        }


        /// <summary>
        /// 处理用户取消关注事件
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public override string HandleUnSubscribe(UnSubscribeEvent message)
        {
            var clientDomin = DIEntity.Instance.GetImpl<IClient>();//会员接口的实现
            //取消关注处理事件
            clientDomin.UnSubscribeEditClient(message.FromUserName);

            return base.HandleUnSubscribe(message);
        }

    }
}