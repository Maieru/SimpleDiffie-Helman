import socket
import threading
import sys
from diffle_helman import DiffleHelman
from ceaser_encoder import CeaserEncoder
from ceaser_decoder import CeaserDecoder

# Função para lidar com a comunicação com o cliente
def handle_client(client_socket, serverX):
    buffer = bytearray(1024)

    # Calcula o valor r1 usando o algoritmo Diffle-Hellman
    r1 = DiffleHelman.calculate_r(serverX)
    # Envia r1 para o cliente
    client_socket.send(str(r1).encode('utf-8'))

    # Recebe r2 do cliente
    received_r = client_socket.recv(1024)
    r2 = int(received_r.decode('utf-8'))

    # Calcula a chave compartilhada usando r2
    key = DiffleHelman.calculate_k(serverX, r2)

    # Inicializa os codificadores e decodificadores de César com a chave compartilhada
    ceaser_encoder = CeaserEncoder(key)
    ceaser_decoder = CeaserDecoder(key)

    print("Pode iniciar o client")

    while True:
        # Recebe a mensagem do cliente
        received = client_socket.recv(1024)
        received_text = received.decode('utf-8')

        # Decodifica a mensagem recebida
        decrypted_text = ceaser_decoder.decode(received_text)

        # Converte a mensagem decodificada para maiúsculas
        response = ''.join([char.upper() if char.isalpha() else char for char in decrypted_text])

        # Codifica a resposta
        response_encrypted = ceaser_encoder.encode(response)
        # Envia a resposta codificada de volta para o cliente
        client_socket.send(response_encrypted.encode('utf-8'))

def main():
    print("Digite a porta do servidor TCP")
    port = None

    # Solicita ao usuário a porta do servidor
    while port is None:
        try:
            port = int(input())
        except ValueError:
            print("Digita a port direito")

    serverX = 3

    # Cria o socket do servidor
    server = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    server.bind(('localhost', port))
    server.listen(5)

    print("Servidor escutando na porta", port)

    while True:
        # Aceita uma nova conexão de cliente
        client_socket, addr = server.accept()
        # Cria uma nova thread para lidar com o cliente
        client_handler = threading.Thread(target=handle_client, args=(client_socket, serverX))
        client_handler.start()

if __name__ == "__main__":
    main()