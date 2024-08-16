using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comuns
{
    public static class Diffle_Helman
    {
        public static int G = 7;
        public static int N = 23;

        public static UInt64 CalculateR(int privateNumber) => Convert.ToUInt64(Math.Pow(G, privateNumber) % N);
        public static int CalculateK(int privateNumber, UInt64 R) => Convert.ToInt32(Math.Pow(R, privateNumber) % N);
    }
}
