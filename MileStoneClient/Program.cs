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
            //CommunicationLayer.Communication con = new CommunicationLayer.Communication("localhost");
            Console.WriteLine( CommunicationLayer.Communication.Send("http://127.0.0.1:80", new CommunicationLayer.Message()));
            Console.WriteLine(CommunicationLayer.Communication.GetTenMessages("http://127.0.0.1:80"));

            Console.ReadKey();
        }
    }
}
