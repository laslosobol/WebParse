using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SampleProject.Backend.ClientBase
{
    /// <summary>
    /// You can use this class to work with your site.If you dont need any of this functionality just remove\comment it.If you cant figure out how it works use any way you comfortable  with.
    ///PLEASE!!!!! NEVER use any ways what work synchronously and dont try to do it asynchronous with Task.Run(()=>)
    /// </summary>
    public class Request : RequestBase
    {
        public static async Task<T> ApiAction<T>(Uri uri, Method method, Dictionary<string, string> parameters,
              Dictionary<string, string> sHeader)
        {
            var request = new RestRequest(uri, method);
            foreach (var p in parameters)
                request.AddParameter(p.Key, p.Value);

            foreach (var v in sHeader.ToList())
                request.AddHeader(v.Key, v.Value);
            foreach (var v in HeadersDictionary.ToList())
                request.AddHeader(v.Key, v.Value);

            return await ApiAction<T>(request);
        }

        public static async Task<T> ApiAction<T>(Uri uri, Method method, object obj, Dictionary<string, string> sHeader)//method you need to use if you have to add body to request
        {
            var request = new RestRequest(uri, method);
            request.AddJsonBody(obj);//or you could use AddXMLBody or even AddBody

            foreach (var v in sHeader.ToList())
                request.AddHeader(v.Key, v.Value);
            foreach (var v in HeadersDictionary.ToList())
                request.AddHeader(v.Key, v.Value);

            return await ApiAction<T>(request);
        }

        private static async Task<T> ApiAction<T>(IRestRequest request)
        {
            if (Client == null)
            {
                Client = new RestClient(DestinationUrl)
                {
                    CookieContainer = CContainer,
                };
                Client.AddDefaultParameter("User-Agent", UserAgent, ParameterType.HttpHeader);
            }
            else
            {
                Client.CookieContainer = CContainer;
            }
            try
            {
                var response = await Client.ExecuteAsync(request);


                if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Created)
                {
                    var json = Encoding.UTF8.GetString(response.RawBytes, 0, response.RawBytes.Length);

                    if (json.Equals("{}"))
                        return default;
                    return JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });

                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }


            return default;
        }
    }
}
