using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace Core.Repository.ExternalSupport
{
    /// <summary>
    /// 使用Emit动态代理收集实体信息
    /// </summary>
    /// <typeparam name="T">实体类</typeparam>
    public class TDynamicBuilder<T>
    {
        #region 变量区域
        private static readonly MethodInfo getValueMethod = typeof(IDataRecord).GetMethod("get_Item", new[] { typeof(int) });

        private static readonly MethodInfo isDBNullMethod = typeof(IDataRecord).GetMethod("IsDBNull", new[] { typeof(int) });

        private delegate T Load(IDataRecord dataRecord);

        /// <summary>
        /// 代理
        /// </summary>
        private Load Handler;//最终执行动态方法的一个委托 参数是IDataRecord接口
        #endregion

        #region 构造函数
        private TDynamicBuilder() { }
        #endregion

        #region +Build Emit动态代理生成一个实体对象
        /// <summary>
        /// Emit动态代理生成一个实体对象
        /// </summary>
        /// <param name="dataRecord">DataReader接口实现对象</param>
        /// <returns></returns>
        public T Build(IDataRecord dataRecord)
        {
            return Handler(dataRecord);//执行CreateBuilder里创建的DynamicCreate动态方法的委托
        }
        #endregion

        #region + CreateBuilder 创建一个Builder器
        /// <summary>
        /// 创建一个Builder器
        /// </summary>
        /// <param name="dataRecord">DataReader接口实现对象</param>
        /// <returns></returns>
        public static TDynamicBuilder<T> CreateBuilder(IDataRecord dataRecord)
        {
            var dynamicBuilder = new TDynamicBuilder<T>();
            var type = typeof(T);
            //定义一个名为DynamicCreate的动态方法，返回值typof(T)，参数typeof(IDataRecord)
            var method = new DynamicMethod("DynamicCreate", type, new[] { typeof(IDataRecord) }, typeof(T), true);
            var generator = method.GetILGenerator();//创建一个MSIL生成器，为动态方法生成代码
            var result = generator.DeclareLocal(type);//声明指定类型的局部变量 可以T t;这么理解
                                                      //The next piece of code instantiates the requested type of object and stores it in the local variable. 可以t=new T();这么理解
            generator.Emit(OpCodes.Newobj, type.GetConstructor(Type.EmptyTypes));
            generator.Emit(OpCodes.Stloc, result);
            var propertys = type.GetProperties();
            //数据集合
            for (var i = 0; i < dataRecord.FieldCount; i++)
            {
                //查找属性
                var propertyInfo = propertys.FirstOrDefault(a => string.Equals(a.Name.ToLower(), dataRecord.GetName(i).ToLower()));
                if (propertyInfo == null) continue;
                if (!string.Equals(propertyInfo.Name, dataRecord.GetName(i), StringComparison.CurrentCultureIgnoreCase)) continue;
                //根据列名取属性  原则上属性和列是一一对应的关系
                var endIfLabel = generator.DefineLabel();

                /*The code then loops through the fields in the data reader, finding matching properties on the type passed in. 
                 * When a match is found, the code checks to see if the value from the data reader is null.
                 */
                generator.Emit(OpCodes.Ldarg_0);
                generator.Emit(OpCodes.Ldc_I4, i);
                //就知道这里要调用IsDBNull方法 如果IsDBNull==true contine
                generator.Emit(OpCodes.Callvirt, isDBNullMethod);
                generator.Emit(OpCodes.Brtrue, endIfLabel);

                /*If the value in the data reader is not null, the code sets the value on the object.*/
                generator.Emit(OpCodes.Ldloc, result);
                generator.Emit(OpCodes.Ldarg_0);
                generator.Emit(OpCodes.Ldc_I4, i);
                generator.Emit(OpCodes.Callvirt, getValueMethod); //调用get_Item方法
                generator.Emit(OpCodes.Unbox_Any, dataRecord.GetFieldType(i));
                generator.Emit(OpCodes.Callvirt, propertyInfo.GetSetMethod()); //给该属性设置对应值

                generator.MarkLabel(endIfLabel);

            }

            /*The last part of the code returns the value of the local variable*/
            generator.Emit(OpCodes.Ldloc, result);
            generator.Emit(OpCodes.Ret);//方法结束，返回

            //完成动态方法的创建，并且创建执行该动态方法的委托，赋值到全局变量handler,handler在Build方法里Invoke
            dynamicBuilder.Handler = (Load)method.CreateDelegate(typeof(Load));
            return dynamicBuilder;
        }
        #endregion

    }
}
