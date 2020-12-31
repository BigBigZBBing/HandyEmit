using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
#if NETCORE5
using System.Runtime.Intrinsics.X86;
using System.Runtime.Intrinsics;
#endif

namespace ILWheatBread.Optimizing
{
    public static class BasicOptimize
    {

#if NETCORE5
        /// <summary>
        /// 这个实现方式特别慢 不适用于简单逻辑的优化
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void CopyIntrinsics(void* arr1, void* arr2, Int32 count)
        {
            Int32 i;
            Vector256<Byte> v1;
            Int32 vectorSize = Vector256<Byte>.Count;
            Byte* pbyte1 = (Byte*)arr1;
            Byte* pbyte2 = (Byte*)arr2;
            for (i = 0; i < count; i++)
            {
                v1 = Avx2.LoadVector256(pbyte1);
                Avx2.Store(pbyte2, v1);
            }
        }
#endif

        public static void Copy(Char[] arr1, Char[] arr2, Int32 count)
        {
            TCopy(arr1, arr2, count);
        }

        public static void Copy(Byte[] arr1, Byte[] arr2, Int32 count)
        {
            TCopy(arr1, arr2, count);
        }

        public static void Copy(Int16[] arr1, Int16[] arr2, Int32 count)
        {
            TCopy(arr1, arr2, count);
        }

        public static void Copy(Int32[] arr1, Int32[] arr2, Int32 count)
        {
            TCopy(arr1, arr2, count);
        }

        public static void Copy(Int64[] arr1, Int64[] arr2, Int32 count)
        {
            TCopy(arr1, arr2, count);
        }

        public static void Copy(Single[] arr1, Single[] arr2, Int32 count)
        {
            TCopy(arr1, arr2, count);
        }

        public static void Copy(Double[] arr1, Double[] arr2, Int32 count)
        {
            TCopy(arr1, arr2, count);
        }

        private static void TCopy<T>(T[] arr1, T[] arr2, Int32 count) where T : struct
        {
            Int32 i;
            Vector<T> v1;
            Int32 vectorSize = Vector<T>.Count;
            if (count >= vectorSize)
            {
                Int32 rest = count % vectorSize;
                Int32 length = count - rest;
                for (i = 0; i < length; i += vectorSize)
                {
                    v1 = new Vector<T>(arr1, i);
                    v1.CopyTo(arr2, i);
                }

                if (rest != 0)
                    Buffer.BlockCopy(arr1, length, arr2, length, rest);
            }
            else
            {
                Buffer.BlockCopy(arr1, 0, arr2, 0, count);
            }
        }

    }
}
