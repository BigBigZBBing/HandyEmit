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
    public class CopyBenchmark
    {
        static int count = 320000008;

        byte[] test1 = new byte[count];

        public CopyBenchmark()
        {
            Random random = new Random();
            for (int i = 0; i < count; i++)
            {
                test1[i] = (byte)random.Next(byte.MaxValue);
            }
        }

        [Benchmark(Description = "BasicOptimize.Copy")]
        public void SimdCopy()
        {
            byte[] test2 = new byte[count];
            BasicOptimize.Copy(test1, test2, count);
        }

        [Benchmark(Description = "Array.Copy")]
        public void ArrayCopy()
        {
            byte[] test2 = new byte[count];
            Array.Copy(test1, test2, count);
        }

        [Benchmark(Description = "Buffer.BlockCopy")]
        public void BufferCopy()
        {
            byte[] test2 = new byte[count];
            Buffer.BlockCopy(test1, 0, test2, 0, count);
        }

        [Benchmark(Description = "Buffer.MemoryCopy")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void MemoryCopy()
        {
            byte[] test2 = new byte[count];
            fixed (byte* pbyte1 = test1)
            fixed (byte* pbyte2 = test2)
            {
                Buffer.MemoryCopy(pbyte1, pbyte2, count * sizeof(byte), count * sizeof(byte));
            }
        }

    }
}
