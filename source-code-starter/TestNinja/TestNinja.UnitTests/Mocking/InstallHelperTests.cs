using Moq;
using NUnit.Framework;
using System.Net;
using TestNinja.Mocking;
using TestNinja.Mocking.Mocks;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class InstallHelperTests
    {
        private Mock<IWebClientWrapper> _helper;
        private InstallerHelper _installer;

        [SetUp]
        public void SetuP()
        {
            _helper = new Mock<IWebClientWrapper>();
            _installer = new InstallerHelper(_helper.Object);
        }

        [Test]
        public void DownloadInstaller_WhenCalled_ThrowsError()
        {
            _helper.Setup(h => h.DownloadFile(It.IsAny<string>(), It.IsAny<string>())).Throws<WebException>();

            var result = _installer.DownloadInstaller("customer", "installer");
            Assert.That(result, Is.EqualTo(false));
            _helper.Verify(h => h.DownloadFile(It.IsAny<string>(), It.IsAny<string>()));
        }

        [Test]
        public void DownloadInstaller_WhenCalled_ReturnSucces()
        {
            var result = _installer.DownloadInstaller("customer", "installer");
            Assert.That(result, Is.EqualTo(true));
            _helper.Verify(h => h.DownloadFile(It.IsAny<string>(), It.IsAny<string>()));
        }
    }
}
