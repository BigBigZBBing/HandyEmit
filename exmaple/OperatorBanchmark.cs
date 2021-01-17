using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ILWheatBread.Optimizing.BasicOptimize;

namespace exmaple
{
    public class OperatorBanchmark
    {
        [Benchmark(Description = "Add_1")]
        public void Add_1()
        {
            _ = 4894 + 2337;
        }

        [Benchmark(Description = "Add_2")]
        public void Add_2()
        {
            _ = Add(4894, 2337);
        }
    }
}
