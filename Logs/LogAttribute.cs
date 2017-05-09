using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logs
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    /*
	 *这一行代码，把特性[AttributeUsage]附加到特性类的定义上。方括号的语法表明一个特性的构造器被调用。
	 *所以，特性类也可以拥有它们自己的特性，这看起来可能有点混淆，但是随着我给你展示可以用特性类来做些什么，你对它的认识，将会越来越清晰。
       [AttributeUsage]特性具有一个定位参数和两个命名参数。定位参数指定了特性类将被用于何种类型，定位参数的值是枚举AttributeTargets的组合。
	 * 在我的例子里，我仅仅把特性类应用在类和方法上，所以通过组合两个AttributeTargets的值的满足了我的要求。
      [AttributeUsage]特性的第一个命名参数是AllowMultiple，
	 * 该参数指定了是否可以在同一个类型上应用多次（你所定义的）特性类。默认值是false，即不允许应用多次。
	 * 但是，根据这个例子的实际情况，你将会在某一类型上不止一次的应用特性（DefectTrackAttribute），
	 * 所以应该使用[AttributeUsage]的命名参数AllowMultiple，并将其设置为true。
	 * 这是因为，一个特定的类和方法在其生命周期里会经历多次修订，所以你需要使用[DefectTrackAttribute]特性记录每一次变化。
      [AttributeUsage]特性的第二个命名参数是Inherited，它指定了派生类是否继承此特性。
	 * 我使用了此参数的默认的值false。因为我使用的是默认值，所以也就不需要指定该命名参数。为什么不需要继承呢？
	 * 我想获取源代码的修改信息是跟每一个具体的类和方法有关的。如果把Inherited 设为true，那么开发者将会混淆一个类的[DefectTrackAttribute]特性，
	 * 无法辨别[DefectTrackAttribute]特性是它自己的还是从父类继承的。
     上面的代码展示了特性类（DefectTrackAttribute）的定义。它继承于System.Attribute，事实上，所有的特性均直接或间接的继承于System.Attribute。
	 */
    public class LogAttribute : Attribute
    {

        /*
         * 操作的模块名字
         */
        public string sModuleName
        {
            get;
            set;
        }

        //操作了类型
        public Operate type
        {
            get;
            set;
        }

        /// <summary>
        /// 操作类型
        /// </summary>
        public LogAttribute(string sModuleName, Operate type)
        {
            this.sModuleName = sModuleName;
            this.type = type;
        }
    }

    public enum Operate
    {
        //添加
        添加 = 1,
        //更新
        编辑 = 2,
        //删除
        删除 = 3,
        //修改
        修改 = 4,
        //冻结/解冻
        冻结解冻 = 5

    }
}
