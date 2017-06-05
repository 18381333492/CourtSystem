using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;

namespace WeiXin.Tool
{
    /// <summary>
    /// 获取微信access_token帮助类
    /// </summary>
    public class access_token
    {
      
        public string appid;

        public string secret;

        public string Access_Token;//接受的access_token

        /// <summary>
        /// 初始化构造函数
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="secret"></param>
        public access_token(string appid,string secret)
        {
            this.appid = appid;
            this.secret = secret;
        }


        /// <summary>
        /// 获取
        /// </summary>
        /// <returns></returns>
        public string Get()
        {
            if(File.Exists(AppDomain.CurrentDomain.BaseDirectory + "access_token.txt"))
            {//文件存在
                StreamReader sr = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "access_token.txt",Encoding.Default);
                List<string> line=new List<string>();
                string res;
                while ((res=sr.ReadLine()) != null)
                {
                    line.Add(res);
                }
                sr.Close();//关闭流
                if (DateTime.Now > DateTime.Parse(line[1]))
                {//access_token已过期
                    return Get_access_token();
                }
                else
                    return line[0];
            }
            else
            {//不存在
               return Get_access_token();
            }
        }

        /// <summary>
        /// 获取access_token
        /// </summary>
        /// <returns></returns>
        public string Get_access_token()
        {
            string sUrl = string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", appid, secret);
            string result = HttpHelper.HttpGet(sUrl);
            JObject res = JObject.Parse(result);
            if (res["access_token"] != null)
            {//返回成功
                FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "access_token.txt", FileMode.Create);//如果文件存在,直接覆盖
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(res["access_token"].ToString());
                int expires_in = int.Parse(res["expires_in"].ToString());
                sw.WriteLine(DateTime.Now.AddSeconds(expires_in-200).ToString());
                //清空缓冲区
                sw.Flush();
                //关闭流
                sw.Close();
                fs.Close();
                return res["access_token"].ToString();
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
