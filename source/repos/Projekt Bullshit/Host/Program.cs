using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

Console.WriteLine("[BS] - Host initialized"); //stworz port i adres

Int32 port = 13000;
IPAddress localadress = IPAddress.Parse("127.0.0.1");

Console.WriteLine("[BS] - Starting server"); //uruchom serwer

TcpListener server = new TcpListener(localadress, port);

server.Start();

Byte[] bytes = new Byte[256];
String data = null;

Console.WriteLine("[BS] - Started listening");

while (true)
{
    Console.Write("[BS] - Awaiting connection... ");
    TcpClient client = server.AcceptTcpClient();
    Console.WriteLine("Connected!");

    data = null;

    NetworkStream stream = client.GetStream();

    int i;

    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
    {
        // Translate data bytes to a ASCII string.
        data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
        Console.WriteLine("Received: {0}", data);

        // Process the data sent by the client.
        data = data.ToUpper();

        byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);

        // Send back a response.
        stream.Write(msg, 0, msg.Length);
        Console.WriteLine("Sent: {0}", data);
    }

    client.Close();
}

server.Stop();

Console.WriteLine("[BS] - Initiated shutdown");
Console.WriteLine("[BS] - Client closed");
Console.WriteLine("[BS] - Stopped listening");
Console.WriteLine("[BS] - Server closed");

Console.Read();