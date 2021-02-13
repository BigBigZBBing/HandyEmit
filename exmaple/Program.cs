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
    public class TestModel
    {
        public string Name { get; set; }
        public int Old { get; set; }
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

    class Program
    {
        static void Main(string[] args)
        {
            //数值数组Copy性能测试
            //BenchmarkRunner.Run<ByteCopyBenchmark>();
            //BenchmarkRunner.Run<UInt32CopyBenchmark>();
            //结果Int32比Byte更有效

            //测试位运算计算
            //BenchmarkRunner.Run<OperatorBanchmark>();

            //BenchmarkRunner.Run<ReflectCreate>();
            BenchmarkRunner.Run<ReflectGetSet>();

            Console.ReadKey();
        }
    }
}
