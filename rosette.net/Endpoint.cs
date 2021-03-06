﻿using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace RosetteApi {
    public abstract class Endpoint {
        private string _endpointUri;
        protected Dictionary<string, object> Parameters { get; set; }
        private RosetteFile _contentFile;


        protected Endpoint(Api api) {
            Parameters = new Dictionary<string, object>();
            API = api;
            API.SetupClient();
        }

        public abstract RosetteResponse Run();

        protected string EndpointUri
        {
            get { return _endpointUri; }
            set { _endpointUri = API.URI + value; }
        }

        protected Api API { get; private set; }

        protected RosetteResponse Get() {
            Task<HttpResponseMessage> task = Task.Run<HttpResponseMessage>(async () => await API.Client.GetAsync(EndpointUri));

            return new RosetteResponse(task.Result);
        }

        protected RosetteResponse Post() {
            if (API.Options().Any()) {
                Parameters["options"] = API.Options();
            }
            //string jsonRequest = new JavaScriptSerializer().Serialize(parameters);
            string jsonRequest = serialize(Parameters);
            HttpContent content = new StringContent(jsonRequest);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            Task<HttpResponseMessage> task = Task.Run<HttpResponseMessage>(async () => await API.Client.PostAsync(EndpointUri, content));

            return new RosetteResponse(task.Result);
        }

        protected RosetteResponse Post(RosetteFile file) {
            if (Parameters.Keys.Any()) {
                file = new RosetteFile(file.Filename, file.ContentType, serialize(Parameters));
            }

            Task<HttpResponseMessage> task = Task.Run<HttpResponseMessage>(async () => await API.Client.PostAsync(EndpointUri, file.AsMultipart()));

            return new RosetteResponse(task.Result);
        }

        private string serialize(Dictionary<string, object> dict) {
            return JsonConvert.SerializeObject(dict, new JsonSerializerSettings {
                //TypeNameHandling = TypeNameHandling.All,
                //TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple,
                NullValueHandling = NullValueHandling.Ignore
            });
        }
    }

}
