using System.Net;
using TestNinja.Mocking.Mocks;

namespace TestNinja.Mocking
{
    public class InstallerHelper
    {
        private readonly IWebClientWrapper _webClientWrapper;
        private string _setupDestinationFile;

        public InstallerHelper(IWebClientWrapper webClientWrapper)
        {
            _webClientWrapper = webClientWrapper;
        }

        public bool DownloadInstaller(string customerName, string installerName)
        {
            try
            {
                _webClientWrapper.DownloadFile(
                    string.Format("http://example.com/{0}/{1}",
                        customerName,
                        installerName),
                    _setupDestinationFile);

                return true;
            }
            catch (WebException)
            {
                return false;
            }
        }
    }
}