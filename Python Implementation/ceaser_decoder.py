from ceaser_encoder import CeaserEncoder

class CeaserDecoder:
    def __init__(self, shift):
        self._shift = -shift

    def decode(self, plain_text):
        return CeaserEncoder(self._shift).encode(plain_text)