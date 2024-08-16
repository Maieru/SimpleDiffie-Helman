import socket
import threading
import sys
from diffle_helman import DiffleHelman
from ceaser_encoder import CeaserEncoder
from ceaser_decoder import CeaserDecoder

def handle_client(client_socket, serverX):
    buffer = bytearray(1024)

    r1 = DiffleHelman.calculate_r(serverX)
    client_socket.send(str(r1).encode('utf-8'))

    received_r = client_socket.recv(1024)
    r2 = int(received_r.decode('utf-8'))

    key = DiffleHelman.calculate_k(serverX, r2)

    ceaser_encoder = CeaserEncoder(key)
    ceaser_decoder = CeaserDecoder(key)

    print("Pode iniciar o client")

    while True:
        received = client_socket.recv(1024)
        received_text = received.decode('utf-8')

        decrypted_text = ceaser_decoder.decode(received_text)

        response = ''.join([char.upper() if char.isalpha() else char for char in decrypted_text])

        response_encrypted = ceaser_encoder.encode(response)
        client_socket.send(response_encrypted.encode('utf-8'))

def main():
    print("Digite a porta do servidor TCP")
    port = None

    while port is None:
        try:
            port = int(input())
        except ValueError:
            print("Digita a port direito")

    serverX = 3

    server = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    server.bind(('localhost', port))
    server.listen(5)

    print("Servidor escutando na porta", port)

    while True:
        client_socket, addr = server.accept()
        client_handler = threading.Thread(target=handle_client, args=(client_socket, serverX))
        client_handler.start()

if __name__ == "__main__":
    main()