using NUnit.Framework;
using RosetteApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RosetteApi.Tests {
    [TestFixture()]
    public class PingTests {
        [Test()]
        public void PingTest() {
            string apiKey = Environment.GetEnvironmentVariable("API_KEY");
            Api api = new Api(apiKey);
            Assert.AreEqual(apiKey, api.ApiKey);
        }

        [Test()]
        public void RunTest() {
            string apiKey = Environment.GetEnvironmentVariable("API_KEY");
            Api api = new Api(apiKey);
            RosetteResponse response = new Ping(api).Run();

            System.Diagnostics.Debug.WriteLine(response.Headers);
            System.Diagnostics.Debug.WriteLine(response.Content);
            System.Diagnostics.Debug.WriteLine(response.ContentAsJson);
        }
    }
}