namespace SuperHeroCatalogue.Application.Interfaces
{
    using System;

    /// <summary>
    /// A provider for the current date time.
    /// </summary>
    public interface IDateTimeProvider
    {
        /// <summary>
        /// Gets the current date time.
        /// </summary>
        /// <value>The current date time.</value>
        DateTime Now { get; }
    }
}