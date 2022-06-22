using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace app
{
    class UDPServer
    {
        private const int listenPort = 11000;

        static void Main(string[] args)
        {
            StartListener();
        }
        
        private static void StartListener()
        {
            UdpClient listener = new UdpClient(listenPort); //Server will locate at listen port
            IPEndPoint groupEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), listenPort);

            try
            {
                while (true)
                {
                    Console.WriteLine("Waiting for broadcast"); //Wait for any client send message
                    byte[] bytes = listener.Receive(ref groupEP);

                    var receivedMessage = Encoding.ASCII.GetString(bytes, 0, bytes.Length); //Covert bytes to string
                    Console.WriteLine($"Received broadcast from {groupEP} : {receivedMessage}");

                    //Send back to connected client
                    byte[] sendBuffer = Encoding.ASCII.GetBytes("Yeah, I heard you");
                    listener.Send(sendBuffer, sendBuffer.Length, groupEP);
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                listener.Close();
            }
        }
    }
}
