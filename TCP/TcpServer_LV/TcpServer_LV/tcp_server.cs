using System;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using System.Collections;
using System.IO;
namespace TcpServer_LV
{
    class Server
    {
        private TcpListener tcpListener;
        private Thread listenThread;
        private Thread scanner;
        public ArrayList clientList = new ArrayList();

        public Server()
        {
            this.tcpListener = new TcpListener(IPAddress.Any, 3000);
            this.listenThread = new Thread(new ThreadStart(ListenForClients));
            this.listenThread.Start();
            this.scanner = new Thread(new ThreadStart(print));

        }

        public struct extended
        {
           public TcpClient tcp;
           public string IP;
           public string Name;
        }

        private void ListenForClients()
        {
            this.tcpListener.Start();

            while (true)
            {
                lock (this)
                {
                    //blocks until a client has connected to the server
                    TcpClient client = this.tcpListener.AcceptTcpClient();
                    extended y = new extended();

                    //Packe alle ins struct und dann in eine Liste zu späteren vewendung

                    y.IP = client.Client.RemoteEndPoint.ToString();
                    y.tcp = client;
                    clientList.Add(y);

                    //create a thread to handle communication
                    //with connected client
                    Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClientComm));
                    clientThread.Start(client);
                }
            }
        }

        private void HandleClientComm(object client)
        {
            TcpClient tcpClient = (TcpClient)client;
            NetworkStream clientStream = tcpClient.GetStream();

            byte[] message = new byte[4096];
            int bytesRead;

            while (true)
            {
                bytesRead = 0;

                try
                {
                    //blocks until a client sends a message
                    bytesRead = clientStream.Read(message, 0, 4096);
                }
                catch
                {
                    //a socket error has occured
                    break;
                }

                if (bytesRead == 0)
                {
                    //the client has disconnected from the server
                    break;
                }

                //message has successfully been received
                ASCIIEncoding encoder = new ASCIIEncoding();
                Console.WriteLine("Client: " +encoder.GetString(message, 0, bytesRead));
            }

            tcpClient.Close();
        }

        void print()
        {
            scan x = new scan();
            while (true)
            {
                System.Threading.Thread.Sleep(5000);
                StreamWriter w = new StreamWriter("online.txt");
                w.Write(x.online(clientList));
                w.Close();
                
            }
        }
    }
}