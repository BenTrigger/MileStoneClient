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

        public CommunicationoMessage(Guid id=new Guid(), string userName ="", DateTime date= new DateTime(), string messageContent ="",string groupId ="")
        {
            this._id = id;
            this._userName = userName;
            this._date = date;
            this._messageContent = messageContent;
            this._groupId = groupId;
        }

         
    }
}
