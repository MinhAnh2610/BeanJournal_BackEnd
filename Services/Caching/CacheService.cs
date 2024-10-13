using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using ServiceContracts;
using Services.Caching.Options;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Caching
{
	public class CacheService : ICacheService
	{
		private static readonly ConcurrentDictionary<string, bool> _cacheKeys = new();
		private readonly IDistributedCache _distributedCache;
		public CacheService(IDistributedCache distributedCache)
		{
			_distributedCache = distributedCache;
		}
		public async Task<T?> GetAsync<T>(string key) where T : class
		{
			string? cachedValue = await _distributedCache.GetStringAsync(key);
			
			if (cachedValue is null) 
			{ 
				return null;
			}

			T? value = JsonConvert.DeserializeObject<T>(cachedValue);

			return value;
		}

		public async Task<T> GetAsync<T>(string key, Func<Task<T>> factory) where T : class
		{
			T? cachedValue = await GetAsync<T>(key);

			if (cachedValue is not null)
			{
				return cachedValue;
			}

			cachedValue = await factory();

			await SetAsync(key, cachedValue);

			return cachedValue;
		}

		public async Task RemoveAsync(string key)
		{
			await _distributedCache.RemoveAsync(key);

			_cacheKeys.TryRemove(key, out bool _);
		}

		public async Task RemoveByPrefixAsync(string prefixKey)
		{
			//foreach (string key in _cacheKeys.Keys)
			//{
			//	if (key.StartsWith(prefixKey))
			//	{
			//		await RemoveAsync(key);
			//	}
			//}

			IEnumerable<Task> tasks = _cacheKeys.Keys
				.Where(k => k.StartsWith(prefixKey))
				.Select(k => RemoveAsync(k));

			await Task.WhenAll(tasks);
		}

		public async Task SetAsync<T>(string key, T value) where T : class
		{
			string cacheValue = JsonConvert.SerializeObject(value);

			await _distributedCache.SetStringAsync(key, cacheValue, CacheOptions.DefaultExpiration);

			_cacheKeys.TryAdd(key, false);
		}
	}
}
