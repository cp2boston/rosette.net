using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace RosetteApi
{
    public class Entities : Endpoint
    {
        private Dictionary<string, object> _parameters;

        public Entities(Api api) : base(api) {
           EndpointUri = "entities";
            _parameters = new Dictionary<string, object>();
        }

        public override RosetteResponse Run() {
            if (!_parameters.ContainsKey("content") && !_parameters.ContainsKey("contentUri")) {
                throw new RosetteException("Either content or contentUri must be provided");
            }

            return Post(_parameters);
        }

        public Entities Content(string content) {
            if (_parameters.ContainsKey("contentUri")) {
                throw new RosetteException("content and contentUri cannot both be defined");
            }
            _parameters["content"] = content;
            return this;
        }

        public Entities ContentUri(string contentUri) {
            if (_parameters.ContainsKey("content")) {
                throw new RosetteException("content and contentUri cannot both be defined");
            }
            _parameters["contentUri"] = contentUri;
            return this;
        }

        public Entities Language(string language) {
            _parameters["language"] = language;
            return this;
        }

        public Entities ContentType(string contentType) {
            _parameters["contentType"] = contentType;
            return this;
        }

        public Entities Genre(string genre) {
            _parameters["genre"] = genre;
            return this;
        }

        public Entities WithDictionary(Dictionary<string, object> dict) {
            _parameters = dict;

            if (!_parameters.ContainsKey("content") && !_parameters.ContainsKey("contentUri")) {
                throw new RosetteException("Either content or contentUri must be provided");
            }
            if (_parameters.ContainsKey("content") && _parameters.ContainsKey("contentUri")) {
                throw new RosetteException("content and contentUri cannot both be defined");
            }
            return this;
        }
    }
}
