using DapperHelper;
using EFBaseHelper.Writing;
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

        //操作接口
        protected IWriting excute = Unity.DIEntity.GetInstance().GetImpl<IWriting>();
    }
}
