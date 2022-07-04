using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;

namespace Sample.Api.Services
{
    public class DistributedCacheConsumerService : IService
    {
        private readonly IDistributedCache _distributedCache;

        public DistributedCacheConsumerService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task DoWorkAsync()
        {
            await _distributedCache.SetStringAsync("testando", "123");

            _distributedCache.Refresh("testea");

            await _distributedCache.GetAsync("testando");
        }
    }
}