using DapperHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sevices.Interface
{
    public class BaseBll
    {
        //查询接口
        protected IReading query = Unity.DIEntity.GetInstance().GetImpl<IReading>();
    }
}
