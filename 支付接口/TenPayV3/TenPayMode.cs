using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;


namespace TenPayV3
{
    /// <summary>
    /// 微信支付模式
    /// </summary>
    public class TenPayMode
    {

        //#region 扫码支付模式一相关

        ///// <summary>
        ///// 获取扫码支付模式一的支付二维码
        ///// </summary>
        ///// <param name="sOrderNo">订单ID</param>
        ///// <returns></returns>
        //public byte[] GetPayCode_First(string sOrderId)
        //{
        //    string appid = Ten_PayConfig.appid;
        //    string mch_id = Ten_PayConfig.mch_id;
        //    string nonce_str = Ten_PayConfig.nonce_str();
        //    string time_stamp = Ten_PayConfig.time_stamp();

        //    var Parameters = new Dictionary<string, string>();
        //    Parameters.Add("appid", Ten_PayConfig.appid);
        //    Parameters.Add("mch_id", Ten_PayConfig.mch_id);
        //    Parameters.Add("time_stamp", time_stamp);
        //    Parameters.Add("nonce_str", nonce_str);
        //    Parameters.Add("product_id", sOrderId);//商户定义的商品id或者订单号
        //    string sign = TenPaySign.CreateSign(Parameters);

        //    string sCodeLink = @"weixin://wxpay/bizpayurl?appid=" + appid + "&mch_id=" + mch_id + "&nonce_str=" + nonce_str + "&product_id=" + sOrderId +
        //       "&time_stamp=" + time_stamp + "&sign=" + sign;

        //    return Ten_PayHelp.MakeCode(sCodeLink);
        //}

        ///// <summary>
        ///// 扫码回调的函数
        ///// </summary>
        //public bool ScanCallBack(HttpRequestBase request, out string product_id)
        //{
        //    bool bState = false;
        //    byte[] bNetStream = request.BinaryRead(request.ContentLength);//获取微信回调的请求数据的字节流
        //    string sParam = System.Text.Encoding.UTF8.GetString(bNetStream);
        //    var Parameters = Ten_PayHelp.GetDictionaryFromXml(sParam);
        //    product_id = Parameters["product_id"];
        //    if (Parameters != null)
        //    {
        //        if (TenPaySign.CheckSign(Parameters))
        //        {//签名验证成功
        //            bState = true;
        //        }
        //    }
        //    return bState;
        //}

        ///// <summary>
        ///// 扫码支付模式一的统一下单支付接口
        ///// </summary>
        ///// <param name="body">描述</param>
        ///// <param name="out_trade_no">商户系统内部的订单编号</param>
        ///// <param name="total_fee">订单总金额，单位为分</param>
        ///// <param name="spbill_create_ip">APP和网页支付提交用户端ip，Native支付填调用微信支付API的机器IP.</param>
        ///// <param name="notify_url">接收微信支付异步通知回调地址,不能携带参数</param>
        ///// <param name="product_id">订单ID</param>
        ///// <returns></returns>
        //public string UniteOrder_First(string body, string out_trade_no, string total_fee, string spbill_create_ip, string notify_url, string product_id)
        //{
        //    /************************* 调用微信统一支付API所必需的参数******************************************/
        //    //公众账号ID      appid                  微信分配的公众账号ID（企业号corpid即为此appId）
        //    //商户号          mch_id                 微信支付分配的商户号
        //    //随机字符串      nonce_str              随机字符串，不长于32位。推荐随机数生成算法
        //    //签名            sign                   签名，详见签名生成算法
        //    //商品描述        body                   商品或支付单简要描述
        //    //商户订单号      out_trade_no           商户系统内部的订单号,32个字符内、可包含字母, 其他说明见商户订单号
        //    //总金额          total_fee              订单总金额，单位为分，详见支付金额
        //    //终端IP          spbill_create_ip       APP和网页支付提交用户端ip，Native支付填调用微信支付API的机器IP。
        //    //通知地址        notify_url             接收微信支付异步通知回调地址，通知url必须为直接可访问的url，不能携带参数。
        //    //交易类型        trade_type             取值如下：JSAPI，NATIVE，APP，详细说明见参数规定
        //    //商品ID          product_id             trade_type=NATIVE，此参数必传。此id为二维码中包含的商户定义的商品id或者订单号，商户自行定义。
        //    /************************* 调用微信统一支付API所必需的参数******************************************/
        //    var Dic = new Dictionary<string, string>();
        //    Dic.Add("appid", Ten_PayConfig.appid);
        //    Dic.Add("mch_id", Ten_PayConfig.mch_id);
        //    Dic.Add("nonce_str", Ten_PayConfig.nonce_str());
        //    Dic.Add("body", body);
        //    Dic.Add("out_trade_no", out_trade_no);
        //    Dic.Add("total_fee", total_fee);
        //    Dic.Add("spbill_create_ip", spbill_create_ip);
        //    Dic.Add("notify_url", notify_url);
        //    Dic.Add("trade_type", "NATIVE");
        //    Dic.Add("product_id", product_id);
        //    string sign = TenPaySign.CreateSign(Dic);//创建签名
        //    Dic.Add("sign", sign);

        //    string RequestData = Ten_PayHelp.InstallXml(Dic);

        //    //请求统一下单支付API
        //    string sUrl = "https://api.mch.weixin.qq.com/pay/unifiedorder";
        //    string sResult = Ten_PayHelp.HttpPost(sUrl, RequestData);//调用接口

        //    Message Msg = new Message();
        //    var Parameters = Ten_PayHelp.GetDictionaryFromCDATAXml(sResult);

        //    if (TenPaySign.CheckSign(Parameters))
        //    {//验证签名
        //        if (Parameters["return_code"] == "SUCCESS")
        //        {
        //            if (Parameters["result_code"] == "SUCCESS")
        //            {//统一下单成功
        //                Msg.state = true;
        //                Msg.text = Parameters["prepay_id"];
        //            }
        //            else
        //            {//统一下单失败
        //                Msg.error = Parameters["err_code_des"];//错误信息描述
        //            }
        //        }
        //        else
        //        {
        //            Msg.error = Parameters["return_msg"];
        //        }
        //    }
        //    else
        //    {
        //        Msg.error = "签名验证失败";
        //    }
        //    return ReturnPrepay_id(Msg);
        //}


        ///// <summary>
        ///// 返回微信预支付回话标识
        ///// </summary>
        ///// <param name="message"></param>
        ///// <returns></returns>
        //private string ReturnPrepay_id(Message message)
        //{
        //    var ReturnDictionary = new Dictionary<string, string>();
        //    ReturnDictionary.Add("appid", Ten_PayConfig.appid);
        //    ReturnDictionary.Add("mch_id", Ten_PayConfig.mch_id);
        //    ReturnDictionary.Add("nonce_str", Ten_PayConfig.nonce_str());
        //    ReturnDictionary.Add("prepay_id", message.text);
        //    if (message.state)
        //    {//成功
        //        ReturnDictionary.Add("return_code", "SUCCESS");
        //        ReturnDictionary.Add("result_code", "SUCCESS");
        //    }
        //    else
        //    {//失败
        //        ReturnDictionary.Add("return_code", "FAIL");
        //        ReturnDictionary.Add("return_msg", message.error);
        //        ReturnDictionary.Add("err_code_des", message.error);
        //        ReturnDictionary.Add("return_msg", message.error);
        //    }
        //    string sign = TenPaySign.CreateSign(ReturnDictionary);
        //    ReturnDictionary.Add("sign", sign);
        //    return Ten_PayHelp.InstallCDATAXml(ReturnDictionary);
        //}

        //#endregion

   
        //#region 扫码支付模式二相关

        ///// <summary>
        ///// 获取扫码支付模式二的支付二维码
        ///// </summary>
        ///// <param name="sCodeLink"></param>
        ///// <returns></returns>
        //public byte[] GetPayCode_Second(string sCodeLink)
        //{
        //    return Ten_PayHelp.MakeCode(sCodeLink);
        //}


        ///// <summary>
        ///// 扫码模式二的统一下单支付接口
        ///// </summary>
        ///// <param name="body">描述</param>
        ///// <param name="out_trade_no">商户系统内部的订单编号</param>
        ///// <param name="total_fee">订单总金额，单位为分</param>
        ///// <param name="spbill_create_ip">APP和网页支付提交用户端ip，Native支付填调用微信支付API的机器IP.</param>
        ///// <param name="notify_url">接收微信支付异步通知回调地址,不能携带参数</param>
        ///// <param name="product_id">订单ID</param>
        ///// <returns></returns>
        //public string UniteOrder_Second(string body, string out_trade_no, string total_fee, string spbill_create_ip, string notify_url, string product_id)
        //{
        //    var Dic = new Dictionary<string, string>();
        //    Dic.Add("appid", Ten_PayConfig.appid);
        //    Dic.Add("mch_id", Ten_PayConfig.mch_id);
        //    Dic.Add("nonce_str", Ten_PayConfig.nonce_str());
        //    Dic.Add("body", body);
        //    Dic.Add("out_trade_no", out_trade_no);
        //    Dic.Add("total_fee", total_fee);
        //    Dic.Add("spbill_create_ip", spbill_create_ip);
        //    Dic.Add("notify_url", notify_url);
        //    Dic.Add("trade_type", "NATIVE");
        //    Dic.Add("product_id", product_id);
        //    string sign = TenPaySign.CreateSign(Dic);//创建签名
        //    Dic.Add("sign", sign);

        //    string RequestData = Ten_PayHelp.InstallXml(Dic);

        //    string sReturnResult = string.Empty;
        //    //请求统一下单支付API
        //    string sUrl = "https://api.mch.weixin.qq.com/pay/unifiedorder";
        //    string sResult = Ten_PayHelp.HttpPost(sUrl, RequestData);//调用接口

        //    var Parameters = Ten_PayHelp.GetDictionaryFromCDATAXml(sResult);

        //    if (TenPaySign.CheckSign(Parameters))
        //    {//验证签名
        //        if (Parameters["return_code"] == "SUCCESS")
        //        {
        //            if (Parameters["result_code"] == "SUCCESS")
        //            {//统一下单成功
        //                sReturnResult = Parameters["code_url"];
        //            }
        //        }
        //    }
        //    return sReturnResult;
        //}

        //#endregion


        //#region 微信JS-sdk支付

        ///// <summary>
        ///// 获取微信用户的openid
        ///// </summary>
        ///// <returns></returns>
        //public string GetOpendId(HttpRequestBase Request)
        //{
        //    string code = Request.QueryString["code"];
        //    if (string.IsNullOrEmpty(code))
        //    {//获取code
        //        string code_url = string.Format(@"https://open.weixin.qq.com/connect/oauth2/authorize?
        //                     appid={0}&redirect_uri={1}&response_type=code&scope=snsapi_base&state=lk#wechat_redirect",
        //            Ten_PayConfig.appid, Ten_PayHelp.UrlEncode(Request.Url.AbsoluteUri, "UTF-8"));
        //        return code_url;
        //    }
        //    else
        //    {//通过code换取网页授权access_token
        //        string url = string.Format(@"https://api.weixin.qq.com/sns/oauth2/access_token?
        //                               appid={0}&secret={1}&code={2}&grant_type=authorization_code",
        //                               Ten_PayConfig.appid, Ten_PayConfig.appsecret, code);
        //        string returnStr= Ten_PayHelp.HttpPost(url,string.Empty);
        //        JObject job = C_Json.ParseJson(returnStr);
        //        if (job != null&&job["openid"]!=null)
        //        {//获取openid
        //            return job["openid"].ToString();
        //        }
        //    }
        //    return null;
        //}

        ///// <summary>
        ///// 获取微信Js支付的配置信息
        ///// </summary>
        ///// <param name="product_id"></param>
        ///// <returns></returns>
        //public JS_PayConfig GetJsPayConfig(string product_id)
        //{
        //    JS_PayConfig config =new JS_PayConfig();
        //    config.appId = Ten_PayConfig.appid;
        //    config.nonceStr = Ten_PayConfig.nonce_str();
        //    config.timeStamp = Ten_PayConfig.time_stamp();
        //    config.package = string.Format("product_id={0}", product_id);

        //    var Params = new Dictionary<string, string>();
        //    Params.Add("appId", config.appId);
        //    Params.Add("nonceStr", config.nonceStr);
        //    Params.Add("timeStamp", config.timeStamp);
        //    Params.Add("package", config.package);
        //    Params.Add("signType", "MD5");
        //    string paySign = TenPaySign.CreateSign(Params);
        //    config.paySign = paySign;
        //    return config;
        //}

        ///// <summary>
        ///// 微信公众号JS_SDK的统一下单支付
        ///// </summary>
        ///// <param name="body">订单描述</param>
        ///// <param name="out_trade_no">订单编号</param>
        ///// <param name="total_fee">订单价格</param>
        ///// <param name="spbill_create_ip">终端IP(APP和网页支付提交用户端ip，Native支付填调用微信支付API的机器IP)</param>
        ///// <param name="notify_url">异步回调通知地址</param>
        ///// <param name="product_id">订单ID</param>
        ///// <param name="openid">微信用户对于公众号的唯一标识</param>
        ///// <returns></returns>
        //public string UniteOrder_JS_SDK(string body, string out_trade_no, string total_fee, string spbill_create_ip, string notify_url, string product_id,string openid)
        //{
        //    var Parameters = new Dictionary<string, string>();
        //    Parameters.Add("appid", Ten_PayConfig.appid);
        //    Parameters.Add("mch_id", Ten_PayConfig.mch_id);
        //    Parameters.Add("nonce_str", Ten_PayConfig.nonce_str());
        //    Parameters.Add("body", body);
        //    Parameters.Add("out_trade_no", out_trade_no);
        //    Parameters.Add("total_fee", total_fee);
        //    Parameters.Add("spbill_create_ip", spbill_create_ip);
        //    Parameters.Add("notify_url", notify_url);
        //    Parameters.Add("trade_type", "JSAPI");
        //    Parameters.Add("product_id", product_id);
        //    Parameters.Add("openid", openid);
        //    string sign = TenPaySign.CreateSign(Parameters);
        //    Parameters.Add("sign", sign);
        //    //组装统一下单数据
        //    string RequestData = Ten_PayHelp.InstallXml(Parameters);
        //    string sReturnResult = string.Empty;
        //    //请求统一下单支付API
        //    string sUrl = "https://api.mch.weixin.qq.com/pay/unifiedorder";

        //    string sResult = Ten_PayHelp.HttpPost(sUrl, RequestData);//调用接口
        //    var NewParameters = Ten_PayHelp.GetDictionaryFromCDATAXml(sResult);

        //    if (TenPaySign.CheckSign(NewParameters))
        //    {//验证签名
        //        if (Parameters["return_code"] == "SUCCESS")
        //        {
        //            if (Parameters["result_code"] == "SUCCESS")
        //            {//统一下单成功
        //                sReturnResult = Parameters["prepay_id"];
        //            }
        //        }
        //    }
        //    return sReturnResult;
        //}
        //#endregion


        //#region 微信支付共用部分

        ///// <summary>
        ///// 微信异步通知支付结果.
        ///// </summary>
        ///// <param name="Request">Request请求</param>
        ///// <param name="sOrderNo">订单编号</param>
        ///// <param name="ResponeData">返回微信系统的数据</param>
        ///// <returns></returns>
        //public bool AsyncCallBack(HttpRequestBase Request, out string sOrderNo, out string ResponeData)
        //{
        //    bool PayState = false;
        //    sOrderNo = string.Empty;
        //    ResponeData = string.Empty;
        //    byte[] bNetStream = Request.BinaryRead(Request.ContentLength);//获取微信异步回调的请求数据的字节流
        //    string sParam = System.Text.Encoding.UTF8.GetString(bNetStream);
        //    var Parameters = Ten_PayHelp.GetDictionaryFromCDATAXml(sParam);
        //    if (TenPaySign.CheckSign(Parameters))
        //    {//验证签名
        //        if (Parameters["return_code"] == "SUCCESS")
        //        {
        //            if (Parameters["result_code"] == "SUCCESS")
        //            {//支付成功
        //                PayState = true;
        //                sOrderNo = Parameters["out_trade_no"];
        //                var ResponeDic = new Dictionary<string, string>();
        //                ResponeDic.Add("return_code", "SUCCESS");
        //                ResponeDic.Add("return_code", "SUCCESS");
        //                ResponeData = Ten_PayHelp.InstallCDATAXml(ResponeDic);
        //            }
        //        }
        //    }
        //    return PayState;
        //}
        //#endregion

    }
}
