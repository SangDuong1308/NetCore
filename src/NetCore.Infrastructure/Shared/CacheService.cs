﻿using NetCore.Application.Customer.Shared;
using NetCore.Application.Shared;
using NetCore.Infrastructure.Settings;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace NetCore.Infrastructure.Shared
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IOptions<AppSettings> _appSettings;

        //TODO: Add expiration time

        public CacheService(IDistributedCache distributedCache, IOptions<AppSettings> appSettings)
        {
            _distributedCache = distributedCache;
            _appSettings = appSettings;
        }
        public async Task<T?> GetAsync<T>(string key)
        {
            var cachedValue = await _distributedCache.GetStringAsync(key);
            if(string.IsNullOrEmpty(cachedValue))
            {
                return default(T?);
            }
            var customerDto = JsonConvert.DeserializeObject<T>(cachedValue);
            return customerDto;
        }

        public async Task SetAsync<T>(string key, T value, int expirationTimeSeconds = default)
        {
            if(expirationTimeSeconds == default)
            {
                expirationTimeSeconds = _appSettings.Value.Cache.ExpirationTimeSeconds;
            }
            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(expirationTimeSeconds)
            };
            await _distributedCache.SetStringAsync(key, JsonConvert.SerializeObject(value), cacheOptions);
        }

        public async Task RemoveAsync(string key)
            => await _distributedCache.RemoveAsync(key);

        public async Task ReplaceAsync<T>(string key, T value)
        {
            await RemoveAsync(key);
            await SetAsync(key, value);
        }
    }
}
