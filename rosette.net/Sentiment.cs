using System.Collections.Generic;

namespace RosetteApi {
    public class Sentiment : Endpoint {
        private RosetteFile _contentFile;

        public Sentiment(Api api) : base(api) {
            EndpointUri = "sentiment";
            Parameters = new Dictionary<string, object>();
            _contentFile = null;
        }

        public override RosetteResponse Run() {
            if (_contentFile == null) {
                if (!Parameters.ContainsKey("content") && !Parameters.ContainsKey("contentUri")) {
                    throw new RosetteException("Either content or contentUri must be provided");
                }
                return Post();
            }
            else {
                return Post(_contentFile);
            }
        }

        public Sentiment Content(string content) {
            if (Parameters.ContainsKey("contentUri")) {
                throw new RosetteException("content and contentUri cannot both be defined");
            }
            Parameters["content"] = content;
            return this;
        }

        public Sentiment ContentUri(string contentUri) {
            if (Parameters.ContainsKey("content")) {
                throw new RosetteException("content and contentUri cannot both be defined");
            }
            Parameters["contentUri"] = contentUri;
            return this;
        }

        /// <summary>
        /// Loads the content from a file
        /// </summary>
        /// <param name="fileName">string: Path to the data file</param>
        /// <param name="contentType">(string, optional): Description of the content type of the data file. "text/plain" is used if unsure.</param>
        /// <returns>this</returns>
        public Sentiment ContentFromFile(string filename, string contentType = "text/plain") {
            _contentFile = new RosetteFile(filename, contentType);

            return this;
        }

        public Sentiment Language(string language) {
            Parameters["language"] = language;
            return this;
        }

        public Sentiment ContentType(string contentType) {
            Parameters["contentType"] = contentType;
            return this;
        }

        public Sentiment Genre(string genre) {
            Parameters["genre"] = genre;
            return this;
        }

        public Sentiment WithDictionary(Dictionary<string, object> dict) {
            Parameters = dict;

            if (!Parameters.ContainsKey("content") && !Parameters.ContainsKey("contentUri")) {
                throw new RosetteException("Either content or contentUri must be provided");
            }
            if (Parameters.ContainsKey("content") && Parameters.ContainsKey("contentUri")) {
                throw new RosetteException("content and contentUri cannot both be defined");
            }
            return this;
        }
    }
}
