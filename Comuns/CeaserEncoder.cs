using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comuns
{
    // Classe responsável por codificar mensagens usando a cifra de César
    public class CeaserEncoder
    {
        private readonly int _shift; // Deslocamento usado para codificação

        // Construtor que inicializa o deslocamento, garantindo que esteja no intervalo de 0 a 254
        public CeaserEncoder(int shift)
        {
            shift = shift % 255; // Garante que o deslocamento esteja no intervalo de 0 a 254

            if (shift < 0)
                shift = 255 + shift; // Ajusta o deslocamento se for negativo

            _shift = shift; // Armazena o deslocamento ajustado
        }

        // Método que codifica o texto plano
        public string Encode(string plainText)
        {
            var returnString = ""; // String para armazenar o texto codificado

            // Codifica cada caractere do texto plano
            foreach (char c in plainText)
            {
                var encodedChar = EncodeChar(c); // Codifica o caractere
                returnString += encodedChar; // Adiciona o caractere codificado à string de retorno
            }

            return returnString; // Retorna o texto codificado
        }

        // Método privado que codifica um único caractere
        private char EncodeChar(char encodingChar)
        {
            if (_shift == 0)
                return encodingChar; // Retorna o caractere original se o deslocamento for 0

            var encodingCharASCII = (int)encodingChar; // Converte o caractere para seu valor ASCII
            var encodedChar = encodingCharASCII + _shift; // Aplica o deslocamento

            return (char)(encodedChar % 255); // Retorna o caractere codificado, garantindo que esteja no intervalo ASCII
        }
    }
}
