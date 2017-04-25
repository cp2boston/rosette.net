using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RosetteApi {
    public class RosetteName {
        private Dictionary<string, string> _name;
        public RosetteName(string text, string language = null, string script = null, string entityType = null) {
            _name = new Dictionary<string, string>();
            _name["text"] = text;

            if (language != null) WithLanguage(language);
            if (script != null) WithScript(script);
            if (entityType != null) WithEntityType(entityType);
        }

        public RosetteName WithLanguage(string language) {
            _name["language"] = language;

            return this;
        }

        public RosetteName WithScript(string script) {
            _name["script"] = script;

            return this;
        }

        public RosetteName WithEntityType(string entityType) {
            _name["entityType"] = entityType;

            return this;
        }
        [JsonProperty("text")]
        public string Text { get { return _name["text"]; } }
        [JsonProperty("language")]
        public string Language { get { return _name.ContainsKey("language") ? _name["language"] : null; } }
        [JsonProperty("script")]
        public string Script { get { return _name.ContainsKey("script") ? _name["script"] : null; } }
        [JsonProperty("entityType")]
        public string EntityType { get { return _name.ContainsKey("entityType") ? _name["entityType"] : null; } }
    }
}
