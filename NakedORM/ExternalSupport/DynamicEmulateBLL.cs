using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Core.Repository.ExternalSupport
{
    public class DynamicEmulateBLL
    {
        M_Base _m_base;
        public DynamicEmulateBLL(M_Base _mbase)
        {
            _m_base = _mbase;
        }
        public object ObjectEmulate(string nsclassName, object data)
        {
            Type _classType = _m_base.DymaticType.Where(a => a.TypeName == nsclassName).FirstOrDefault().DType;
            var obj = Activator.CreateInstance(_classType);

            foreach (PropertyInfo pi in _classType.GetProperties())
            {
                if (data.GetType().GetProperty(pi.Name) != null)
                {
                    Console.WriteLine("有相同的值");
                    pi.SetValue(obj, data.GetType().GetProperty(pi.Name).GetValue(data));
                }
            }
            return obj;
        }

        public object JsonEmulate(string nsclassName, string jsonstring)
        {
            Type _classType = _m_base.DymaticType.Where(a => a.TypeName == nsclassName).FirstOrDefault().DType;
            return Newtonsoft.Json.JsonConvert.DeserializeObject(jsonstring, _classType);
        }

    }
}