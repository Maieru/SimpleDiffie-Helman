import socket
from diffle_helman import DiffleHelman
from ceaser_encoder import CeaserEncoder
from ceaser_decoder import CeaserDecoder

def main():
    print("Digite o IP do servidor TCP")
    host = input()

    print("Digite a porta do servidor TCP")
    port = None

    while port is None:
        try:
            port = int(input())
        except ValueError:
            print("Digita a port direito")

    print("Pressione qualquer tecla quando o servidor estiver pronto!")
    input()

    client = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    client.connect((host, port))

    buffer = bytearray(1024)

    received_r = client.recv(1024)
    r1 = int(received_r.decode('utf-8'))

    clientY = 6
    r2 = DiffleHelman.calculate_r(clientY)
    client.send(str(r2).encode('utf-8'))

    key = DiffleHelman.calculate_k(clientY, r1)

    print("Digite a mensagem para enviar: ")
    mensagem_para_enviar = input()

    ceaser_encoder = CeaserEncoder(key)
    ceaser_decoder = CeaserDecoder(key)

    encrypted_message = ceaser_encoder.encode(mensagem_para_enviar)
    client.send(encrypted_message.encode('utf-8'))

    reply = client.recv(1024)
    reply_text = reply.decode('utf-8')
    reply_text_decoded = ceaser_decoder.decode(reply_text)

    print(reply_text_decoded)

if __name__ == "__main__":
    main()