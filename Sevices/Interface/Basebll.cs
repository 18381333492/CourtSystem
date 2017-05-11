using DapperHelper;
using EFBaseHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sevices
{
    public class BaseBll
    {
        //查询接口
        protected IReading query = Unity.DIEntity.GetInstance().GetImpl<IReading>();

        //操作接口
        protected IWriting excute = Unity.DIEntity.GetInstance().GetImpl<IWriting>();
    }
}
