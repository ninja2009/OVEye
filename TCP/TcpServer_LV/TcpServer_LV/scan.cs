using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace TcpServer_LV
{
    class scan
    {
        public string online(ArrayList x)
        {
            string on = string.Empty;
            foreach (Server.extended y in x)
            {
                try
                {
                    NetworkStream clientStream = y.tcp.GetStream();
                    ASCIIEncoding encoder = new ASCIIEncoding();
                    byte[] buffer = encoder.GetBytes("check");

                    clientStream.Write(buffer, 0, buffer.Length);
                    clientStream.Flush();
                    on += y.IP + "\n";
                    

                }
                catch 
                {
 
                }
                
            }
            return on;
        }


    }
}
