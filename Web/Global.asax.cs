using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Common;
using System.Threading;
using Unity;
using SystemInterface;
using Fleck;
using EFModels.MyModels;
using System.Net.NetworkInformation;
using Logs;

namespace Web
{
	// 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
	// 请访问 http://go.microsoft.com/?LinkId=9394801

	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();

			WebApiConfig.Register(GlobalConfiguration.Configuration);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);

            Application["bIsStartUp"] = true;
            //Timer timer = new Timer(new TimerCallback(WebSiteStartUp),
            //                         null,//一个包含回调方法要使用的信息的对象，或者为空引用
            //                         0,//调用 callback 之前延迟的时间量（以毫秒为单位）。(即第一次调用该委托的时间间隔)
            //                         //指定 Timeout.Infinite 以防止计时器开始计时。指定零 (0) 以立即启动计时器。
            //                        60*60*1000);//调用 callback 的时间间隔（以毫秒为单位）

            WebSocket();//启动socket服务
            LogsHelper.init(AppDomain.CurrentDomain.BaseDirectory+ "App_Data\\log4net.xml");
        }

        /// <summary>
        /// 截取应用程序的错误
        /// 一般处理的是404错误
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_Error(object sender,EventArgs e)
        {       
            //Exception ex = Server.GetLastError();
            //if(ex is HttpException)
            //{
            //    if((ex as HttpException).GetHttpCode() == (int)HttpStatusCode.NotFound)
            //    {
            //        ;
            //    }
            //}
        }

        /// <summary>
        /// 会话结束
        /// </summary>
        protected void Session_End()
        {
            
        }

        /// <summary>
        /// 会话开始
        /// </summary>
        protected void Session_Start()
        {
            
        }


        /// <summary>
        /// 启动Socket服务
        /// </summary>
        private void WebSocket()
        {
            int port = GetPort();
            var allSockets = new List<IWebSocketConnection>();
            var server = new WebSocketServer(string.Format("ws://127.0.0.1:{0}", port));
            server.Start(socket =>
            {
                socket.OnOpen = () =>
                {
                    allSockets.Add(socket);
                    socket.Send(new SocketMode()
                    {
                        sPriKey = socket.ConnectionInfo.Id.ToString(),
                        IsFirstConnect = true
                    }.toJson());
                };
                socket.OnClose = () =>
                {
                    allSockets.Remove(socket);
                };
                socket.OnMessage = message =>
                {
                    var revices =C_Json.Deserialize<SocketMode>(message);
                    if (!string.IsNullOrEmpty(revices.sSendPriKey))
                    {
                        var sendSocket = allSockets.Where(m => m.ConnectionInfo.Id.ToString()==revices.sSendPriKey).SingleOrDefault();
                        if (sendSocket != null)
                        {
                            sendSocket.Send(new SocketMode()
                            {
                                sPriKey = sendSocket.ConnectionInfo.Id.ToString(),
                                data = revices.data,
                            }.toJson());
                        }
                    }
                };
            });
        }


        /// <summary>
        ///  获取可用的端口号
        /// </summary>
        /// <returns></returns>
        private int GetPort()
        {
            List<int> allUsedPorts = GetPortUsed();
            int port =0;
            do
            {
                port = new Random().Next(5000, 65535);//随机获取一个端口号
            }
            while (allUsedPorts.Contains(port));
            Application["WebScoket_Port"] = port;
            return port;
        }


        /// <summary>
        /// 获取操作系统已用的端口号
        /// </summary>
        /// <returns></returns>
        private List<int> GetPortUsed()
        {
            //获取本地计算机的网络连接和通信统计数据的信息
            IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
            //返回本地计算机上的所有Tcp监听程序
            IPEndPoint[] ipsTCP = ipGlobalProperties.GetActiveTcpListeners();
            //返回本地计算机上的所有UDP监听程序
            IPEndPoint[] ipsUDP = ipGlobalProperties.GetActiveUdpListeners();
            //返回本地计算机上的Internet协议版本4(IPV4 传输控制协议(TCP)连接的信息。
            TcpConnectionInformation[] tcpConnInfoArray = ipGlobalProperties.GetActiveTcpConnections();
            List<int> allPorts = new List<int>();
            foreach (IPEndPoint ep in ipsTCP) allPorts.Add(ep.Port);
            foreach (IPEndPoint ep in ipsUDP) allPorts.Add(ep.Port);
            foreach (TcpConnectionInformation conn in tcpConnInfoArray) allPorts.Add(conn.LocalEndPoint.Port);
            return allPorts;
        }

       
    }

}