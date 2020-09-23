using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository.ExternalSupport
{
    public abstract class EmitHelper
    {

        private static AssemblyName assmblyname;
        private static string DllName;
        private static AssemblyBuilder assemblybuilder;
        private static ModuleBuilder modulebuilder;
        private static TypeBuilder typebuilder;
        private static Type _dymaticType;

        public static Type DymaticType
        {
            get
            {
                return _dymaticType;
            }

            //set
            //{
            //    dymaticType = value;
            //}
        }

        public static void Create(string dllname)
        {
            DllName = dllname;
            assmblyname = new AssemblyName(DllName);
            ///2程序集生成器
            assemblybuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(assmblyname, AssemblyBuilderAccess.RunAndSave);

            // For a single-module assembly, the module name is usually
            // the assembly name plus an extension.
            ////3动态创建模块
            modulebuilder = assemblybuilder.DefineDynamicModule(assmblyname.Name);
        }
        public static void CreateClass(string NsClassName)
        {
            typebuilder = modulebuilder.DefineType(NsClassName, TypeAttributes.Public);
        }
        public static void CreateMember(string MemberName, Type memberType)
        {
            FieldBuilder fbNumber = typebuilder.DefineField(
              "m_" + MemberName,
             memberType,
              FieldAttributes.Private);


            PropertyBuilder pbNumber = typebuilder.DefineProperty(
                MemberName,
                System.Reflection.PropertyAttributes.HasDefault,
                memberType,
                null);


            MethodAttributes getSetAttr = MethodAttributes.Public |
                MethodAttributes.SpecialName | MethodAttributes.HideBySig;


            MethodBuilder mbNumberGetAccessor = typebuilder.DefineMethod(
                "get_" + MemberName,
                getSetAttr,
                memberType,
                Type.EmptyTypes);

            ILGenerator numberGetIL = mbNumberGetAccessor.GetILGenerator();

            numberGetIL.Emit(OpCodes.Ldarg_0);
            numberGetIL.Emit(OpCodes.Ldfld, fbNumber);
            numberGetIL.Emit(OpCodes.Ret);

            // Define the "set" accessor method for Number, which has no return
            // type and takes one argument of type int (Int32).
            MethodBuilder mbNumberSetAccessor = typebuilder.DefineMethod(
                "set_" + MemberName,
                getSetAttr,
                null,
                new Type[] { memberType });

            ILGenerator numberSetIL = mbNumberSetAccessor.GetILGenerator();
            // Load the instance and then the numeric argument, then store the
            // argument in the field.
            numberSetIL.Emit(OpCodes.Ldarg_0);
            numberSetIL.Emit(OpCodes.Ldarg_1);
            numberSetIL.Emit(OpCodes.Stfld, fbNumber);
            numberSetIL.Emit(OpCodes.Ret);

            // Last, map the "get" and "set" accessor methods to the
            // PropertyBuilder. The property is now complete.
            pbNumber.SetGetMethod(mbNumberGetAccessor);
            pbNumber.SetSetMethod(mbNumberSetAccessor);
            ///最重要的是你最后要创建类型

        }
        public static Type SaveClass()
        {
            _dymaticType = typebuilder.CreateTypeInfo().AsType();
            return DymaticType;
        }
        public static void Save()
        {
            assemblybuilder.Save(assmblyname.Name + ".dll");
        }
        ///// <summary>
        ///// 创建一个实体类并保存生成类型
        ///// </summary>
        ///// <param name="NsClassName"></param>
        ///// <param name="propertys"></param>
        //public void Execute(string NsClassName, Dictionary<string, Type> propertys)
        //{

        //    CreateClass(NsClassName);
        //    foreach (var item in propertys)
        //    {
        //        CreateMember(item.Key, item.Value);
        //    }
        //    SaveClass();
        //    Save();
        //}

        //public void Execute(List<M_DefineClass> _classes) {

        //    foreach (M_DefineClass _class in _classes)
        //    {
        //        CreateClass(_class.NsClassName);
        //        foreach (var prop in _class.Props)
        //        {
        //            CreateMember(prop.MemberName, prop.MemberType);
        //        }
        //        SaveClass();
        //    }

        //}

        //public void Test(string dllname, string NsClassName, string MemberName, Type memberType)
        //{
        //    //1设置程序集名称
        //    assmblyname = new AssemblyName(dllname);
        //    ///2程序集生成器
        //    assemblybuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(assmblyname, AssemblyBuilderAccess.RunAndSave);
        //    ////3动态创建模块
        //    modulebuilder = assemblybuilder.DefineDynamicModule(assmblyname.Name, assmblyname.Name + ".dll");
        //    ///4.创建类
        //    typebuilder = modulebuilder.DefineType(NsClassName, TypeAttributes.Public);
        //    ///5.创建私有字段
        //    FieldBuilder fbNumber = typebuilder.DefineField(
        //     "m_" + MemberName,
        //     memberType,
        //     FieldAttributes.Private);

        //    ///6.创建共有属性
        //    PropertyBuilder pbNumber = typebuilder.DefineProperty(
        //        MemberName,
        //        System.Reflection.PropertyAttributes.HasDefault,
        //        memberType,
        //        null);


        //    MethodAttributes getSetAttr = MethodAttributes.Public |
        //        MethodAttributes.SpecialName | MethodAttributes.HideBySig;


        //    MethodBuilder mbNumberGetAccessor = typebuilder.DefineMethod(
        //        "get_" + MemberName,
        //        getSetAttr,
        //        memberType,
        //        Type.EmptyTypes);

        //    ILGenerator numberGetIL = mbNumberGetAccessor.GetILGenerator();

        //    numberGetIL.Emit(OpCodes.Ldarg_0);
        //    numberGetIL.Emit(OpCodes.Ldfld, fbNumber);
        //    numberGetIL.Emit(OpCodes.Ret);

        //    // Define the "set" accessor method for Number, which has no return
        //    // type and takes one argument of type int (Int32).
        //    MethodBuilder mbNumberSetAccessor = typebuilder.DefineMethod(
        //        "set_" + MemberName,
        //        getSetAttr,
        //        null,
        //        new Type[] { typeof(int) });

        //    ILGenerator numberSetIL = mbNumberSetAccessor.GetILGenerator();
        //    // Load the instance and then the numeric argument, then store the
        //    // argument in the field.
        //    numberSetIL.Emit(OpCodes.Ldarg_0);
        //    numberSetIL.Emit(OpCodes.Ldarg_1);
        //    numberSetIL.Emit(OpCodes.Stfld, fbNumber);
        //    numberSetIL.Emit(OpCodes.Ret);

        //    // Last, map the "get" and "set" accessor methods to the
        //    // PropertyBuilder. The property is now complete.
        //    pbNumber.SetGetMethod(mbNumberGetAccessor);
        //    pbNumber.SetSetMethod(mbNumberSetAccessor);

        //    ///最重要的是你最后要创建类型
        //    Type t = typebuilder.CreateType();
        //    assemblybuilder.Save(assmblyname.Name + ".dll");

        //}


    }
}