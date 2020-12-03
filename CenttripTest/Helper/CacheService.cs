using System;
using System.Runtime.Caching;

namespace Centtrip.Helper
{
    public class CacheService : ICacheService
    {
        /// <summary>
        /// Will check if there's anything by the name of cacheKey in the cache, and if there's not, it will call a delegate method.
        /// </summary>
        /// <typeparam name="T"> Entity type </typeparam>
        /// <param name="cacheKey"> Key of cached data in the memory </param>
        /// <param name="getItemCallback"> The delegate method to fetch data </param>
        /// <returns> List of chosen entity </returns>
        public T GetOrSet<T>(string cacheKey, Func<T> getItemCallback) where T : class
        {
            T item = MemoryCache.Default.Get(cacheKey) as T;
            if (item == null)
            {
                item = getItemCallback();
                MemoryCache.Default.Add(cacheKey, item, DateTime.Now.AddMinutes(10));
            }
            return item;
        }
    }
    public interface ICacheService
    {
        T GetOrSet<T>(string cacheKey, Func<T> getItemCallback) where T : class;
    }
}