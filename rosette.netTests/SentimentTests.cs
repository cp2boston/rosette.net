using NUnit.Framework;
using System;
using System.IO;


namespace RosetteApi.Tests {
    [TestFixture()]
    public class SentimentTests {
        [Test()]
        public void SentimentWithFileTest() {
            string apiKey = Environment.GetEnvironmentVariable("API_KEY");
            Api api = new Api(apiKey);

            var newFile = Path.GetTempFileName();
            StreamWriter sw = new StreamWriter(newFile);
            string sentiment_file_data = @"<html><head><title>New Ghostbusters Film</title></head><body><p>Original Ghostbuster Dan Aykroyd, who also co-wrote the 1984 Ghostbusters film, couldn’t be more pleased with the new all-female Ghostbusters cast, telling The Hollywood Reporter, “The Aykroyd family is delighted by this inheritance of the Ghostbusters torch by these most magnificent women in comedy.”</p></body></html>";
            sw.WriteLine(sentiment_file_data);
            sw.Flush();
            sw.Close();

            RosetteResponse response = new Sentiment(api).ContentFromFile(newFile).Language("eng").Run();
            System.Diagnostics.Debug.WriteLine(response.Headers);
            System.Diagnostics.Debug.WriteLine(response.Body);
        }
    }
}