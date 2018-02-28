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
            CommunicationLayer.Communication con = new CommunicationLayer.Communication();
            Console.WriteLine( CommunicationLayer.Communication.Send("localhost", new CommunicationLayer.Message()));
            Console.ReadKey();
        }
    }
}
