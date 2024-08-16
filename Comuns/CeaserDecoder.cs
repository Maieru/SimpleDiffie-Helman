using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comuns
{
    // Classe responsável por decodificar mensagens usando a cifra de César
    public class CeaserDecoder
    {
        private readonly int _shift; // Deslocamento usado para decodificação

        // Construtor que inicializa o deslocamento com o valor negativo do parâmetro fornecido
        public CeaserDecoder(int shift)
        {
            _shift = -shift;
        }

        // Método que decodifica o texto cifrado
        public string Decode(string plainText)
        {
            // Utiliza a classe CeaserEncoder para realizar a decodificação
            // A decodificação é feita aplicando a cifra de César com o deslocamento negativo
            return new CeaserEncoder(_shift).Encode(plainText);
        }
    }
}
