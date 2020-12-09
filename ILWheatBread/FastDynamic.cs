using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Xml.Linq;

namespace ILWheatBread
{
    /// <summary>
    /// 内存高效模型
    /// </summary>
    public sealed class FastDynamic : IEnumerable
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FastDynamic() { }

        /// <summary>
        /// 指向
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public Object this[String Name]
        {
            get
            {
                return this.Properties[Name].Get();
            }
            set
            {
                this.Properties[Name].Set(value);
            }
        }

        /// <summary>
        /// 所有属性集合
        /// </summary>
        public ConcurrentDictionary<String, FastProperty> Properties { get; internal set; }

        /// <summary>
        /// 实例缓存
        /// </summary>
        internal Object Instance { get; set; }

        /// <summary>
        /// 序列化成Json
        /// </summary>
        /// <returns></returns>
        public String ToJson()
        {
            return JsonConvert.SerializeObject(this.Instance);
        }

        /// <summary>
        /// 序列化成Xml
        /// </summary>
        /// <returns></returns>
        public String ToXml()
        {
            XDocument doc = new XDocument();
            XElement classNode = new XElement(Instance.GetType().Name);
            foreach (var value in Properties.Values)
            {
                var prop = new XElement(value.PropertyName, value.Get());
                prop.Add(new XAttribute("Type", value.PropertyType));
                classNode.Add(prop);
            }
            doc.Add(classNode);
            return doc.ToString();
        }

        /// <summary>
        /// 迭代器
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (var item in Properties)
            {
                yield return item;
            }
        }
    }
}
