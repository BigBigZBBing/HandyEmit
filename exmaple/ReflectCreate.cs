using BenchmarkDotNet.Attributes;
using ILWheatBread;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace exmaple
{
    public class ReflectCreate
    {
        [MethodImplAttribute(MethodImplOptions.AggressiveInlining)]
        [Benchmark(Description = "NormalNew")]
        public void NormalNew()
        {
            new Model();
        }

        [MethodImplAttribute(MethodImplOptions.AggressiveInlining)]
        [Benchmark(Description = "ReflectNew")]
        public void ReflectNew()
        {
            Activator.CreateInstance(typeof(Model));
        }

        [MethodImplAttribute(MethodImplOptions.AggressiveInlining)]
        [Benchmark(Description = "EmitNew")]
        public void EmitNew()
        {
            FastReflect.New<Model>();
        }
    }
}
