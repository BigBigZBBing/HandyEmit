using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace HandyEmit.SmartEmit.Field
{
    public class FieldEntity<T> : FieldManager<T>
    {
        /// <summary>
        /// 实体结构
        /// </summary>
        private Dictionary<String, PropertyInfo> EntityStruct = new Dictionary<String, PropertyInfo>();

        internal FieldEntity(LocalBuilder stack, ILGenerator il) : base(stack, il)
        {
            Type type = typeof(T);
            foreach (PropertyInfo prop in type.GetProperties(BindingFlags.Public))
            {
                this.EntityStruct.Add(prop.Name, prop);
            }
        }

        /// <summary>
        /// 获取赋值
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public LocalBuilder this[String Name]
        {
            get
            {
                if (!ContanisKey(Name)) ManagerGX.GxException("Entity prop is null;");
                LocalBuilder item = il.DeclareLocal(EntityStruct[Name].PropertyType);
                base.Ldloc();
                base.Emit(OpCodes.Callvirt, EntityStruct[Name].GetGetMethod());
                base.Emit(OpCodes.Stloc_S, item);
                return item;
            }
            set
            {
                if (!ContanisKey(Name)) ManagerGX.GxException("Entity prop is null;");
                base.Ldloc();
                base.Emit(OpCodes.Ldloc, value);
                base.Emit(OpCodes.Callvirt, EntityStruct[Name].GetSetMethod());
            }
        }

        /// <summary>
        /// 排查是否存在属性名
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        private Boolean ContanisKey(String Name)
        {
            return EntityStruct.ContainsKey(Name);
        }
    }
}
