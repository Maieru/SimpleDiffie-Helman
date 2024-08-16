using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comuns
{
    public class CeaserEncoder
    {
        private readonly int _shift;

        public CeaserEncoder(int shift) 
        {
            shift = shift % 255;

            if (shift < 0)
                shift = 255 + shift;

            _shift = shift;
        }

        public string Encode(string plainText)
        {
            var returnString = "";

            foreach (char c in plainText)
            {
                var encodedChar = EncodeChar(c);
                returnString += encodedChar;
            }

            return returnString;
        }

        private char EncodeChar(char encodingChar)
        {
            if (_shift == 0)
                return encodingChar;

            var encodingCharASCII = (int)encodingChar;
            var encodedChar = encodingCharASCII + _shift;

            return (char)(encodedChar % 255);
        }
    }
}
