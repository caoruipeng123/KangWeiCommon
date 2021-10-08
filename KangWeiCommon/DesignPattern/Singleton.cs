using System;

namespace KangWeiCommon.DesignPattern
{
    /// <summary>
    /// 单例模式实现
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Singleton<T> where T : class
    {
        /// <summary>
        /// 当前类的唯一实例
        /// </summary>
        private static T _instance;

        /// <summary>
        /// 全局锁 
        /// </summary>
        private static readonly object syslock = new object();

        /// <summary>
        /// 无参构造函数
        /// </summary>
        protected Singleton()
        {
        }

        /// <summary>
        /// 当前类的唯一实例
        /// </summary>
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (syslock)
                    {
                        if (_instance == null)
                        {
                            _instance = Activator.CreateInstance<T>();
                        }
                    }
                }
                return _instance;
            }
        }
    }
}
