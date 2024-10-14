using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Caching.Options
{
	public static class CacheOptions
	{
		public static DistributedCacheEntryOptions DefaultExpiration => new DistributedCacheEntryOptions()
		{
			AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
		};
	}
}
