using EFModel;
using Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Sevices
{
    public partial class GoodsService : ServiceBase
    {

        /// <summary>
        /// 添加商品
        /// </summary>
        /// <param name="Item"></param>
        /// <returns></returns>
        public int Add(Goods item)
        {
            item.ID = Guid.NewGuid();
            item.sGoodNo =C_String.RandomCode(8);
            item.dInsertTime = DateTime.Now;
            excute.Add<Goods>(item);
            return excute.SaveChange();
        }


        /// <summary>
        /// 编辑商品
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public int Edit(Goods item)
        {
            excute.Edit<Goods>(item);
            return excute.SaveChange();
        }

        /// <summary>
        /// 上下架商品
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        public int UnderCarriage(List<Guid> Ids)
        {
            var query=excute.db.Goods.Where(m => Ids.Contains(m.ID));
            foreach(var m in query)
            {
                m.bShelves = m.bShelves == true ? false : true;
                excute.Edit<Goods>(m);
            }
            return  excute.SaveChange();
        }

    }
}
