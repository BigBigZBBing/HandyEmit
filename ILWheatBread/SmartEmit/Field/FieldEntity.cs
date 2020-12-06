using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ILWheatBread.SmartEmit.Field
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
                Pop();
                Emit(OpCodes.Callvirt, EntityBody[Name].get);
                Emit(OpCodes.Stloc_S, item);
                return item;
            }
            set
            {
                if (!ContanisKey(Name)) ManagerGX.GxException("Entity prop is null;");
                Pop();
                Emit(OpCodes.Ldloc, value);
                Emit(OpCodes.Callvirt, EntityBody[Name].set);
            }
        }

        /// <summary>
        /// 获取属性值
        /// </summary>
        /// <param name="FieldName"></param>
        /// <returns></returns>
        public LocalBuilder GetValue(String FieldName)
        {
            if (!ContanisKey(FieldName)) ManagerGX.GxException("Entity property is null;");
            LocalBuilder item = generator.DeclareLocal(EntityBody[FieldName].type);
            Pop();
            Emit(OpCodes.Callvirt, EntityBody[FieldName].get);
            Emit(OpCodes.Stloc_S, item);
            return item;
        }

        /// <summary>
        /// 属性赋值
        /// </summary>
        /// <param name="FieldName"></param>
        /// <param name="value"></param>
        public void SetValue(String FieldName, LocalBuilder value)
        {
            if (!ContanisKey(FieldName)) ManagerGX.GxException("Entity property is null;");
            Pop();
            Emit(OpCodes.Ldloc, value);
            Emit(OpCodes.Callvirt, EntityBody[FieldName].set);
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
