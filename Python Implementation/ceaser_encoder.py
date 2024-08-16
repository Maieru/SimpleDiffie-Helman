class CeaserEncoder:
    def __init__(self, shift):
        shift = shift % 255
        if shift < 0:
            shift = 255 + shift
        self._shift = shift

    def encode(self, plain_text):
        return ''.join([self._encode_char(c) for c in plain_text])

    def _encode_char(self, encoding_char):
        if self._shift == 0:
            return encoding_char
        encoding_char_ascii = ord(encoding_char)
        encoded_char = encoding_char_ascii + self._shift
        return chr(encoded_char % 255)