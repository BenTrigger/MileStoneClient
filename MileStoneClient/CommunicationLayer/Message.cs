using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MileStoneClient.CommunicationLayer
{
    class Message
    {
        private Guid _id;
        private string _userName;
        private DateTime _date;
        private string _messageContent;

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

        public Message(Guid id, string userName, DateTime date, string messageContent)
        {
            this._id = id;
            this._userName = userName;
            this._date = date;
            this._messageContent = messageContent;
        }

         
    }
}
