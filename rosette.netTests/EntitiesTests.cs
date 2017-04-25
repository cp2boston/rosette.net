using NUnit.Framework;
using RosetteApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RosetteApi.Tests {
    [TestFixture()]
    public class EntitiesTests {
        [Test()]
        public void EntitiesTest() {
            string apiKey = Environment.GetEnvironmentVariable("API_KEY");

            string entities_text_data = @"Bill Murray will appear in new Ghostbusters film: Dr. Peter Venkman was spotted filming a cameo in Boston this… http://dlvr.it/BnsFfS";
            Api api = new Api(apiKey);
            RosetteResponse response = new Entities(api).Content(entities_text_data).Run();
            System.Diagnostics.Debug.WriteLine(response.Headers);
            System.Diagnostics.Debug.WriteLine(response.Body);
        }
    }
}