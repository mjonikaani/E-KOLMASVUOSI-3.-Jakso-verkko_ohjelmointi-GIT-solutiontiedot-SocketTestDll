using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Sockets;
using System.Net;

namespace SocketLibrary
{
    public class COMMANDS
    {
        public const string TIME = "TIME";
        public const string QUIT = "QUIT";
        public const string NUMBER_OF_REQUESTS = "NUMBER_OF_REQUESTS";
    }

    public class SocketHelper
    {
        private TcpClient client;
        private NetworkStream ns;
        private StreamWriter sw;
        private StreamReader sr;

        public SocketHelper(TcpClient client)
        {
            this.client = client;
        }

        public void Open()
        {
            ns = client.GetStream();
            sw = new StreamWriter(ns);
            sr = new StreamReader(ns);

            sw.AutoFlush = true;
        }

        public void Close()
        {
            sw.Close();
            sr.Close();
            ns.Close();
            client.Close();
        }

        public void Write(string s)
        {
            sw.WriteLine(s);
        }

        public void Write(COMMANDS cmd)
        {
            sw.WriteLine(cmd.ToString());
        }

        public bool DataAvailable()
        {
            return ns.DataAvailable;
        }

        public string Read()
        {
            return sr.ReadLine();
        }

    }

}
