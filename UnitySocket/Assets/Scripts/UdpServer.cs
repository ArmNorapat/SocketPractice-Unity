using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class UdpServer : MonoBehaviour
{
    private const int listenPort = 11000;

    // Start is called before the first frame update
    void Start()
    {
        Thread serverThread = new Thread(StartListener);
        serverThread.IsBackground = true;
        serverThread.Start();
    }

    void StartListener()
    {
        UdpClient listener = new UdpClient(listenPort); //Server will locate at listen port
        IPEndPoint groupEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), listenPort);

        try
        {
            while (true)
            {
                Debug.Log("Waiting for broadcast"); //Wait for any client send message
                byte[] bytes = listener.Receive(ref groupEP);

                var receivedMessage = Encoding.ASCII.GetString(bytes, 0, bytes.Length); //Covert bytes to string
                Debug.Log($"Received broadcast from {groupEP} : {receivedMessage}");

                JobManager.Instance.AddJob(()=>
                {
                    //Do unity behaviour stuff
                });

                //Send back to connected client
                byte[] sendBuffer = Encoding.ASCII.GetBytes("Yeah, I heard you");
                listener.Send(sendBuffer, sendBuffer.Length, groupEP);
            }
        }
        catch (SocketException e)
        {
            Debug.Log(e);
        }
        finally
        {
            listener.Close();
        }
    }
}
