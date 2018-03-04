using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MileStoneClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine( CommunicationLayer.Communication.Instance.Send("http://127.0.0.1:80", new CommunicationLayer.CommunicationoMessage()));
            Console.WriteLine(CommunicationLayer.Communication.Instance.GetTenMessages("http://127.0.0.1:80"));

            Console.ReadKey();
        }
    }
}
