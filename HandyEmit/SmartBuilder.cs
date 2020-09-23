using HandyEmit.SmartEmit;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace HandyEmit
{
    /// <summary>
    /// 模块快速构建方案
    /// </summary>
    public class SmartBuilder
    {
        private String dllName;
        private AssemblyName assmblyName;
        private AssemblyBuilder assemblyBuilder;
        private ModuleBuilder moduleBuilder;
        private TypeBuilder typeBuilder;
        private FieldBuilder fieldBuilder;
        private PropertyBuilder propertyBuilder;
        private MethodBuilder methodBuilder;
        private ILGenerator MainIL;
        private Type _dymaticType;
        private Object _instance;

        /// <summary>
        /// 内存实体
        /// </summary>
        public object Instance { get => _instance; set => _instance = value; }


        #region 快速构建模块

        public SmartBuilder(String dllName)
        {
            this.dllName = dllName;
        }

        /// <summary>
        /// 创建程序集
        /// </summary>
        /// <param name="dllname"></param>
        public SmartBuilder Assembly()
        {
            //创建程序集
            assmblyName = new AssemblyName(dllName);

            //程序集生成器
#if NET48
            assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(assmblyName, AssemblyBuilderAccess.RunAndSave);
#else
            assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assmblyName, AssemblyBuilderAccess.RunAndCollect);
#endif

            //动态创建模块
#if NET48
            moduleBuilder = assemblyBuilder.DefineDynamicModule(assmblyName.Name, $"{assmblyName.Name}.dll");
#else
            moduleBuilder = assemblyBuilder.DefineDynamicModule(assmblyName.Name);
#endif

            return this;
        }

        /// <summary>
        /// 创建类库
        /// </summary>
        /// <param name="ClassName"></param>
        public SmartBuilder Class(String ClassName, ClassType ClassType = ClassType.公共)
        {
            typeBuilder = moduleBuilder.DefineType(ClassName, (TypeAttributes)ClassType);
            return this;
        }

        /// <summary>
        /// 创建私有变量
        /// </summary>
        /// <param name="FieldName"></param>
        /// <param name="Type"></param>
        /// <param name="Attr"></param>
        /// <param name="ConstValue"></param>
        public void Field(String FieldName, Type Type, FieldAttributes Attr = FieldAttributes.Private, Object ConstValue = null)
        {
            fieldBuilder = typeBuilder.DefineField(FieldName, Type, Attr);
            if (ConstValue != null)
                fieldBuilder.SetConstant(ConstValue);
        }

        /// <summary>
        /// 创建属性
        /// </summary>
        /// <param name="PropertyName"></param>
        /// <param name="Type"></param>
        /// <param name="Attr"></param>
        public void Property(String PropertyName, Type Type, PropertyAttributes Attr = PropertyAttributes.None)
        {
            propertyBuilder = typeBuilder.DefineProperty(PropertyName, Attr, Type, null);
        }

        /// <summary>
        /// 创建方法
        /// </summary>
        /// <param name="MethodName"></param>
        /// <param name="builder"></param>
        /// <param name="RetType"></param>
        /// <param name="Attr"></param>
        /// <param name="ParamTypes"></param>
        public void Method(String MethodName, Action<FuncGenerator> builder, Type RetType = null, MethodAttributes Attr = MethodAttributes.Public, Type[] ParamTypes = null)
        {
            methodBuilder = typeBuilder.DefineMethod(MethodName, Attr, RetType, ParamTypes);

            builder?.Invoke(new FuncGenerator(methodBuilder.GetILGenerator()));
        }

        /// <summary>
        /// 快捷get
        /// </summary>
        /// <param name="Type"></param>
        public void _get(Type Type)
        {
            Method("get_Item", null, Type, MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig, Type.EmptyTypes);

            MainIL = methodBuilder.GetILGenerator();
            MainIL.Emit(OpCodes.Ldarg_0);
            MainIL.Emit(OpCodes.Ldfld, fieldBuilder);
            MainIL.Emit(OpCodes.Ret);
            propertyBuilder.SetGetMethod(methodBuilder);
        }

        /// <summary>
        /// 快捷set
        /// </summary>
        /// <param name="Type"></param>
        public void _set(Type Type)
        {
            Method("set_Item", null, null, MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig, new Type[] { Type });

            MainIL = methodBuilder.GetILGenerator();
            MainIL.Emit(OpCodes.Ldarg_0);
            MainIL.Emit(OpCodes.Ldarg_1);
            MainIL.Emit(OpCodes.Stfld, fieldBuilder);
            MainIL.Emit(OpCodes.Ret);
            propertyBuilder.SetSetMethod(methodBuilder);
        }

        /// <summary>
        /// 保存类型
        /// </summary>
        /// <returns></returns>
        public void SaveClass()
        {
            _dymaticType = typeBuilder.CreateTypeInfo();
        }

        /// <summary>
        /// 保存类库
        /// </summary>
#if NET48
        public void Save()
        {
            assemblyBuilder.Save($"{assmblyName.Name}.dll");
        }
#endif

        /// <summary>
        /// 快速创建实例
        /// </summary>
        private void Build()
        {
            _instance = Activator.CreateInstance(_dymaticType);
        }

        /// <summary>
        /// 快速构建字段
        /// </summary>
        /// <param name="FieldName"></param>
        /// <param name="FieldType"></param>
        public void SmartProperty(String FieldName, Type FieldType)
        {
            Field($"_{FieldName}", FieldType);
            Property(FieldName, FieldType);
            _get(FieldType);
            _set(FieldType);
        }

        #endregion

        #region 特定构建扩展

        /// <summary>
        /// 生成特定对象
        /// </summary>
        /// <returns></returns>
        public EmitDynamic InitEntity()
        {
            var props = _dymaticType.GetProperties();
            Build();
            return new EmitDynamic()
            {
                Properties = new ConcurrentDictionary<String, EmitProperty>(ManagerGX.GetProps(props, Instance)),
                Instance = Instance
            };
        }

        /// <summary>
        /// 创建内存动态函数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="MethodName"></param>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static T DynamicMethod<T>(String MethodName, Action<FuncGenerator> builder) where T : class
        {
            var type = typeof(T);
            if (type.Name.IndexOf("Func`") == -1) throw new Exception("please use Func or Action");
            var types = type.GenericTypeArguments.ToList();
            var retType = types.Last();
            types.RemoveAt(types.Count - 1);
            DynamicMethod dynamicBuilder = new DynamicMethod(MethodName, retType, types.ToArray());
            builder?.Invoke(new FuncGenerator(dynamicBuilder.GetILGenerator()));
            return dynamicBuilder.CreateDelegate(typeof(T)) as T;
        }

        #endregion

    }
}
