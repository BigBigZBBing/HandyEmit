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

            decimal tt = 3.4564m;

            string test = tt.ToString();

            //数值数组Copy性能测试
            //BenchmarkRunner.Run<ByteCopyBenchmark>();
            //BenchmarkRunner.Run<UInt32CopyBenchmark>();
            //结果Int32比Byte更有效

            //测试位运算计算
            BenchmarkRunner.Run<OperatorBanchmark>();

            Console.ReadKey();
        }
    }
}
