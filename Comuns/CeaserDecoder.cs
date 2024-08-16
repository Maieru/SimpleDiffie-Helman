using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comuns
{
    public class CeaserDecoder
    {
        private readonly int _shift;

        public CeaserDecoder(int shift)
        {
            _shift = -shift;
        }

        public string Decode(string plainText)
        {
            return new CeaserEncoder(_shift).Encode(plainText);
        }
    }
}
