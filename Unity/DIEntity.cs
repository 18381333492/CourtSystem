using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unity
{
    public class DIEntity
    {
        /// <summary>
        /// 注入配置对象如果你要用这个配置，就
        /// 表示对象的配置信息都是从web.config上
        /// 获取的，这里我没有用这种方式，这里我
        /// 直接用的是代码做映射，如果你要用，
        /// 配置文件在这个项目的Web.config里面
        /// </summary>
        private UnityConfigurationSection configuration = 
                        ConfigurationManager.GetSection(UnityConfigurationSection.SectionName) as UnityConfigurationSection;

        //注入容器
        private static UnityContainer container = new UnityContainer();


        /// <summary>
        /// 获取实例
        /// </summary>
        /// <returns></returns>
        public static DIEntity GetInstance()
        {
            return new DIEntity();
        }
        /// <summary>
        /// 初始化构造函数
        /// </summary>
        public DIEntity()
        {
            configuration.Configure(container);
        }

        /// <summary>
        /// 获取接口的实现对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetImpl<T>()
        {
            try
            {
                return container.Resolve<T>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
