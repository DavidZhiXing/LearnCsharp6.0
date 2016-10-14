using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
namespace CsharpCookBook
{
    class UnionStruct
    {
    }

    [StructLayoutAttribute(LayoutKind.Explicit)]
    struct SignedNumber
    {
        [FieldOffsetAttribute(0)]
        public sbyte num1;
        [FieldOffsetAttribute(0)]
        public short num2;
        [FieldOffsetAttribute(0)]
        public int num3;
        [FieldOffsetAttribute(0)]
        public long num4;
        [FieldOffsetAttribute(0)]
        public float num5;
        [FieldOffsetAttribute(0)]
        public double num6;
    }

    [StructLayoutAttribute(LayoutKind.Explicit)]
    struct SignedNumberWithText
    {
        [FieldOffsetAttribute(0)]
        public sbyte num1;
        [FieldOffsetAttribute(0)]
        public short num2;
        [FieldOffsetAttribute(0)]
        public int num3;
        [FieldOffsetAttribute(0)]
        public long num4;
        [FieldOffsetAttribute(0)]
        public float num5;
        [FieldOffsetAttribute(0)]
        public double num6;
        [FieldOffsetAttribute(0)]
        public string text1;
    }
}
