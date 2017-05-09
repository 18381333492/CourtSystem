using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Common
{
    /// <summary>
    /// C# 对Xml文件的读写操作帮助类
    /// </summary>
    public class C_XmlHelp
    {
        /* 1.使用XmlDocument.
        *使用XmlDocument是一种基于文档结构模型的方式来读取XML文件.
        *在XML文件中,我们可以把XML看作是
            由文档声明(Declare),
            元素(Element),
            属性(Attribute),
            文本(Text)等构成的一个树.
        最开始的一个结点叫作根结点,每个结点都可以有自己的子结点.得到一个结点后,可以通过一系列属性或方法得到这个结点的值或其它的一些属性.
        例如:
             xn 代表一个结点
             xn.Name;//这个结点的名称 
             xn.Value;//这个结点的值   
             xn.ChildNodes;//这个结点的所有子结点   
             xn.ParentNode;//这个结点的父结点
       */
        //1. 读取所有的数据.使用的时候,首先声明一个XmlDocument对象,然后调用Load方法,从指定的路径加载XML文件.

        //2.使用XmlTextReader以流的形式读取Xml文件(只能读)

       
        //支付的配置文件路径
        public static string sPayXmlPath=AppDomain.CurrentDomain.BaseDirectory+ "App_Data//PayConfig//PayConfig.xml";
        

        /// <summary>
        /// 根据节点名称读取节点的值
        /// </summary>
        /// <param name="sNodeName"></param>
        /// <returns></returns>
        public static string ReadXmlConfig(string sNodeName)
        {
            string Value = string.Empty;
            XmlTextReader reader = new XmlTextReader(sPayXmlPath);
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    if (reader.Name ==sNodeName)
                    {
                        Value =reader.ReadElementString().Trim();//获取节点下面的值
                        break;
                    }
                }
            }
            reader.Close();
            return Value;
        }

    }
}
