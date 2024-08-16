using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comuns
{
    // Classe estática responsável por implementar o algoritmo de Diffie-Hellman
    public static class Diffle_Helman
    {
        // Constantes públicas usadas no cálculo de Diffie-Hellman
        public static int G = 7; // Base (gerador)
        public static int N = 23; // Módulo (número primo)

        // Método para calcular o valor R usando a fórmula: R = (G^privateNumber) % N
        public static UInt64 CalculateR(int privateNumber) => Convert.ToUInt64(Math.Pow(G, privateNumber) % N);

        // Método para calcular a chave compartilhada K usando a fórmula: K = (R^privateNumber) % N
        public static int CalculateK(int privateNumber, UInt64 R) => Convert.ToInt32(Math.Pow(R, privateNumber) % N);
    }
}
