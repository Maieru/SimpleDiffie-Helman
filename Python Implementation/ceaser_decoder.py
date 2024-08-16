from ceaser_encoder import CeaserEncoder

# Classe para decodificar mensagens usando a cifra de César
class CeaserDecoder:
    def __init__(self, shift):
        """
        Inicializa o decodificador com um deslocamento negativo.
        
        :param shift: O deslocamento usado para a cifra de César
        """
        self._shift = -shift

    def decode(self, plain_text):
        """
        Decodifica o texto cifrado usando a cifra de César.
        
        :param plain_text: O texto cifrado a ser decodificado
        :return: O texto decodificado
        """
        # Utiliza o codificador de César com o deslocamento negativo para decodificar
        return CeaserEncoder(self._shift).encode(plain_text)