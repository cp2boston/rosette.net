using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rosette;

namespace Rosette {
    class Program {
        static void Main(string[] args) {

            string apiKey = Environment.GetEnvironmentVariable("API_KEY");
            
            var p = DocumentParameters.Create("George Bush")
                .SetLanguage("eng");            

            var api = Rosette.callEndpoint(Rosette.Endpoint.Ping, apiKey, p);

            var result = api.Execute;

            api = Rosette.callEndpoint(Rosette.Endpoint.Entities, apiKey, p);

            result = api.Execute;
        }

        
    }
}
