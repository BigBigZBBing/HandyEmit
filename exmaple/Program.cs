using ILWheatBread.SmartEmit;
using ILWheatBread.SmartEmit.Field;
using exmaple.Model;
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

namespace exmaple
{
    public static class test
    {
        public static string name { get; set; }
    }

    class Program
    {

        //static IRepository _Repository { get; set; }

        static void Main(string[] args)
        {
            var tt = typeof(string[]).BaseType;

            //EmitDynamicExmaple();

            //DbOperation();

            //ILpkCSharpDom();

            //ExistsILpkCSharpDom();

            //CopyILpkCSharpDom();

            Console.ReadKey();
        }

        /// <summary>
        /// 测试For 增量 5亿次性能测试
        /// </summary>
        private static void ILpkCSharpDom()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            int temp = 0;
            for (int i = 0; i < 500000000; i++)
            {
                temp += i;
            }
            stopwatch.Stop();
            Console.WriteLine($"5E次增量 C# 运算 {stopwatch.ElapsedMilliseconds}");

            Action dele = SmartBuilder.DynamicMethod<Action>("test", func =>
            {
                var temp = func.NewInt32();
                func.For(0, 500000000, build =>
                {
                    temp += (CanCompute<int>)build;
                });
                func.EmitReturn();
            });
            stopwatch.Restart();
            dele();
            stopwatch.Stop();
            Console.WriteLine($"5E次增量 IL 运算 {stopwatch.ElapsedMilliseconds}");
        }

        /// <summary>
        /// 创建1000长度数组 Exists函数性能测试
        /// </summary>
        private static void ExistsILpkCSharpDom()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            int[] temp = new int[10000];
            for (int i = 0; i < temp.Length; i++)
            {
                temp[i] = i;
            }
            for (int i = 0; i < temp.Length; i++)
            {
                Array.Exists(temp, e =>
                {
                    if (e == i) return true;
                    return false;
                });
            }
            stopwatch.Stop();
            Console.WriteLine($"创建10000长度数组 C# Exists处理 {stopwatch.ElapsedMilliseconds}");

            Action dele = SmartBuilder.DynamicMethod<Action>("test1", func =>
            {
                var temp = func.NewArray<int>(10000);
                func.For(0, temp.GetLength(), int1 =>
                {
                    temp.SetValue(int1, int1);
                });
                func.For(0, temp.GetLength(), build =>
                {
                    temp.Exists(build);
                });
                func.EmitReturn();
            });
            stopwatch.Restart();
            dele();
            stopwatch.Stop();
            Console.WriteLine($"创建10000长度数组 IL Exists处理 {stopwatch.ElapsedMilliseconds}");
        }

        /// <summary>
        /// 数组Copy函数性能测试
        /// </summary>
        private static void CopyILpkCSharpDom()
        {
            int index = 10000000;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            int[] temp = new int[index];
            int[] temp1 = new int[index];
            for (int i = 0; i < temp.Length; i++)
            {
                temp[i] = i;
            }
            Array.Copy(temp, 0, temp1, 0, temp.Length);
            stopwatch.Stop();
            Console.WriteLine($"创建100000000长度数组 C# Copy处理 {stopwatch.ElapsedMilliseconds}");

            Action dele = SmartBuilder.DynamicMethod<Action>("test2", func =>
            {
                var temp = func.NewArray<int>(index);
                var temp1 = func.NewArray<int>(index);
                func.For(0, temp.GetLength(), int1 =>
                {
                    temp.SetValue(int1, int1);
                });
                temp.Copy(temp1, temp1.GetLength());
                func.EmitReturn();
            });
            stopwatch.Restart();
            dele();
            stopwatch.Stop();
            Console.WriteLine($"创建100000000长度数组 IL Copy处理处理 {stopwatch.ElapsedMilliseconds}");
        }

        //private static void DbOperation()
        //{
        //    _Repository = new Repository("Data Source=localhost;Initial Catalog=dapperDb; User Id=sa;Password=111111");

        //    using (var con = _Repository.ConnectMSSQL())
        //    {
        //        using (var tran = con.BeginTranScoape())
        //        {
        //            ////主键查询
        //            //var list1 = con.GetKey<SysUser>(1);

        //            ////条件查询
        //            //var list = con.GetList<SysUser>(where =>
        //            //{
        //            //    where
        //            //    ._("Name", "戴志伟");
        //            //});

        //            try
        //            {
        //                //for (int i = 0; i < 10000; i++)
        //                //{
        //                //    //单量新增
        //                //    var res = con.Add(new TestModel()
        //                //    {
        //                //        test2 = new Random().Next(0, 10000),
        //                //        test3 = Create(100),
        //                //        test4 = Convert.ToDecimal(new Random().Next(0, 10000) + new Random().NextDouble()),
        //                //        test5 = DateTime.Now.AddSeconds(new Random().Next(-1000, 1000)),
        //                //        test6 = Convert.ToSingle(new Random().Next(0, 10000) + new Random().NextDouble())
        //                //    });
        //                //}

        //                //List<TestModel> model = new List<TestModel>();
        //                //for (int i = 0; i < 100000; i++)
        //                //{
        //                //    model.Add(new TestModel()
        //                //    {
        //                //        test2 = new Random().Next(0, 10000),
        //                //        test3 = Create(100),
        //                //        test4 = Convert.ToDecimal(new Random().Next(0, 10000) + new Random().NextDouble()),
        //                //        test5 = DateTime.Now.AddSeconds(new Random().Next(-1000, 1000)),
        //                //        test6 = Convert.ToSingle(new Random().Next(0, 10000) + new Random().NextDouble())
        //                //    });
        //                //}
        //                //var res = con.AddRange(model);

        //                //更新
        //                var res1 = con.Update<SysUser>(set =>
        //                    set._("Name", "邓超"),
        //                where =>
        //                    where._("Name", "邓超1")
        //                );

        //                tran.Commit();
        //            }
        //            catch (System.Exception ex)
        //            {
        //                tran.Rollback();
        //            }

        //        }


        //        //分页查询
        //        //DbPager pager = new DbPager(2, 2);
        //        //var list = con.GetListPager<SysUser>(pager);
        //        //Int32 count = Convert.ToInt32(pager.TotalCount);

        //        //个别字段查询
        //        //var list2 = con.GetList<SysUser>(Reveal: x => new { x.Name, x.Old });

        //        //批量新增
        //        //var res = con.AddRange(new List<SysUser>()
        //        //{
        //        //    new SysUser(){
        //        //        Name = "胡友洋",
        //        //        Old = 32
        //        //    },
        //        //    new SysUser(){
        //        //        Name = "邓超",
        //        //        Old = 23
        //        //    },
        //        //    new SysUser(){
        //        //        Name = "孙博强",
        //        //        Old = 26
        //        //    }
        //        //});


        //        //删除
        //        //var res = con.Delete<SysUser>(5);

        //    }
        //}

        private static void EmitDynamicExmaple()
        {
            SmartBuilder emit = new SmartBuilder("Core.Model");
            emit.Assembly();
            emit.Class("Model", Qualifier.Public);
            for (int i = 0; i < 100; i++)
            {
                emit.CreateProperty("Name" + i, typeof(String));
            }
            EmitDynamic TEST = emit.InitEntity();

            String II = "";

            for (int i = 0; i < 100; i++)
            {
                for (int tt = 0; tt < 100; tt++)
                {
                    TEST["Name" + tt] = tt.ToString();

                    II = TEST["Name" + tt].ToString();
                }
            }

            var text = TEST.ToXml();

            IDictionary<String, Dictionary<String, Object>> Entity = new Dictionary<String, Dictionary<String, Object>>();
            Entity.Add("Model", new Dictionary<String, Object>());

            for (int i = 0; i < 100; i++)
            {
                Entity["Model"].Add("Name" + i, default(String));
            }

            for (int i = 0; i < 100000; i++)
            {
                for (int tt = 0; tt < 100; tt++)
                {
                    Entity["Model"]["Name" + i] = i;
                    II = Entity["Model"]["Name" + i].ToString();
                }
            }
        }
    }

    public class Test
    {
        public int test1 { get; set; }
    }
}
