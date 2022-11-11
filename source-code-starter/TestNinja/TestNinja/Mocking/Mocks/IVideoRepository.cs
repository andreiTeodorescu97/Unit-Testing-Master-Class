using System.Collections.Generic;

namespace TestNinja.Mocking.Mocks
{
    public interface IVideoRepository
    {
        IEnumerable<Video> GetUnprocessedVideos();
    }
}