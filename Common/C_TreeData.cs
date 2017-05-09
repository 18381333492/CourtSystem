using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{

    public class C_TreeData
    {
        /// <summary>
        /// 序列化成comBotree或者trees的json数据
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string ComBoxTreeData(List<Tree> list)
        {
            if (list.Count > 0)
            {
                List<Tree> MainList = (from m in list where (m.iToid ==string.Empty) select m).ToList();
                List<Tree> ChildList = (from m in list where (m.iToid != string.Empty) select m).ToList();
                JArray result = new JArray();
                foreach (var o in MainList)
                {
                    JObject Main = new JObject();
                    JArray ChildArray = new JArray();//二级树形数组
                                                     //调用递归
                    ChildArray = Recursion(o, ChildList);
                    Main.Add(new JProperty("id", o.id.ToString()));
                    Main.Add(new JProperty("text", o.text));
                    Main.Add(new JProperty("children", ChildArray));
                    result.Add(Main);
                }
                return result.ToString();
            }
            else
            {
                return new JArray().ToString();
            }
        }

        /// <summary>
        /// 递归调用组织数据
        /// </summary>
        /// <param name="data"></param>
        private static JArray Recursion(Tree data, List<Tree> ChildList)
        {
            JArray TT = new JArray();
            JArray temp = new JArray();
            foreach (var o in ChildList)
            {
                JObject T = new JObject();
                if (data.id.ToString().ToLower() == o.iToid.ToLower())
                {
                    temp = Recursion(o, ChildList);
                    T.Add(new JProperty("id", o.id.ToString()));
                    T.Add(new JProperty("text", o.text));
                    T.Add(new JProperty("children", temp));
                    TT.Add(T);
                }
            }
            return TT;
        }
    }

    /// <summary>
    /// comBoTree的数据传入的数据结构
    /// </summary>
    public class Tree
    {
        public Guid id
        {
            get;
            set;
        }
        public string text
        {
            get;
            set;
        }
        public string iToid
        {
            get;
            set;
        }
    }
}
