using Comuns;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

// Exibe uma mensagem solicitando a porta do servidor TCP
Console.WriteLine("Digite a porta do servidor TCP");
int? port = null; // Variável para armazenar a porta do servidor

const int serverX = 3; // Constante usada no cálculo Diffie-Hellman

var buffer = new byte[1024]; // Buffer para armazenar dados recebidos

// Loop para garantir que uma porta válida seja inserida
while (!port.HasValue)
{
    if (!int.TryParse(Console.ReadLine(), out int portCerta))
        Console.WriteLine("Digita a port direito"); // Mensagem de erro se a porta não for válida
    else
        port = portCerta; // Armazena a porta válida
}

// Obtém a entrada de host para "localhost"
IPHostEntry host = Dns.GetHostEntry("localhost");
IPAddress ipAddress = host.AddressList[0]; // Obtém o primeiro endereço IP da lista
IPEndPoint localEndPoint = new IPEndPoint(ipAddress, port.Value); // Cria um ponto de extremidade com o IP e a porta

// Cria um socket para ouvir conexões TCP
using Socket listener = new(
    ipAddress.AddressFamily,
    SocketType.Stream,
    ProtocolType.Tcp);

listener.Bind(localEndPoint); // Associa o socket ao ponto de extremidade local
listener.Listen(); // Coloca o socket em modo de escuta para conexões de entrada
var handler = await listener.AcceptAsync(); // Aceita uma conexão de cliente

// Calcula o valor R usando Diffie-Hellman e envia para o cliente
var r1 = Diffle_Helman.CalculateR(serverX);
await handler.SendAsync(Encoding.UTF8.GetBytes(r1.ToString()));

// Recebe o valor R do cliente
var receivedR = await handler.ReceiveAsync(buffer, 0);
var r2 = Convert.ToUInt64(Encoding.UTF8.GetString(buffer));

// Calcula a chave compartilhada usando Diffie-Hellman
var key = Diffle_Helman.CalculateK(serverX, r2);

// Cria instâncias dos codificadores e decodificadores de César com a chave compartilhada
var ceaserEncoder = new CeaserEncoder(key);
var ceaserDecoder = new CeaserDecoder(key);

Console.WriteLine("Pode iniciar o client"); // Mensagem indicando que o cliente pode iniciar

// Loop para processar mensagens recebidas do cliente
while (true)
{
    var received = await handler.ReceiveAsync(buffer, SocketFlags.None); // Recebe dados do cliente
    var receivedText = Encoding.UTF8.GetString(buffer, 0, received); // Converte os dados recebidos para string

    var decryptedText = ceaserDecoder.Decode(receivedText); // Decodifica o texto recebido

    var response = ""; // Variável para armazenar a resposta

    // Converte letras para maiúsculas e mantém outros caracteres inalterados
    foreach (var character in decryptedText)
    {
        if (char.IsLetter(character))
            response += character.ToString().ToUpper();
        else
            response += character;
    }

    // Codifica a resposta e envia de volta para o cliente
    var responseEncrypted = ceaserEncoder.Encode(response);
    var responseBytes = Encoding.UTF8.GetBytes(responseEncrypted);
    await handler.SendAsync(responseBytes, 0);
}
