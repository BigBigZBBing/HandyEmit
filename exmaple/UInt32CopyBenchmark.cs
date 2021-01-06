using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using ILWheatBread.Optimizing;

namespace exmaple
{
    public class UInt32CopyBenchmark
    {
        static int count = 4096;

        int[] test1 = new int[count];

        public UInt32CopyBenchmark()
        {
            Random random = new Random();
            for (int i = 0; i < count; i++)
            {
                test1[i] = (int)random.Next(int.MaxValue);
            }
        }

        [Benchmark(Description = "BasicOptimize.Copy")]
        public void SimdCopy()
        {
            int[] test2 = new int[count];
            BasicOptimize.Copy(test1, test2, count);
        }

        [Benchmark(Description = "Array.Copy")]
        public void ArrayCopy()
        {
            int[] test2 = new int[count];
            Array.Copy(test1, test2, count);
        }

        [Benchmark(Description = "Buffer.BlockCopy")]
        public void BufferCopy()
        {
            int[] test2 = new int[count];
            Buffer.BlockCopy(test1, 0, test2, 0, count);
        }

        [Benchmark(Description = "Buffer.MemoryCopy")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void MemoryCopy()
        {
            int[] test2 = new int[count];
            fixed (int* pbyte1 = test1)
            fixed (int* pbyte2 = test2)
            {
                Buffer.MemoryCopy(pbyte1, pbyte2, count * sizeof(int), count * sizeof(int));
            }
        }

    }
}
