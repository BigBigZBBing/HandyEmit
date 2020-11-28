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
        private Dictionary<String, EntityProperty> EntityBody => new Dictionary<String, EntityProperty>();

        public List<String> Fields => EntityBody.Keys.ToList();

        internal FieldEntity(LocalBuilder stack, ILGenerator generator) : base(stack, generator)
        {
            Type type = typeof(T);
            foreach (PropertyInfo prop in type.GetProperties())
            {
                this.EntityBody.Add(prop.Name, new EntityProperty(prop));
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
                LocalBuilder item = generator.DeclareLocal(EntityBody[Name].type);
                base.PushIn();
                base.Emit(OpCodes.Callvirt, EntityBody[Name].get);
                base.Emit(OpCodes.Stloc_S, item);
                return item;
            }
            set
            {
                if (!ContanisKey(Name)) ManagerGX.GxException("Entity prop is null;");
                base.PushIn();
                base.Emit(OpCodes.Ldloc, value);
                base.Emit(OpCodes.Callvirt, EntityBody[Name].set);
            }
        }

        /// <summary>
        /// 排查是否存在属性名
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        private Boolean ContanisKey(String Name)
        {
            return EntityBody.ContainsKey(Name);
        }

        private struct EntityProperty
        {
            public EntityProperty(PropertyInfo property)
            {
                this.property = property;
            }

            public PropertyInfo property { get; set; }
            public Type type => property.PropertyType;
            public MethodInfo get => property.GetGetMethod();
            public MethodInfo set => property.GetSetMethod();
        }
    }
}
