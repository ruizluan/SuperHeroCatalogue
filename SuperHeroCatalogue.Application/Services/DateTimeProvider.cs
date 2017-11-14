using SuperHeroCatalogue.Application.Interfaces;

namespace SuperHeroCatalogue.Application.Services
{
    using System;

    /// <inheritdoc />
    /// <summary>
    /// The date time provider.
    /// </summary>
    public class DateTimeProvider : IDateTimeProvider
    {
        /// <inheritdoc />
        /// <summary>
        /// Gets the current date time.
        /// </summary>
        /// <value>The current date time.</value>
        public DateTime Now => DateTime.Now;
    }
}