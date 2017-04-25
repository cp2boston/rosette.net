using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RosetteApi {
    public class NameSimilarity : Endpoint {

        public NameSimilarity(Api api, RosetteName name1, RosetteName name2) : base(api) {
            Parameters["name1"] = name1;
            Parameters["name2"] = name2;
            EndpointUri = "name-similarity";

        }

        public NameSimilarity(Api api, string name1, string name2) : base(api) {
            Parameters = new Dictionary<string, object>();
            Parameters["name1"] = new RosetteName(name1);
            Parameters["name2"] = new RosetteName(name2);
            EndpointUri = "name-similarity";

        }

        public override RosetteResponse Run() {
            if (!Parameters.ContainsKey("name1") && !Parameters.ContainsKey("name2")) {
                throw new RosetteException("Both name1 and name2 must be provided either as RosetteName objects or as simple strings");
            }
            if (Parameters["name1"] == null || Parameters["name2"] == null) {
                throw new RosetteException("Neither name1 or name2 can be null");
            }
            return Post();
        }

        public NameSimilarity Name1(string text, string language = null, string script = null, string entityType = null) {
            Parameters["name1"] = new RosetteName(text, language, script, entityType);
            return this;
        }

        public NameSimilarity Name2(string text, string language = null, string script = null, string entityType = null) {
            Parameters["name2"] = new RosetteName(text, language, script, entityType);
            return this;
        }

        public NameSimilarity withDictionary(Dictionary<string, object> names) {
            Parameters = names;
            return this;
        }
    }
}
