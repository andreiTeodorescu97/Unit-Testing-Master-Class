using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using TestNinja.Mocking;
using TestNinja.Mocking.Mocks;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class VideoServiceTests
    {
        private VideoService _videoService;
        private Mock<IVideoRepository> _repo;
        private Mock<IFileReader> _fileReader;

        [SetUp]
        public void SetUp()
        {
            _fileReader = new Mock<IFileReader>();
            _repo = new Mock<IVideoRepository>();
            _videoService = new VideoService(_fileReader.Object, _repo.Object);
        }

        [Test]
        public void ReadVideoTitle_VideoIsNull_ReturnAnError()
        {
            _fileReader.Setup(fr => fr.Read("video.txt")).Returns("");

            var result = _videoService.ReadVideoTitle();

            Assert.That(result, Does.Contain("error").IgnoreCase);
        }

        [Test]
        public void GetUnprocessedVideosAsCsv_AllVideosAreProcessed_EmptyString()
        {
            //arrange
            _repo.Setup(r => r.GetUnprocessedVideos()).Returns(new List<Video>());

            //act
            var result = _videoService.GetUnprocessedVideosAsCsv();

            //assert
            Assert.That(result, Is.EqualTo(""));
        }

        [Test]
        public void GetUnprocessedVideosAsCsv_FewUnprocessedVideo_ConcatenadIds()
        {
            //arrange
            _repo.Setup(r => r.GetUnprocessedVideos()).Returns(new List<Video>
            {
                new Video{Id = 1},
                new Video{Id = 2},
                new Video{Id = 3},
            });

            //act
            var result = _videoService.GetUnprocessedVideosAsCsv();

            //assert
            Assert.That(result, Is.EqualTo("1,2,3"));
        }
    }
}

