using Newtonsoft.Json.Linq;
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
            //example how to use unixTime and Ticks(DateTime)
            int timeUtc = (int)new DateTime(2005, 12, 1).ToFileTimeUtc();
            Console.WriteLine(timeUtc);

            CommunicationLayer.CommunicationoMessage msg = new CommunicationLayer.CommunicationoMessage(new Guid(), "Ben", timeUtc, "BenRh safd", "2");
            msg = CommunicationLayer.Communication.Instance.Send("http://127.0.0.1:5452", msg);
            Console.WriteLine(msg.ToString());
            List<CommunicationLayer.CommunicationoMessage> msgList;
            msgList = CommunicationLayer.Communication.Instance.GetTenMessages("http://127.0.0.1:5452");
            Console.WriteLine("Reuest 10 Last Messages:");
            foreach (CommunicationLayer.CommunicationoMessage msgItem in msgList)
            {
                Console.WriteLine(msgItem.ToString());
                Console.WriteLine("");
            }
            Console.ReadKey();
        }
    }
}
