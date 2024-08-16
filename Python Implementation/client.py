import socket
from diffle_helman import DiffleHelman
import socket
from diffle_helman import DiffleHelman
from ceaser_encoder import CeaserEncoder
from ceaser_decoder import CeaserDecoder

# Função principal do cliente
def main():
    # Solicita ao usuário o IP do servidor
    print("Digite o IP do servidor TCP")
    host = input()

    # Solicita ao usuário a porta do servidor
    print("Digite a porta do servidor TCP")
    port = None

    while port is None:
        try:
            port = int(input())
        except ValueError:
            print("Digita a port direito")

    # Aguarda o servidor estar pronto
    print("Pressione qualquer tecla quando o servidor estiver pronto!")
    input()

    # Cria o socket do cliente e conecta ao servidor
    client = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    client.connect((host, port))

    buffer = bytearray(1024)

    # Recebe r1 do servidor
    received_r = client.recv(1024)
    r1 = int(received_r.decode('utf-8'))

    # Calcula r2 usando o algoritmo Diffle-Helman
    clientY = 6
    r2 = DiffleHelman.calculate_r(clientY)
    # Envia r2 para o servidor
    client.send(str(r2).encode('utf-8'))

    # Calcula a chave compartilhada usando r1
    key = DiffleHelman.calculate_k(clientY, r1)

    # Solicita ao usuário a mensagem para enviar
    print("Digite a mensagem para enviar: ")
    mensagem_para_enviar = input()

    # Inicializa os codificadores e decodificadores de César com a chave compartilhada
    ceaser_encoder = CeaserEncoder(key)
    ceaser_decoder = CeaserDecoder(key)

    # Codifica a mensagem e envia para o servidor
    encrypted_message = ceaser_encoder.encode(mensagem_para_enviar)
    client.send(encrypted_message.encode('utf-8'))

    # Recebe a resposta do servidor
    reply = client.recv(1024)
    reply_text = reply.decode('utf-8')
    # Decodifica a resposta
    reply_text_decoded = ceaser_decoder.decode(reply_text)

    # Imprime a resposta decodificada
    print(reply_text_decoded)

# Executa a função principal se o script for executado diretamente
if __name__ == "__main__":
    main()
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