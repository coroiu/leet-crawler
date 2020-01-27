using System;


namespace Coroiu.Leet.Crawler.Test
{
    internal class MockUri : Uri
    {
        public MockUri(string path) : base($"http://{path}")
        {
        }
    }
}
