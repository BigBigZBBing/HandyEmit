using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ILWheatBread.Compress
{
    public class BasicAnalytics
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe static Byte[] GetBytes(Char value)
        {
            Byte[] bytes = new Byte[1];
            fixed (Byte* buf = bytes)
            {
                *(Char*)buf = value;
            }
            return bytes;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe static Byte[] GetBytes(Int16 value)
        {
            Byte[] bytes = new Byte[2];
            fixed (Byte* buf = bytes)
            {
                *(Int16*)buf = value;
            }
            return bytes;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe static Byte[] GetBytes(Int32 value)
        {
            Byte[] bytes = new Byte[4];
            fixed (Byte* buf = bytes)
            {
                *(Int32*)buf = value;
            }
            return bytes;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe static Byte[] GetBytes(Int64 value)
        {
            Byte[] bytes = new Byte[8];
            fixed (Byte* b = bytes)
            {
                *((Int64*)b) = value;
            }
            return bytes;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe static Byte[] GetBytes(Single value)
        {
            Byte[] bytes = new Byte[4];
            fixed (Byte* b = bytes)
            {
                *((Single*)b) = value;
            }
            return bytes;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe static Byte[] GetBytes(Double value)
        {
            Byte[] bytes = new Byte[8];
            fixed (Byte* b = bytes)
            {
                *((Double*)b) = value;
            }
            return bytes;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe static Char BitToChar(Byte[] data)
        {
            fixed (Byte* pbyte = &data[0])
            {
                return *(Char*)pbyte;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe static Int16 BitToInt16(Byte[] data)
        {
            fixed (Byte* pbyte = &data[0])
            {
                return *(Int16*)pbyte;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe static Int32 BitToInt32(Byte[] data)
        {
            fixed (Byte* pbyte = &data[0])
            {
                return *(Int32*)pbyte;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe static Int64 BitToInt64(Byte[] data)
        {
            fixed (Byte* pbyte = &data[0])
            {
                return *(Int64*)pbyte;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe static Single BitToSingle(Byte[] data)
        {
            fixed (Byte* pbyte = &data[0])
            {
                return *(Single*)pbyte;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe static Double BitToDouble(Byte[] data)
        {
            fixed (Byte* pbyte = &data[0])
            {
                return *(Double*)pbyte;
            }
        }

    }
}
