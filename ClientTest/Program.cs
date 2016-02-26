using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.IO;
using SocketLibrary;

namespace ClientTest
{
    class Program
    {
        static void Main(string[] args)
        {
            int port = 9000;
            TcpClient client = new TcpClient("localhost", port);

            SocketHelper socketHelper = new SocketHelper(client);
            socketHelper.Open();

            while (true)
            {
                Console.WriteLine("Anna komento");
                string komento = Console.ReadLine();

                // lähetetään komento
                socketHelper.Write(komento);

                // odotetaan vastausta
                string vastaus = socketHelper.Read();
                Console.WriteLine(vastaus);
                if (komento == COMMANDS.QUIT)
                    break;
            }

            socketHelper.Close();

            Console.ReadKey();
        }
    }
}
