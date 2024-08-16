using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comuns
{
    public static class Diffle_Helman
    {
        public static int G = 17;
        public static int N = 127;

        public static UInt64 CalculateR(int privateNumber) => Convert.ToUInt64(Math.Pow(G, privateNumber) % N);
        public static UInt64 CalculateK(int privateNumber, UInt64 R) => Convert.ToUInt64(Math.Pow(R, privateNumber) % N);
    }
}
