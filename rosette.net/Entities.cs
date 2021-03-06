﻿using System.Collections.Generic;

namespace RosetteApi
{
    public class Entities : Endpoint
    {
        private RosetteFile _contentFile;

        public Entities(Api api) : base(api) {
           EndpointUri = "entities";
            Parameters = new Dictionary<string, object>();
            _contentFile = null;
        }

        public override RosetteResponse Run() {
            if (!Parameters.ContainsKey("content") && !Parameters.ContainsKey("contentUri")) {
                throw new RosetteException("Either content or contentUri must be provided");
            }

            return _contentFile == null ? Post() : Post(_contentFile);
        }

        public Entities Content(string content) {
            if (Parameters.ContainsKey("contentUri")) {
                throw new RosetteException("content and contentUri cannot both be defined");
            }
            Parameters["content"] = content;
            return this;
        }

        public Entities ContentUri(string contentUri) {
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
        /// <param name="options">(string, optional): Json string to add extra information</param>
        /// <returns>this</returns>
        public Entities ContentFromFile(string filename, string contentType = "text/plain") {
            _contentFile = new RosetteFile(filename, contentType);

            return this;
        }

        public Entities Language(string language) {
            Parameters["language"] = language;
            return this;
        }

        public Entities ContentType(string contentType) {
            Parameters["contentType"] = contentType;
            return this;
        }

        public Entities Genre(string genre) {
            Parameters["genre"] = genre;
            return this;
        }

        public Entities WithDictionary(Dictionary<string, object> dict) {
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
