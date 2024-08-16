using Comuns;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

Console.WriteLine("Digite a porta do servidor TCP");
int? port = null;

const int serverX = 91;

var buffer = new byte[1024];

while (!port.HasValue)
{
    if (!int.TryParse(Console.ReadLine(), out int portCerta))
        Console.WriteLine("Digita a port direito");
    else
        port = portCerta;
}

IPHostEntry host = Dns.GetHostEntry("10.1.70.25");
IPAddress ipAddress = host.AddressList[0];
IPEndPoint localEndPoint = new IPEndPoint(ipAddress, port.Value);

var ceaserEncoder = new CeaserEncoder(5);
var ceaserDecoder = new CeaserDecoder(5);

using Socket listener = new(
    ipAddress.AddressFamily,
    SocketType.Stream,
    ProtocolType.Tcp);

listener.Bind(localEndPoint);
listener.Listen();
var handler = await listener.AcceptAsync();

var r1 = Diffle_Helman.CalculateR(serverX);
await handler.SendAsync(Encoding.UTF8.GetBytes(r1.ToString()));

var receivedR = await handler.ReceiveAsync(buffer, 0);
var r2 = Convert.ToInt32(Encoding.UTF8.GetString(buffer));

var key = Diffle_Helman.CalculateK(serverX, r1);

Console.WriteLine("Pode iniciar o client");

while (true)
{
    var received = await handler.ReceiveAsync(buffer, SocketFlags.None);
    var receivedText = Encoding.UTF8.GetString(buffer, 0, received);

    var decryptedText = ceaserDecoder.Decode(receivedText);

    var response = "";

    foreach (var character in decryptedText)
    {
        if (char.IsDigit(character))
            response += character.ToString().ToUpper();
        else
            response += character;
    }

    var responseEncrypted = ceaserEncoder.Encode(response);
    var responseBytes = Encoding.UTF8.GetBytes(responseEncrypted);
    await handler.SendAsync(responseBytes, 0);
}