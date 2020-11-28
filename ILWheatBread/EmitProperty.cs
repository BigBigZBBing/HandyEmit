using System;
using System.Reflection;
using System.Reflection.Emit;

namespace ILWheatBread
{
    public class EmitProperty
    {
        /// <summary>
        /// 单一Set对象
        /// </summary>
        private PropertySetterEmit setter;

        /// <summary>
        /// 单一Get对象
        /// </summary>
        private PropertyGetterEmit getter;

        /// <summary>
        /// 属性名称
        /// </summary>
        public String PropertyName { get; private set; }

        /// <summary>
        /// 属性类型
        /// </summary>
        public Type PropertyType { get; private set; }

        /// <summary>
        /// 实例对象
        /// </summary>
        public Object Instance { get; private set; }

        /// <summary>
        /// Get函数
        /// </summary>
        public MethodInfo GetMethod { get; private set; }

        /// <summary>
        /// Set函数
        /// </summary>
        public MethodInfo SetMethod { get; private set; }


        public EmitProperty(PropertyInfo propertyInfo, Object Instance = null)
        {
            if (propertyInfo == null)
            {
                throw new ArgumentNullException("属性不能为空");
            }

            if (propertyInfo.CanWrite)
            {
                setter = new PropertySetterEmit(propertyInfo);
            }

            if (propertyInfo.CanRead)
            {
                getter = new PropertyGetterEmit(propertyInfo);
            }

            this.PropertyName = propertyInfo.Name;

            this.PropertyType = propertyInfo.PropertyType;

            this.GetMethod = propertyInfo.GetGetMethod();

            this.SetMethod = propertyInfo.GetSetMethod();

            this.Instance = Instance;
        }

        /// <summary>
        /// 属性赋值操作（Emit技术）
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="value"></param>
        public void Set(Object value)
        {
            if (Instance == null)
            {
                throw new ArgumentNullException("实例为空");
            }
            this.setter?.Invoke(Instance, value);
        }

        /// <summary>
        /// 属性取值操作(Emit技术)
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public Object Get()
        {
            if (Instance == null)
            {
                throw new ArgumentNullException("实例为空");
            }
            return this.getter?.Invoke(Instance);
        }

        /// <summary>
        /// 属性赋值操作（Emit技术）
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="value"></param>
        public void Set(Object instance, Object value)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("实例为空");
            }
            this.setter?.Invoke(instance, value);
        }

        /// <summary>
        /// 属性取值操作(Emit技术)
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public Object Get(Object instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("实例为空");
            }
            return this.getter?.Invoke(instance);
        }
    }

    /// <summary>
    /// Emit 动态构造 Get方法
    /// </summary>
    public class PropertyGetterEmit
    {

        private readonly Func<Object, Object> getter;

        public PropertyGetterEmit(PropertyInfo propertyInfo)
        {
            if (propertyInfo == null)
            {
                throw new ArgumentNullException("propertyInfo");
            }

            this.getter = CreateGetterEmit(propertyInfo);
        }

        public Object Invoke(Object instance)
        {
            return getter?.Invoke(instance);
        }

        private Func<Object, Object> CreateGetterEmit(PropertyInfo property)
        {
            if (property == null)
                throw new ArgumentNullException("property");

            //获取方法信息
            MethodInfo getMethod = property.GetGetMethod(true);

            //创建一个IL空间内的方法 [PropertyGetter]
            DynamicMethod dm = new DynamicMethod("PropertyGetter", typeof(Object), new Type[] { typeof(Object) }, property.DeclaringType, true);

            //创建IL生产者
            ILGenerator il = dm.GetILGenerator();

            //判断方法是否为静态
            if (!getMethod.IsStatic)
            {
                il.Emit(OpCodes.Ldarg_0);
                //非静态调用
                il.EmitCall(OpCodes.Callvirt, getMethod, null);
            }
            else
            {
                //静态调用
                il.EmitCall(OpCodes.Call, getMethod, null);
            }

            //如果是值类型 就转成Object类型进行装箱
            if (property.PropertyType.IsValueType)
                il.Emit(OpCodes.Box, property.PropertyType);

            il.Emit(OpCodes.Ret);

            return (Func<Object, Object>)dm.CreateDelegate(typeof(Func<Object, Object>));
        }
    }

    /// <summary>
    /// Emit动态构造Set方法
    /// </summary>
    public class PropertySetterEmit
    {

        private readonly Action<Object, Object> setFunc;

        public PropertySetterEmit(PropertyInfo propertyInfo)
        {
            if (propertyInfo == null)
            {
                throw new ArgumentNullException("propertyInfo");
            }

            this.setFunc = CreatePropertySetter(propertyInfo);
        }

        private Action<Object, Object> CreatePropertySetter(PropertyInfo property)
        {
            if (property == null)
                throw new ArgumentNullException("property");

            MethodInfo setMethod = property.GetSetMethod(true);

            DynamicMethod dm = new DynamicMethod("PropertySetter", null, new Type[] { typeof(Object), typeof(Object) }, property.DeclaringType, true);

            ILGenerator il = dm.GetILGenerator();

            if (!setMethod.IsStatic)
            {
                il.Emit(OpCodes.Ldarg_0);
            }

            il.Emit(OpCodes.Ldarg_1);

            EmitCastToReference(il, property.PropertyType);

            if (!setMethod.IsStatic && !property.DeclaringType.IsValueType)
            {
                il.EmitCall(OpCodes.Callvirt, setMethod, null);
            }
            else
            {
                il.EmitCall(OpCodes.Call, setMethod, null);
            }

            il.Emit(OpCodes.Ret);

            return (Action<Object, Object>)dm.CreateDelegate(typeof(Action<Object, Object>));
        }

        private static void EmitCastToReference(ILGenerator il, Type type)
        {
            if (type.IsValueType)
            {
                //如果已经转成Object类型(已装箱) 标记为未装箱
                il.Emit(OpCodes.Unbox_Any, type);
            }
            else
            {
                //类型转换
                il.Emit(OpCodes.Castclass, type);
            }
        }

        public void Invoke(Object instance, Object value)
        {
            this.setFunc?.Invoke(instance, value);
        }
    }
}
