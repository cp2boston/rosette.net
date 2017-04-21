using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;

namespace RosetteApi {
    public class Ping : Endpoint {
        public Ping(Api api) : base(api) {
            EndpointUri = "ping";
            API.SetupClient();
        }

        public override RosetteResponse Run() {
            return Get();
        }
    }
}
