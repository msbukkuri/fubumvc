using System.Linq;
using FubuMVC.Core;
using FubuMVC.Core.Diagnostics.Runtime;
using FubuTestingSupport;
using NUnit.Framework;

namespace FubuMVC.Tests.Diagnostics.Runtime
{
    [TestFixture]
    public class RequestHistoryCacheTester
    {
        [Test]
        public void only_cache_up_to_the_setting_limit()
        {
            var settings = new DiagnosticsSettings{
                MaxRequests = 10
            };

            var cache = new RequestHistoryCache(settings);

            cache.Store(new RequestLog());
            cache.Store(new RequestLog());
            cache.Store(new RequestLog());
            cache.Store(new RequestLog());
            cache.Store(new RequestLog());
            cache.Store(new RequestLog());
            cache.Store(new RequestLog());
            cache.Store(new RequestLog());
            cache.Store(new RequestLog());

            cache.RecentReports().Count().ShouldBe(9);

            cache.Store(new RequestLog());
            cache.Store(new RequestLog());
            cache.Store(new RequestLog());
            cache.Store(new RequestLog());
            cache.Store(new RequestLog());

            cache.RecentReports().Count().ShouldBe(settings.MaxRequests);
        }
    }
}