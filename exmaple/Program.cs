using ILWheatBread.SmartEmit;
using ILWheatBread.SmartEmit.Field;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Emit;
using System.Text;
using System.Diagnostics;
using ILWheatBread.Enums;
using ILWheatBread;
using System.Numerics;
using System.Runtime.Intrinsics.X86;
using System.Runtime.Intrinsics;
using System.Runtime.CompilerServices;
using System.IO;
using ILWheatBread.Compress;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Configs;

namespace exmaple
{
    class Program
    {
        public class DbPager
        {
            /// <summary>
            /// 页数
            /// </summary>
            public Int32 PagerIndex { get; set; } = 1;

            /// <summary>
            /// 每页显示数
            /// </summary>
            public Int32 PagerSize { get; set; } = 15;

            /// <summary>
            /// 数据总数
            /// </summary>
            public Int64 TotalCount { get; set; }
        }

        public class Model
        {
            public long? Id { get; set; }
            public string Name { get; set; }
            public int? Old { get; set; }
            public DateTime? LoginTime { get; set; }
            public DateTime? LoginTime1 { get; set; }
            public DateTime? LoginTime2 { get; set; }
            public DateTime? LoginTime3 { get; set; }
            public DateTime? LoginTime4 { get; set; }
            public DateTime? LoginTime5 { get; set; }

        }

        //static IRepository _Repository { get; set; }

        static void Main(string[] args)
        {
            //SmartBuilder.DynamicMethod<Func<IDataReader, DbPager, Model>>(string.Empty, func =>
            //{
            //    FieldList<Model> retList = func.NewList<Model>();
            //    FieldObject dread = func.NewObject(func.EmitParamRef(0, typeof(IDataReader)));
            //    FieldEntity<DbPager> refpager = func.NewEntity<DbPager>(func.EmitParamRef(1, typeof(DbPager)));
            //    FieldObject drecord = dread.As<IDataRecord>();

            //    func.While(() =>
            //    {
            //        func.NewBoolean(dread.Invoke("Read").ReturnRef()).Output();
            //    }, () =>
            //    {
            //        func.IF(refpager.IsNull() == false, () =>
            //        {
            //            var pos = drecord.Invoke("GetOrdinal", func.NewString("TotalCount")).ReturnRef();
            //            func.IF(func.NewBoolean(drecord.Invoke("IsDBNull", pos).ReturnRef()) == false, () =>
            //            {
            //                var value = drecord.Invoke("GetInt64", pos).ReturnRef();
            //                refpager.SetValue("TotalCount", value);
            //            }).IFEnd();

            //        }).IFEnd();

            //        FieldEntity<Model> model = func.NewEntity<Model>();
            //        var methods = typeof(IDataRecord).GetMethods().ToList();
            //        foreach (var prop in typeof(Model).GetProperties())
            //        {
            //            string propTypeName;
            //            propTypeName = prop.PropertyType.Name;

            //            if (prop.PropertyType.Name == "Nullable`1" || prop.PropertyType.Name.StartsWith("Nullable"))
            //                propTypeName = prop.PropertyType.GenericTypeArguments?[0].Name;

            //            var method = methods.FirstOrDefault(x => x.Name == "Get" + (propTypeName.Equals("Single") ? "Float" : propTypeName));
            //            if (method != null)
            //            {
            //                var pos = drecord.Invoke("GetOrdinal", func.NewString(prop.Name)).ReturnRef();
            //                func.IF(func.NewBoolean(drecord.Invoke("IsDBNull", pos).ReturnRef()) == func.NewBoolean(false), () =>
            //                {
            //                    var value = drecord.Invoke(method.Name, pos).ReturnRef();
            //                    model.SetValue(prop.Name, value);
            //                }).IFEnd();
            //            }
            //        }
            //        retList.Add(model);
            //    });

            //    retList.Output();
            //    func.EmitReturn();
            //});

            //数值数组Copy性能测试
            BenchmarkRunner.Run<ByteCopyBenchmark>();
            //BenchmarkRunner.Run<UInt32CopyBenchmark>();
            //结果Int32比Byte更有效


            Console.ReadKey();
        }
    }
}
