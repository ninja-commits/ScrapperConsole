using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Scrapper.Configuration.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Scrapper.Services.Scrapp
{
    public class ScrapperService : IScrapperService
    {
        private readonly Position _options;

        public ScrapperService(IOptions<Position> options)
        {
            _options = options.Value;
        }

        public async Task RunAsync()
        {
            Console.WriteLine("START SCRAPPING");
            Console.WriteLine(_options.Name);

            await GetHtmlAsync();
        }

        public async Task GetHtmlAsync()
        {
            var url = "https://www.seloger.com/list.htm?projects=2,5&types=2,1&natures=1,2,4&places=[{%22divisions%22:[2238]}]&price=NaN/10000000&mandatorycommodities=0&enterprise=0&qsVersion=1.0&m=search_refine";

            using var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url));
            request.Headers.TryAddWithoutValidation("Host", "www.seloger.com");
            request.Headers.TryAddWithoutValidation("Connection", "keep-alive");
            request.Headers.TryAddWithoutValidation("sec-ch-ua", "\"Chromium\";v=\"94\", \"Google Chrome\";v=\"94\", \"; Not A Brand\";v=\"99");
            request.Headers.TryAddWithoutValidation("sec-ch-ua-platform", "\"windows\"");
            request.Headers.TryAddWithoutValidation("Accept", "*/*");
            request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/94.0.4606.81 Safari/537.36");
            request.Headers.TryAddWithoutValidation("Sec-Fetch-Site", "same-origin");
            request.Headers.TryAddWithoutValidation("Sec-Fetch-Mode", "no-cors");
            request.Headers.TryAddWithoutValidation("Sec-Fetch-Dest", "script");
            request.Headers.TryAddWithoutValidation("Sec-Fetch-Dest", "script");
            request.Headers.TryAddWithoutValidation("Referer", $"{url}");
            request.Headers.TryAddWithoutValidation("Accept-Language", "en,fr-FR;q=0.9,fr;q=0.8,en-US;q=0.7");
            request.Headers.TryAddWithoutValidation("Cookie", "sl-pdd_LD_UserId=f90d6e26-2c46-491d-a1d6-3dc693f71764; visitId=1616528147277-585779176; AMCV_366134FA53DB27860A490D44%40AdobeOrg=1099438348%7CMCIDTS%7C18710%7CMCMID%7C00389641543056194328880888804672304944%7CMCAID%7CNONE%7CMCOPTOUT-1616535347s%7CNONE%7CvVersion%7C2.1.0; stack_ch=%5B%5B%27Acces%2520Direct%27%2C%271616528147537%27%5D%5D; didomi_token=eyJ1c2VyX2lkIjoiMTc4NjA5NWYtY2JkNy02NWY2LWI1N2MtYzcyMjk5M2Y5NDllIiwiY3JlYXRlZCI6IjIwMjEtMTAtMjNUMTY6MTY6NDIuNzkxWiIsInVwZGF0ZWQiOiIyMDIxLTEwLTIzVDE2OjE2OjQyLjc5MVoiLCJ2ZW5kb3JzIjp7ImVuYWJsZWQiOlsiZ29vZ2xlIiwiYzpmaXJlYmFzZS03OGROS0NGOSIsImM6YWNjZW5nYWdlLUU3TGhSTEhRIiwiYzpvbW5pdHVyZS1hZG9iZS1hbmFseXRpY3MiLCJjOmFjY2VuZ2FnZSIsImM6aGFydmVzdC1QVlRUdFVQOCIsImM6c25vd3Bsb3ctTHJScWg5cUoiLCJjOmJhdGNoLUhGUEZGRk5jIiwiYzphcHBzZmx5ZXItQnlod2VWY2IiLCJjOmFpcnNoaXAtalEydGJpSmUiLCJjOmxhdW5jaGRhci04cWE4UWp0NyJdfSwicHVycG9zZXMiOnsiZW5hYmxlZCI6WyJhbmFseXNlZGUtVkRUVVVobjYiLCJwdXJwb3NlX2FuYWx5dGljcyIsImRldmljZV9jaGFyYWN0ZXJpc3RpY3MiLCJnZW9sb2NhdGlvbl9kYXRhIl19LCJ2ZW5kb3JzX2xpIjp7ImVuYWJsZWQiOlsiZ29vZ2xlIiwiYzpsYXVuY2hkYXItOHFhOFFqdDciXX0sInB1cnBvc2VzX2xpIjp7ImVuYWJsZWQiOlsiYW5hbHlzZWRlLVZEVFVVaG42Il19LCJ2ZXJzaW9uIjoyLCJhYyI6IkFrdUFFQUZrQkpZQS5Ba3VBQ0FrcyJ9; euconsent-v2=CPOihYsPOihYsAHABBENBxCsAP_AAH_AAAAAISNf_X__b3_j-_59f_t0eY1P9_7_v-0zjhfdt-8N2f_X_L8X42M7vF36pq4KuR4Eu3LBIQdlHOHcTUmw6okVrzPsbk2Mr7NKJ7PEmnMbO2dYGH9_n93TuZKY7__8___z__-v_v____f_7-3_3__5_X---_e_V399zLv9____39nN___9v-_BCEAkw1LyALsSxwZNo0qhRAjCsJDoBQAUUAwtEVhAyuCnZXAR6ghYAITUBGBECDEFGDAIABAIAkIiAkAPBAIgCIBAACAFSAhAARsAgsALAwCAAUA0LECKAIQJCDI4KjlMCAiRaKCeysASg72NMIQyywAoFH9FRgIlCCBYGQkLBzHAEgJcLJAA.f_gAD_gAAAAA; _gcl_au=1.1.1781181880.1635005803; abtest_consent=1; _dd_s=logs=1&id=19b01d04-b852-45c9-9b3e-2a5b811b7d00&created=1635021426123&expire=1635022892335; datadome=EMWo2an69Iv.XekcAAcCf.tv0TUJSwv8eJcP~gqDO4PV6shhiOSrNDS15zCP0KjqpaTRxOkP~x~K4InqOMm~3LlakMXQ6nLIW3GTfXi~mR");

            var clientHandler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate };
            var httpClient = new HttpClient(clientHandler);
            var response = await httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();
            var responseStream = await response.Content.ReadAsStringAsync();

            Console.WriteLine(responseStream);

            //var t = ReadFully(responseStream);
            //var y = Decompress(t);

            //using (var ms = new MemoryStream(y))
            //using (var streamReader = new StreamReader(ms))
            //using (var jsonReader = new JsonTextReader(streamReader))
            //{
            //    var jOBj = (JObject)JToken.ReadFrom(jsonReader);
            //}


            //using var decompressedStream = new GZipStream(responseStream, CompressionMode.Decompress);
            //using (var streamReader = new StreamReader(decompressedStream))
            //{
            //    string responseBody =  await streamReader.ReadToEndAsync().ConfigureAwait(false);
            //    Console.WriteLine(responseBody);
            //}
        }

        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        public static byte[] Decompress(byte[] data)
        {
            using (var compressedStream = new MemoryStream(data))
            using (var zipStream = new GZipStream(compressedStream, CompressionMode.Decompress))
            using (var resultStream = new MemoryStream())
            {
                zipStream.CopyTo(resultStream);
                return resultStream.ToArray();
            }
        }

    }
}
