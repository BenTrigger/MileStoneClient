using System;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Text;

namespace MileStoneClient.CommunicationLayer
{

    public sealed class Communication // singleTon
    { //sealed modifier prevents other classes from inheriting from it
        private static Communication instance = null;
        private static readonly object padlock = new object();

        //private constructor for singleton
        private Communication()
        {

        }

        public static Communication Instance
        {
            get
            {   //only if there is no instance lock object, otherwise return instance
                if (instance == null)
                {
                    lock (padlock) // senario: n threads in here,
                    {              //locking the first and others going to sleep till the first get new Instance
                        if (instance == null)  // rest n-1 threads no need new instance because its not null anymore.
                        {
                            instance = new Communication();
                        }
                    }
                }
                return instance;
            }
        }

        /// <summary>
        /// inner class that represent the request object from server
        /// </summary>
        private class Request
        {
            public Guid messageGuid;
            public string userName;
            public long msgDate;
            public string messageContent;
            public string groupID;
            public string messageType;

            public Request(CommunicationoMessage _msg, string _msgType)
            {
                this.messageType = _msgType;
                this.messageGuid = _msg.Id;
                this.userName = _msg.UserName;
                this.msgDate = _msg.Date.Ticks;
                this.messageContent = _msg.MessageContent;
                this.groupID = _msg.GroupID;
            }

        }

        /// <summary>
        /// Send method: send request to server with HttpClient and returned updated guid of currect message
        /// </summary>
        /// <param name="url">url of the server</param>
        /// <param name="msg">CommunicationoMessage message content</param>
        /// <returns>Guid from server back to client.</returns>
        public CommunicationoMessage Send(string url, CommunicationoMessage msg)
        {
            return SimpleHTTPClient.SendPostRequest(url, new Request(msg, "1"));
        }

        /// <summary>
        /// GetTenMessages method: send request to server with HttpClient and returned list of last ten messages
        /// </summary>
        /// <param name="url">url of the server</param>
        /// <returns>List of last ten CommunicationoMessage</returns>
        public List<CommunicationoMessage> GetTenMessages(string url)
        {
            List<CommunicationoMessage> retVal = SimpleHTTPClient.GetListRequest(url, "2");
            return retVal;
        }

        //inner class that represent the Http Client request/response
        private class SimpleHTTPClient
        {

            internal static CommunicationoMessage SendPostRequest(string url, Request item)
            {
                JObject jsonItem = JObject.FromObject(item);
                StringContent content = new StringContent(jsonItem.ToString());
                using (var client = new HttpClient())
                {
                    var result = client.PostAsync(url, content).Result;
                    var responseContent = result?.Content?.ReadAsStringAsync().Result;
                    return getMessage(JObject.Parse(responseContent));
                }
            }

            internal static List<CommunicationoMessage> GetListRequest(string url, string messageType)
            {
                List<CommunicationoMessage> res = new List<CommunicationoMessage>();
                JObject jsonItem = new JObject();
                JArray jsonArr = new JArray();
                jsonItem["messageType"] = messageType;
                StringContent content = new StringContent(jsonItem.ToString());
                using (var client = new HttpClient())
                {
                    var result = client.PostAsync(url, content).Result;
                    var responseContent = result?.Content?.ReadAsStringAsync().Result;

                    jsonArr = JArray.Parse(responseContent);
                    for (int i = 0; i < jsonArr.Count; i++)
                    {
                        res.Add(getMessage(jsonArr[i]));
                    }
                    return res;
                }
            }

            private static CommunicationoMessage getMessage(JToken jToken)
            {
                return new CommunicationoMessage(
                    new Guid(jToken["messageGuid"].ToString()),
                    jToken["userName"].ToString(),
                    Convert.ToInt64(jToken["msgDate"]),
                    jToken["messageContent"].ToString(),
                    jToken["groupID"].ToString()
                );
            }


        }
    }
}
