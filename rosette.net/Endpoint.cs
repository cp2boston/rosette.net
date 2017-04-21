using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Script.Serialization;
using System.Threading.Tasks;


namespace RosetteApi {
    public abstract class Endpoint {
        private string _endpointUri;

        protected Endpoint(Api api) {
            API = api;
        }

        public abstract RosetteResponse Run();

        protected string EndpointUri {
            get { return _endpointUri; }
            set { _endpointUri = API.URI + value; }
        }

        protected Api API{ get; private set; }

        protected RosetteResponse Get() {
            Task<HttpResponseMessage> task = Task.Run<HttpResponseMessage>(async () => await API.Client.GetAsync(EndpointUri));

            return new RosetteResponse(task.Result);
        }

        protected RosetteResponse Post(Dictionary<string, object> parameters) {
            if (API.Options().Any()) {
                parameters["options"] = API.Options();
            }
            string jsonRequest = new JavaScriptSerializer().Serialize(parameters);
            HttpContent content = new StringContent(jsonRequest);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            Task<HttpResponseMessage> task = Task.Run<HttpResponseMessage>(async () => await API.Client.PostAsync(EndpointUri, content));

            return new RosetteResponse(task.Result);
        }

    }
}
