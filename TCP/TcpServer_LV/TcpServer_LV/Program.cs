using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;

namespace TcpServer_LV
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Console.WriteLine("Erstelle Server");
            Server x = new Server();
            Console.WriteLine("Erstellt");
            while (true)
            {
                Console.WriteLine("\nGeben sie die Nachricht für alle ein:");
                string g = Console.ReadLine();
                while (true)
                {
                    try
                    {
                        foreach (Server.extended y in x.clientList)
                        {
                            try
                            {
                                NetworkStream clientStream = y.tcp.GetStream();
                                ASCIIEncoding encoder = new ASCIIEncoding();
                                byte[] buffer = encoder.GetBytes(g);

                                clientStream.Write(buffer, 0, buffer.Length);
                                clientStream.Flush();


                            }
                            catch
                            {
                                x.clientList.Remove(y);
                            }

                        }
                        break;
                    }
                    catch
                    {
                    }
                }

                
            }
            
            
        }

        


       
    }
}
