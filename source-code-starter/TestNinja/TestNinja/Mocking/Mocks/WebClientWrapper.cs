using System.Net;

namespace TestNinja.Mocking.Mocks
{
    public class WebClientWrapper : IWebClientWrapper
    {
        public void DownloadFile(string url, string path)
        {
            var client = new WebClient();
            client.DownloadFile(url,path);
        }
    }
}
