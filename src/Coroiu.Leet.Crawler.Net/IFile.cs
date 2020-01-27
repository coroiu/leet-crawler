namespace Coroiu.Leet.Crawler.Net
{
    public interface IFile : IResource
    {
        byte[] Content { get; }
    }
}
