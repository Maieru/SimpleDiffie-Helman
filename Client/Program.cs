using Comuns;
using System.Net;
using System.Net.Sockets;
using System.Text;

Console.WriteLine("Digite o IP do servidor TCP");
var host = Console.ReadLine(); // Lê o IP do servidor a partir da entrada do usuário
const int clientY = 6; // Constante usada no cálculo Diffie-Hellman

Console.WriteLine("Digite a porta do servidor TCP");
int? port = null; // Variável para armazenar a porta do servidor

// Loop para garantir que uma porta válida seja inserida
while (!port.HasValue)
{
    if (!int.TryParse(Console.ReadLine(), out int portCerta))
        Console.WriteLine("Digita a port direito"); // Mensagem de erro se a porta não for válida
    else
        port = portCerta; // Armazena a porta válida
}

Console.WriteLine("Pressione qualquer tecla quando o servidor estiver pronto!");
Console.ReadKey(); // Aguarda o usuário pressionar uma tecla

// Obtém a entrada de host para o IP fornecido
IPHostEntry hostEntry = Dns.GetHostEntry(host);
IPAddress ipAddress = hostEntry.AddressList[0]; // Obtém o primeiro endereço IP da lista
IPEndPoint remoteEP = new IPEndPoint(ipAddress, port.Value); // Cria um ponto de extremidade com o IP e a porta

// Cria um socket para se conectar ao servidor TCP
using Socket client = new(
    ipAddress.AddressFamily,
    SocketType.Stream,
    ProtocolType.Tcp);

var buffer = new byte[1024]; // Buffer para armazenar dados recebidos

client.Connect(remoteEP); // Conecta ao servidor

// Recebe o valor R do servidor
var receivedR = await client.ReceiveAsync(buffer, 0);
var r1 = Convert.ToUInt64(Encoding.UTF8.GetString(buffer));

// Calcula o valor R usando Diffie-Hellman e envia para o servidor
var r2 = Diffle_Helman.CalculateR(clientY);
await client.SendAsync(Encoding.UTF8.GetBytes(r2.ToString()));

// Calcula a chave compartilhada usando Diffie-Hellman
var key = Diffle_Helman.CalculateK(clientY, r1);

Console.WriteLine("Digite a mensagem para enviar: ");
var mensagemParaEnviar = Console.ReadLine(); // Lê a mensagem a ser enviada a partir da entrada do usuário

// Cria instâncias dos codificadores e decodificadores de César com a chave compartilhada
var ceaserEncoder = new CeaserEncoder(key);
var ceaserDecoder = new CeaserDecoder(key);

// Codifica a mensagem e a converte para bytes
var encryptedMessage = ceaserEncoder.Encode(mensagemParaEnviar);
var mensagemBytes = Encoding.UTF8.GetBytes(encryptedMessage);

client.Send(mensagemBytes); // Envia a mensagem codificada para o servidor

// Recebe a resposta do servidor
var reply = await client.ReceiveAsync(buffer);
var replyText = Encoding.UTF8.GetString(buffer, 0, reply); // Converte a resposta recebida para string
var replyTextDecoded = ceaserDecoder.Decode(replyText); // Decodifica a resposta

Console.WriteLine(replyTextDecoded); // Exibe a resposta decodificada
