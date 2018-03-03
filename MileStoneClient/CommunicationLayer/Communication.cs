using System;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Text;

namespace MileStoneClient.CommunicationLayer
{


    public class Communication
    {
        private class Request
        {
            public Guid messageGuid;
            public string userName;
            public long msgDate;
            public string messageContent;
            public string groupID;
            public string messageType;

            public Request(Message _msg, string _msgType)
            {
                this.messageType = _msgType;
                this.messageGuid = _msg.Id;
                this.userName = _msg.UserName;
                this.msgDate = _msg.Date.Ticks;
                this.messageContent = _msg.MessageContent;
                this.groupID = _msg.GroupID;
            }

        }

        public static Guid Send(string url,Message msg)
        {
            return SimpleHTTPClient.SendPostRequest(url,new Request(msg,"1"));
        }

        public static List<Message> GetTenMessages(string url)
        {
            List<Message> retVal = SimpleHTTPClient.GetListRequest(url, "2");
            return retVal;
        }

        private class SimpleHTTPClient
        {

            internal static Guid SendPostRequest(string url, Request item)
            {
                Request response = null;
                JObject jsonItem = JObject.FromObject(item);
                StringContent content = new StringContent(jsonItem.ToString()/*,Encoding.UTF8,"application/json"*/);
                using (var client = new HttpClient())
                {
                    var result = client.PostAsync(url, content).Result;
                    var responseContent = result?.Content?.ReadAsStringAsync().Result;
                    try
                    {
                        response = JsonConvert.DeserializeObject<Request>(responseContent, new JsonSerializerSettings
                        {
                            Error = delegate
                            {
                                throw new JsonException();
                            }
                        });
                    }
                    catch
                    {
                        throw new Exception();
                    }
                      
                    return response.messageGuid;
                }
            }

            internal static List<Message> GetListRequest(string url,string messageType)
            {
                List<Message> res = new List<Message>();
                JObject jsonItem = new JObject();
                jsonItem["messageType"] = messageType;
                StringContent content = new StringContent(jsonItem.ToString());
                using (var client = new HttpClient())
                {
                    var result = client.PostAsync(url, content).Result;
                    var responseContent = result?.Content?.ReadAsStringAsync().Result;
                    
                    jsonItem = new JObject(responseContent);
                    for (int i = 0; i < jsonItem.Count; i++)
                    {
                        res.Add(getMessage(jsonItem[i]));
                    }
                    return res;
                }
            }

            private static Message getMessage(JToken jToken)
            {
                return new Message
                {
                    Date = new DateTime(Convert.ToInt64(jToken["dateTime"])),
                    Id = new Guid(jToken["ID"].ToString()),
                    UserName = jToken["userName"].ToString(),
                    MessageContent = jToken["messageContent"].ToString(),
                    GroupID = jToken["groupID"].ToString()
                };
            }
        }
    }
}
