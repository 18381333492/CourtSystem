using EFModel;
using Sevices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Web.App_Start;
using Common;

namespace Web.Areas.Mobile.Controllers
{
    /// <summary>
    /// 支付相关
    /// </summary>
    public class PayController : MobileBaseController<OrdersService>
    {
        //
        // GET: /Mobile/Pay/

        public ActionResult SuccessTip()
        {
            return View();
        }


        //业务逻辑处理
        public void HandleOrder()
        {
            //交易金额
            string total_fee = Request.QueryString["total_fee"].ToString();
            //商户订单号
            string out_order_no = Request.QueryString["out_order_no"].ToString();
            //服务端校验码
            string sign = Request.QueryString["sign"].ToString();
            //云通付交易订单号
            string trade_no = Request.QueryString["trade_no"].ToString();
            //交易结果（TRADE_SUCCESS说明支付成功）
            string trade_status = Request.QueryString["trade_status"].ToString();
            //合作身份者PID，签约账号，由16位纯数字组成的字符串，请登录商户后台查看
            string partner = PayHelper.partner;
            // MD5密钥，安全检验码，由数字和字母组成的32位字符串，请登录商户后台查看
            string key = PayHelper.key;
            bool State = NotifyState(total_fee, out_order_no, sign, trade_no, trade_status, partner, key);
            if (State && trade_status == "TRADE_SUCCESS")
            {//支付成功处理业务逻辑
             //修改订单状态
                var order = new Orders();
                if (_server.AlterStateByOrderNo(out_order_no, out order) > 0)
                {//业务逻辑修改改成功
                    // SendMessage(order.sPhone, string.Format(C_Config.ReadAppSetting("MsgContent"), order.sGoodName));
                    //购买成功发送给用户短信
                }
            }      
        }


        /// <summary>
        /// 支付成功跳转链接
        /// </summary>
        public ActionResult Success()
        {
            if (Request.QueryString["trade_status"].ToString() == "TRADE_SUCCESS")
            {
                HandleOrder();
               return Redirect("/Mobile/Pay/SuccessTip");
            }
            else
               return Content("支付失败!");
        }

        /// <summary>
        /// 验证状态
        /// </summary>
        /// <param name="total_fee">交易金额</param>
        /// <param name="out_order_no">商户订单号</param>
        /// <param name="sign">服务端校验码</param>
        /// <param name="trade_no">云通付交易订单号</param>
        /// <param name="trade_status">交易结果（TRADE_SUCCESS说明支付成功）</param>
        /// <param name="partner">合作身份者PID，签约账号，由16位纯数字组成的字符串，请登录商户后台查看</param>
        /// <param name="key">MD5密钥，安全检验码，由数字和字母组成的32位字符串，请登录商户后台查看</param>
        /// <returns></returns>
        public bool NotifyState(string total_fee, string out_order_no, string sign, string trade_no, string trade_status, string partner, string key)
        {
            string val = string.Empty;
            val += out_order_no + total_fee + trade_status + partner + key;
            val =PayHelper.md5(val.ToString());
            if (sign == val)
                return true;
            else
                return false;

        }

        /// <summary>
        /// 发送手机验证码
        /// </summary>
        /// <param name="mobileNumber"></param>
        /// <param name="MsgContent"></param>
        /// <returns></returns>
        public bool SendMessage(string mobileNumber, string MsgContent)
        {
            bool bResult = false;
            string postStrTpl = "un={0}&pw={1}&phone={2}&msg={3}&rd=1";
            UTF8Encoding encoding = new UTF8Encoding();
            string sMsgAccount = C_Config.ReadAppSetting("account");
            string sMsgPassword = C_Config.ReadAppSetting("password");
            string postUrl = C_Config.ReadAppSetting("postUrl");
            byte[] postData = encoding.GetBytes(string.Format(postStrTpl, sMsgAccount, sMsgPassword, mobileNumber, MsgContent));
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(postUrl);
            myRequest.Method = "POST";
            myRequest.ContentType = "application/x-www-form-urlencoded";
            myRequest.ContentLength = postData.Length;
            Stream newStream = myRequest.GetRequestStream();

            newStream.Write(postData, 0, postData.Length);
            newStream.Flush();
            newStream.Close();

            HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
            StreamReader sr = new StreamReader(myResponse.GetResponseStream());
            string res=sr.ReadToEnd();
            if (myResponse.StatusCode == HttpStatusCode.OK)
            {
                bResult = true;
            }
            return bResult;
        }
    }
}
