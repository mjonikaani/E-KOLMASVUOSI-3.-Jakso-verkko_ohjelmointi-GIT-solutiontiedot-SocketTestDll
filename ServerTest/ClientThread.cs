using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using SocketLibrary;

namespace ServerTest
{
    // tässä luokassa on asiakkaan palveleminen
    // omassa säikeessään (sivu 34)
    class ClientThread
    {
        private TcpClient client;

        private static int counter = 0;

        public ClientThread(TcpClient client)
        {
            this.client = client;
            //counter++;
            Interlocked.Increment(ref counter);
        }

        // ajetaan omassa säikeessään
        public void ServeClient()
        {
            // avataan yhteydet
            // avataan streamit
            SocketHelper socketHelper = new SocketHelper(client);
            socketHelper.Open();

            DateTime t = DateTime.Now;
            // luetaan ja käsitellään komennot
            bool ok = true;
            while (ok)
            {
                // TEHTÄVÄ: tee ohjelmaan seuraava muutos:
                // tämä säie lopetaan automaattisesti,
                // jos asiakkaalta ei ole tullut komentoa
                // viimeiseen 60 sekuntiin
                
                // katsotaan, onko uutta dataa
                if (socketHelper.DataAvailable())
                {
                    string komento = socketHelper.Read();
                    string vastaus = "";
                    // päivitetään aika
                    t = DateTime.Now;
                    switch (komento)
                    {
                        case COMMANDS.TIME:
                            vastaus = DateTime.Now.ToString();
                            break;
                        case COMMANDS.NUMBER_OF_REQUESTS:
                            vastaus = counter.ToString(); // TODO
                            break;

                        case COMMANDS.QUIT:
                            vastaus = "lopetus";
                            ok = false;
                            break;
                    }
                    socketHelper.Write(vastaus);
                    Console.WriteLine(vastaus);
                }
                else
                {
                    // kauanko aikaa on kulunut viimeisestä
                    // yhteyspyynnöstä?
                    TimeSpan sp = DateTime.Now - t;
                    if (sp.TotalSeconds > 60)
                    { // lopetaan tämän asiakkaan palveleminen
                        Console.WriteLine("auto stop");
                        ok = false;
                    }
                    Thread.Sleep(100);
                }
            }
            // suljetaan yhteydet
            socketHelper.Close();
        }
    }
}
