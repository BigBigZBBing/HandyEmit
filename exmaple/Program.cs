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
        static void Main(string[] args)
        {

            decimal tt = 3.4564m;

            string test = tt.ToString();

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
