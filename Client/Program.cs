using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rosette;

namespace Rosette {
    class Program {
        static void Main(string[] args) {
            
            var p = RosetteParameters.DocumentParameters("George Bush")
                .SetLanguage("eng");            

            var api = Rosette.callEndpoint(Rosette.Endpoint.Ping, env.API_KEY, p);
            
        }

        
    }
}
