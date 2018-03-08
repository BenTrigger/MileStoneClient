using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MileStoneClient.CommunicationLayer
{
    public sealed class CommunicationoMessage
    {
        private Guid _id;
        private string _userName;
        private DateTime _date;
        private string _messageContent;
        private string _groupId;

        public Guid Id
        {
            get
            {
                return _id;
            }

            set
            {
                _id = value;
            }
        }

        public string UserName
        {
            get
            {
                return _userName;
            }

            set
            {
                _userName = value;
            }
        }

        public DateTime Date
        {
            get
            {
                return _date;
            }

            set
            {
                _date = value;
            }
        }

        public string MessageContent
        {
            get
            {
                return _messageContent;
            }

            set
            {
                _messageContent = value;
            }
        }

        public string GroupID
        {
            get
            {
                return _groupId;
            }

            set
            {
                _groupId = value;
            }
        }

        public CommunicationoMessage(Guid id=new Guid(), string userName ="",long utcTime = 0, string messageContent = "", string groupId = "")
        {
            this._id = id;
            this._userName = userName;
            this._date = TimeFromUnixTimestamp(utcTime);
            this._messageContent = messageContent;
            this._groupId = groupId;
        }

        public override string ToString()
        {
            return String.Format("Message ID:{0}\n" +
                "UserName:{1}\n" +
                "DateTime:{2}\n" +
                "MessageContect:{3}\n" +
                "GroupId:{4}\n" 
                , Id,UserName,Date.ToShortDateString(),MessageContent,GroupID);
        }

        private static DateTime TimeFromUnixTimestamp(long unixTimestamp)
        {
            unixTimestamp /= 1000;
            DateTime unixYear0 = new DateTime(1970, 1, 1);
            long unixTimeStampInTicks = unixTimestamp * TimeSpan.TicksPerSecond;
            DateTime dtUnix = new DateTime(unixYear0.Ticks + unixTimeStampInTicks);
            return dtUnix;
        }

    }
}
