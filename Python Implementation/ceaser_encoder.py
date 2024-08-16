class CeaserEncoder:
    def __init__(self, shift):
        """
        Inicializa o codificador de César com um deslocamento.
        
        :param shift: O deslocamento usado para a cifra de César
        """
        # Ajusta o deslocamento para estar dentro do intervalo de 0 a 254
        shift = shift % 255
        if shift < 0:
            shift = 255 + shift
        self._shift = shift

    def encode(self, plain_text):
        """
        Codifica o texto plano usando a cifra de César.
        
        :param plain_text: O texto plano a ser codificado
        :return: O texto codificado
        """
        # Codifica cada caractere do texto plano
        return ''.join([self._encode_char(c) for c in plain_text])

    def _encode_char(self, encoding_char):
        """
        Codifica um único caractere usando a cifra de César.
        
        :param encoding_char: O caractere a ser codificado
        :return: O caractere codificado
        """
        if self._shift == 0:
            return encoding_char
        # Converte o caractere para seu valor ASCII
        encoding_char_ascii = ord(encoding_char)
        # Aplica o deslocamento
        encoded_char = encoding_char_ascii + self._shift
        # Converte de volta para caractere, garantindo que esteja no intervalo ASCII
        return chr(encoded_char % 255)