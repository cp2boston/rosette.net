using NUnit.Framework;
using RosetteApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RosetteApi.Tests {
    [TestFixture()]
    public class NameSimilarityTests {
        [Test()]
        public void NameSimilarityTest() {
            string apiKey = Environment.GetEnvironmentVariable("API_KEY");
            Api api = new Api(apiKey);

            string matched_name_data1 = @"Michael Jackson";
            string matched_name_data2 = @"迈克尔·杰克逊";

            RosetteResponse response = new NameSimilarity(api, 
                new RosetteName(matched_name_data1, "eng", null, "PERSON"),
                new RosetteName(matched_name_data2, null, null, "PERSON"))
                .Run();

            System.Diagnostics.Debug.WriteLine(response.Headers);
            System.Diagnostics.Debug.WriteLine(response.Body);
        }
    }
}