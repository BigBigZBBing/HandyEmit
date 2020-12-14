using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Xml.Linq;

namespace ILWheatBread
{
    public sealed class FastDynamic : IEnumerable
    {
        public FastDynamic()
        {
        }

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

        public ConcurrentDictionary<String, FastProperty> Properties { get; internal set; }

        internal Object Instance { get; set; }

        public String ToJson()
        {
            return JsonConvert.SerializeObject(this.Instance);
        }

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

        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (var item in Properties)
            {
                yield return item;
            }
        }
    }
}
