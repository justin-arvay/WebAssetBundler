namespace ResourceCompiler.Extensions
{
    using System;
    using System.Globalization;

    public static class StringExtensions
    {
        /// <summary>
        /// Determines whether this instance and another specified System.String object have the same value.
        /// </summary>
        /// <param name="instance">The string to check equality.</param>
        /// <param name="comparing">The comparing with string.</param>
        /// <returns>
        /// <c>true</c> if the value of the comparing parameter is the same as this string; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsCaseSensitiveEqual(this string instance, string comparing)
        {
            return string.CompareOrdinal(instance, comparing) == 0;
        }

        /// <summary>
        /// Determines whether this instance and another specified System.String object have the same value.
        /// </summary>
        /// <param name="instance">The string to check equality.</param>
        /// <param name="comparing">The comparing with string.</param>
        /// <returns>
        /// <c>true</c> if the value of the comparing parameter is the same as this string; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsCaseInsensitiveEqual(this string instance, string comparing)
        {
            return string.Compare(instance, comparing, StringComparison.OrdinalIgnoreCase) == 0;
        }

        /// <summary>
        /// Replaces the format item in a specified System.String with the text equivalent of the value of a corresponding System.Object instance in a specified array.
        /// </summary>
        /// <param name="instance">A string to format.</param>
        /// <param name="args">An System.Object array containing zero or more objects to format.</param>
        /// <returns>A copy of format in which the format items have been replaced by the System.String equivalent of the corresponding instances of System.Object in args.</returns>
        public static string FormatWith(this string instance, params object[] args)
        {
            return string.Format(CultureInfo.CurrentCulture, instance, args);
        }
    }
}
