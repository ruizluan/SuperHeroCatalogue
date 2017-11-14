namespace SuperHeroCatalogue.Application.Interfaces
{
    using System;

    /// <summary>
    /// A distributed cache service.
    /// </summary>
    public interface IDistributedCacheService
    {
        /// <summary>
        /// Adds a new entry to the cache.
        /// </summary>
        /// <param name="key">The key of the entry.</param>
        /// <param name="value">The value.</param>
        /// <param name="expriationTime">The expriation time.</param>
        void AddEntry(string key, object value, TimeSpan expriationTime);

        /// <summary>
        /// Gets an entry from the cache.
        /// </summary>
        /// <param name="key">The key of the value to retrieve.</param>
        /// <returns>The value stored with the given key.</returns>
        object GetEntry(string key);

        /// <summary>
        /// Clears the entry with the given key.
        /// </summary>
        /// <param name="key">The key to be cleared.</param>
        void ClearEntry(string key);
    }
}