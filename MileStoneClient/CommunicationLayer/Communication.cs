using System;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace MileStoneClient.CommunicationLayer
{


    public class Communication
    {
        private class Request
        {

            public Message _msg;
            public string _msgType;

            public Request(Message _msg, string _msgType)
            {
                this._msg = _msg;
                this._msgType = _msgType;
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
                StringContent content = new StringContent(jsonItem.ToString());
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
                      
                    return response._msg.Id;
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
