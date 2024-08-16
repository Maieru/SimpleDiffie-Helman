using Comuns;
using System.Net;
using System.Net.Sockets;
using System.Text;

Console.WriteLine("Digite o IP do servidor TCP");
var host = Console.ReadLine();
const int clientY = 19;

int g;
int n;

Console.WriteLine("Digite a porta do servidor TCP");
int? port = null;

while (!port.HasValue)
{
    if (!int.TryParse(Console.ReadLine(), out int portCerta))
        Console.WriteLine("Digita a port direito");
    else
        port = portCerta;
}


Console.WriteLine("Pressione qualquer tecla quando o servidor estiver pronto!");
Console.ReadKey();

IPHostEntry hostEntry = Dns.GetHostEntry("localhost");
IPAddress ipAddress = hostEntry.AddressList[0];
IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);

using Socket client = new(
    ipAddress.AddressFamily,
    SocketType.Stream,
    ProtocolType.Tcp);

var buffer = new byte[1024];

client.Connect(remoteEP);

var receivedR = await client.ReceiveAsync(buffer, 0);   
var r1 = Convert.ToUInt64(Encoding.UTF8.GetString(buffer));

var r2 = Diffle_Helman.CalculateR(clientY);
await client.SendAsync(Encoding.UTF8.GetBytes(r2.ToString()));

var key = Diffle_Helman.CalculateK(clientY, r2);

Console.WriteLine("Digite a mensagem para enviar: ");
var mensagemParaEnviar = Console.ReadLine();

var ceaserEncoder = new CeaserEncoder(5);
var ceaserDecoder = new CeaserDecoder(5);

var encryptedMessage = ceaserEncoder.Encode(mensagemParaEnviar);

var mensagemBytes = Encoding.UTF8.GetBytes(encryptedMessage);

client.Send(mensagemBytes);


var reply = await client.ReceiveAsync(buffer);
var replyText = Encoding.UTF8.GetString(buffer, 0, reply);
var replyTextDecoded = ceaserDecoder.Decode(replyText);

Console.WriteLine(replyTextDecoded);