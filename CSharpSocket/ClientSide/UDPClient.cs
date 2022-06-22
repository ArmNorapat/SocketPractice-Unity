using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace app
{
    class UDPClient
    {
        private const int serverPort = 11000;

        static void Main(string[] args)
        {
            var client = new UdpClient(); // Create client
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), serverPort); // Endpoint where server is listening
            client.Connect(ep); //Connect to endpoint according IPEndPointObject

            Console.WriteLine($"Send : {args[0]}");
            byte[] sendBuffer = Encoding.ASCII.GetBytes(args[0]); //Convert to byte
            client.Send(sendBuffer, sendBuffer.Length); // send data

            // Receive data from server
            var receivedData = client.Receive(ref ep);
            var receivedMessage = Encoding.ASCII.GetString(receivedData);
            Console.WriteLine($"Receive data from {ep.ToString()} : {receivedMessage}");
        }
    }
}
