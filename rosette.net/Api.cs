using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Http;

namespace RosetteApi
{
    public class Api {
        private Dictionary<string, string> _options;
        private Dictionary<string, string> _customHeaders;

        public string URI { get; private set; } = "https://api.rosette.com/rest/v1/";
        public string ApiKey {get; private set;}
        public int MaxRetries { get; private set; } = 1;
        public int SecondsBetweenRetries { get; private set; } = 5;
        public bool Debug { get; private set; } = false;
        public int Timeout { get; private set; } = 100;
        public HttpClient Client { get; private set; } = null;
        public int Concurrency { get; private set; } = ServicePointManager.DefaultConnectionLimit;
        public static string Version
        {
            get { return typeof(Api).Assembly.GetName().Version.ToString(); }
        }

        public Api(string apiKey) {
            ApiKey = apiKey ?? throw new RosetteException("Rosette API Key cannot be null");
            _options = new Dictionary<string, string>();
            _customHeaders = new Dictionary<string, string>();
        }

        public Api WithAltUrl(string url) {
            if (Uri.IsWellFormedUriString(url, UriKind.Absolute)) {
                URI = url.EndsWith("/") ? url : url + "/";
                return this;
            }
            else {
                throw new UriFormatException(string.Format("Invalid URI: {0}", url));
            }
        }

        public Api WithMaxRetries(int retries, int secondsBetweenRetries = 5) {
            MaxRetries = retries <= 0 ? 1 : retries;
            return this;
        }

        public Api WithDebug(bool on) {
            Debug = on;
            return this;
        }

        public Api WithTimeout(int seconds) {
            Timeout = seconds <= 0 ? 100 : seconds;
            return this;
        }

        public Api WithCustomHttpClient(HttpClient client) {
            Client = client;
            return this;
        }

        public Api WithConcurrency(int limit) {
            Concurrency = limit;
            return this;
        }

        public Api SetOption(string name, string value) {
            _options[name] = value;
            return this;
        }

        public Api RemoveOption(string name) {
            if (_options.ContainsKey(name)) {
                _options.Remove(name);
            }
            return this;
        }

        public string GetOption(string name) {
            return _options.ContainsKey(name) ? _options[name] : string.Empty;
        }

        public Dictionary<string, string> Options() {
            return _options;
        }

        public Api SetCustomHeader(string key, string value) {
            _customHeaders[key] = value;
            return this;
        }

        public Api RemoveCustomHeader(string key) {
            if (_customHeaders.ContainsKey(key)) {
                _customHeaders.Remove(key);
            }
            return this;
        }

        public string GetCustomHeader(string key) {
            return _customHeaders.ContainsKey(key) ? _customHeaders[key] : string.Empty;
        }

        /// <summary>SetupClient
        /// Sets up the HttpClient
        /// Uses the Client if one has been set. Otherwise creates a new one.
        /// </summary>
        /// <returns>HttpClient client to use to access the Rosette server.</returns>
        public HttpClient SetupClient() {
            if (Client == null) {
                Client =
                    new HttpClient(
                        new HttpClientHandler {
                            AutomaticDecompression = DecompressionMethods.GZip
                                                     | DecompressionMethods.Deflate
                        }) {
                        BaseAddress = new Uri(URI),
                        Timeout = Timeout != 0 ? TimeSpan.FromSeconds(Timeout) : TimeSpan.Zero
                    };
            }
            else {
                if (Client.BaseAddress == null) {
                    Client.BaseAddress = new Uri(URI);
                }
                if (Client.Timeout == TimeSpan.Zero) {
                    Client.Timeout = new TimeSpan(0, 0, Timeout);
                }
            }
            try {
                Client.DefaultRequestHeaders.Clear();
            }
            catch {
                // exception can be ignored
            }
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            Client.DefaultRequestHeaders.Add("X-RosetteAPI-Key", ApiKey);

            if (Debug) {
                Client.DefaultRequestHeaders.Add("X-RosetteAPI-Devel", "true");
            }

            Regex pattern = new Regex("^X-RosetteAPI-");
            if (_customHeaders.Count > 0) {
                foreach (KeyValuePair<string, string> entry in _customHeaders) {
                    Match match = pattern.Match(entry.Key);
                    if (match.Success) {
                        Client.DefaultRequestHeaders.Add(entry.Key, entry.Value);
                    }
                    else {
                        throw new RosetteException("Custom header name must begin with \"X-RosetteAPI-\"");
                    }

                }
            }
            Client.DefaultRequestHeaders.AcceptEncoding.Add(new System.Net.Http.Headers.StringWithQualityHeaderValue("gzip"));
            Client.DefaultRequestHeaders.AcceptEncoding.Add(new System.Net.Http.Headers.StringWithQualityHeaderValue("deflate"));
            if (!Client.DefaultRequestHeaders.Contains("User-Agent")) Client.DefaultRequestHeaders.Add("User-Agent", "Rosette.NET/" + Version);
            if (!Client.DefaultRequestHeaders.Contains("X-RosetteAPI-Binding")) Client.DefaultRequestHeaders.Add("X-RosetteAPI-Binding", "Rosette.NET");
            if (!Client.DefaultRequestHeaders.Contains("X-RosetteAPI-Binding-Version")) Client.DefaultRequestHeaders.Add("X-RosetteAPI-Binding-Version", Version);

            return Client;
        }

    }
}
