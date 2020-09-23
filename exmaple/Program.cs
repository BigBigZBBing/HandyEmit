using HandyEmit;
using HandyEmit.SmartEmit;
using HandyEmit.SmartEmit.Field;
using Google.Protobuf.WellKnownTypes;
using NakedORM;
using NakedORM.Simple;
using exmaple.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Emit;
using System.Text;

namespace exmaple
{
    class Program
    {

        static IRepository _Repository { get; set; }
        static void Main(string[] args)
        {
            Func<Int64, Int32, Int64> func = SmartBuilder.DynamicMethod<Func<Int64, Int32, Int64>>(String.Empty, il =>
            {
                var long1 = il.NewInt64();
                var int1 = il.NewInt32();
                il.Emit(OpCodes.Ldarg_0);
                long1.PushSt();

                il.Emit(OpCodes.Ldarg_1);
                int1.PushSt();

                var res = long1 - int1;

                res.PushLd();
                il.Emit(OpCodes.Ret);
            });

            Int64 test = func.Invoke(Int64.MaxValue, 10000);

            //EmitModelTest();

            DbOperation();

            Console.ReadKey();
        }

        public static string Create(int length)
        {
            // 创建一个StringBuilder对象存储密码
            StringBuilder sb = new StringBuilder();
            //使用for循环把单个字符填充进StringBuilder对象里面变成14位密码字符串
            for (int i = 0; i < length; i++)
            {
                Random random = new Random(Guid.NewGuid().GetHashCode());
                //随机选择里面其中的一种字符生成
                switch (random.Next(3))
                {
                    case 0:
                        //调用生成生成随机数字的方法
                        sb.Append(createNum());
                        break;
                    case 1:
                        //调用生成生成随机小写字母的方法
                        sb.Append(createSmallAbc());
                        break;
                    case 2:
                        //调用生成生成随机大写字母的方法
                        sb.Append(createBigAbc());
                        break;
                }
            }
            return sb.ToString();
        }

        private static string createSmallAbc()
        {
            //a-z的 ASCII值为97-122
            Random random = new Random(Guid.NewGuid().GetHashCode());
            int num = random.Next(97, 123);
            string abc = Convert.ToChar(num).ToString();
            return abc;
        }

        private static string createBigAbc()
        {
            //A-Z的 ASCII值为65-90
            Random random = new Random(Guid.NewGuid().GetHashCode());
            int num = random.Next(65, 91);
            string abc = Convert.ToChar(num).ToString();
            return abc;
        }

        private static int createNum()
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());
            int num = random.Next(10);
            return num;
        }

        private static void DbOperation()
        {
            _Repository = new Repository("Data Source=localhost;Initial Catalog=dapperDb; User Id=sa;Password=111111");

            using (var con = _Repository.ConnectMSSQL())
            {
                using (var tran = con.BeginTranScoape())
                {
                    ////主键查询
                    //var list1 = con.GetKey<SysUser>(1);

                    ////条件查询
                    //var list = con.GetList<SysUser>(where =>
                    //{
                    //    where
                    //    ._("Name", "戴志伟");
                    //});

                    try
                    {
                        //for (int i = 0; i < 10000; i++)
                        //{
                        //    //单量新增
                        //    var res = con.Add(new TestModel()
                        //    {
                        //        test2 = new Random().Next(0, 10000),
                        //        test3 = Create(100),
                        //        test4 = Convert.ToDecimal(new Random().Next(0, 10000) + new Random().NextDouble()),
                        //        test5 = DateTime.Now.AddSeconds(new Random().Next(-1000, 1000)),
                        //        test6 = Convert.ToSingle(new Random().Next(0, 10000) + new Random().NextDouble())
                        //    });
                        //}

                        List<TestModel> model = new List<TestModel>();
                        for (int i = 0; i < 100000; i++)
                        {
                            model.Add(new TestModel()
                            {
                                test2 = new Random().Next(0, 10000),
                                test3 = Create(100),
                                test4 = Convert.ToDecimal(new Random().Next(0, 10000) + new Random().NextDouble()),
                                test5 = DateTime.Now.AddSeconds(new Random().Next(-1000, 1000)),
                                test6 = Convert.ToSingle(new Random().Next(0, 10000) + new Random().NextDouble())
                            });
                        }

                        var res = con.AddRange(model);

                        tran.Commit();
                    }
                    catch (System.Exception ex)
                    {
                        tran.Rollback();
                    }

                }


                //分页查询
                //DbPager pager = new DbPager(2, 2);
                //var list = con.GetListPager<SysUser>(pager);
                //Int32 count = Convert.ToInt32(pager.TotalCount);

                //个别字段查询
                //var list2 = con.GetList<SysUser>(Reveal: x => new { x.Name, x.Old });



                //批量新增
                //var res = con.AddRange(new List<SysUser>()
                //{
                //    new SysUser(){
                //        Name = "胡友洋",
                //        Old = 32
                //    },
                //    new SysUser(){
                //        Name = "邓超",
                //        Old = 23
                //    },
                //    new SysUser(){
                //        Name = "孙博强",
                //        Old = 26
                //    }
                //});

                //更新
                //IList<DbField> set = new List<DbField>();
                //set.Push("Name", "邓超");
                //IList<DbField> where = new List<DbField>();
                //where.Push("Name", "邓超1");
                //var res = con.Update<SysUser>(set, where);

                //删除
                //var res = con.Delete<SysUser>(5);

            }
        }

        private static void EmitModelTest()
        {
            SmartBuilder emit = new SmartBuilder("Core.Model");
            emit.Assembly();
            emit.Class("Model", ClassType.公共);
            for (int i = 0; i < 100; i++)
            {
                emit.SmartProperty("Name" + i, typeof(String));
            }
            emit.SaveClass();

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
}
