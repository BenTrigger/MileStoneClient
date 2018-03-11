using MileStoneClient.CommunicationLayer;
using System;
using System.Collections.Generic;

namespace MileStoneClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //example how to use unixTime and Ticks(DateTime)
            int timeUtc = (int)new DateTime(2005, 12, 1).ToFileTimeUtc();

            //Create your own Message and copy to CommunicationMessage
            CommunicationMessage msg = new CommunicationMessage(new Guid(), "Ben", timeUtc, "BenRh safd", "2");
            msg = Communication.Instance.Send("http://127.0.0.1:5452", msg);
            Console.WriteLine(msg); // same as Console.WriteLine(msg.ToString()); Override ToString 
            List<CommunicationMessage> msgList = Communication.Instance.GetTenMessages("http://127.0.0.1:5452");
            Console.WriteLine("Reuest 10 Last Messages:");
            foreach (CommunicationMessage msgItem in msgList)
            {
                Console.WriteLine(msgItem);
                Console.WriteLine("");
            }
            Console.ReadKey();
        }
    }
}
