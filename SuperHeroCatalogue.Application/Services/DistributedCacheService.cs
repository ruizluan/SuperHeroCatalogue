using SuperHeroCatalogue.Application.Interfaces;

namespace SuperHeroCatalogue.Application.Services
{
    using System;
    using System.Collections.Generic;

    /// <inheritdoc />
    /// <summary>
    /// The distributed cache service.
    /// </summary>
    public class DistributedCacheService : IDistributedCacheService
    {
        /// <summary>
        /// The date time provider.
        /// </summary>
        private readonly IDateTimeProvider _dateTimeProvider;

        /// <summary>
        /// The cache entries.
        /// </summary>
        private readonly Dictionary<string, CacheEntry> _cacheEntries = new Dictionary<string, CacheEntry>();

        /// <summary>
        /// Initializes a new instance of the <see cref="DistributedCacheService"/> class.
        /// </summary>
        /// <param name="dateTimeProvider">The date time provider.</param>
        public DistributedCacheService(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;
        }

        /// <inheritdoc />
        /// <summary>
        /// Adds a new entry to the cache.
        /// </summary>
        /// <param name="key">The key of the entry.</param>
        /// <param name="value">The value.</param>
        /// <param name="expriationTime">The expriation time.</param>
        public void AddEntry(string key, object value, TimeSpan expriationTime)
        {
            _cacheEntries[key] = new CacheEntry(_dateTimeProvider.Now.Add(expriationTime), value);
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets an entry from the cache.
        /// </summary>
        /// <param name="key">The key of the value to retrieve.</param>
        /// <returns>The value stored with the given key.</returns>
        public object GetEntry(string key)
        {
            CacheEntry cacheEntry;

            if (!_cacheEntries.TryGetValue(key, out cacheEntry)) return null;

            return _dateTimeProvider.Now < cacheEntry.ExpriationTime ? cacheEntry.Value : null;
        }

        /// <inheritdoc />
        /// <summary>
        /// Clears the entry with the given key.
        /// </summary>
        /// <param name="key">The key to be cleared.</param>
        public void ClearEntry(string key)
        {
            _cacheEntries.Remove(key);
        }

        /// <summary>
        /// A cache entry.
        /// </summary>
        private class CacheEntry
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CacheEntry"/> class.
            /// </summary>
            /// <param name="expriationTime">The expriation time.</param>
            /// <param name="value">The value to be cached.</param>
            public CacheEntry(DateTime expriationTime, object value)
            {
                ExpriationTime = expriationTime;
                Value = value;
            }

            /// <summary>
            /// Gets the expriation time.
            /// </summary>
            /// <value>The expriation time.</value>
            public DateTime ExpriationTime { get; }

            /// <summary>
            /// Gets the cached value.
            /// </summary>
            /// <value>The cached value.</value>
            public object Value { get; }
        }
    }
}