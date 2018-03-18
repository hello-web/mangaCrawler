using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MangaCrawler.Crawler
{
    static class HttpDownloader
    {
        public static async Task<string> GetStringAsync(HttpMethod method, string address)
        {
            HttpResponseMessage httpResponse = await GetResponseAsync(method, address);

            if (httpResponse.StatusCode == HttpStatusCode.OK)
            {
                HttpContent content = httpResponse.Content;
                return await content.ReadAsStringAsync();
            }

            return null;
        }

        public static async Task<Stream> GetAsync(HttpMethod method, string address)
        {
            HttpResponseMessage httpResponse = await GetResponseAsync(method, address);

            if (httpResponse.StatusCode == HttpStatusCode.OK)
            {
                HttpContent content = httpResponse.Content;

                return await content.ReadAsStreamAsync();
            }

            return null;
        }

        public static async Task<HttpResponseMessage> GetResponseAsync(HttpMethod method, string address)
        {
            using (HttpClient client = new HttpClient())
            {
                Uri requestUri = new Uri(address);
                HttpRequestMessage httpRequest = new HttpRequestMessage(method, requestUri);
                HttpResponseMessage httpResponse = await client.SendAsync(httpRequest);

                return httpResponse;
            }
        }
    }
}
