﻿// WebAssetBundler - Bundles web assets so you dont have to.
// Copyright (C) 2012  Justin Arvay
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

namespace WebAssetBundler.Web.Mvc
{
    using System;
    using System.Globalization;
    using System.IO;
using System.Security.Cryptography;
using System.Text;

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

        /// <summary>
        /// Determine the number of occurances in the string.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int CountOccurance(this string instance, string value)
        {
            return instance.Split(new [] { value }, StringSplitOptions.None).Length - 1;
        }

        /// <summary>
        /// Check is the string is not not null or empty
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNotNullOrEmpty(this string value)
        {
            return !string.IsNullOrEmpty(value);
        }

        /// <summary>
        /// Replaces the first occurance of the search param with the replace param.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="search"></param>
        /// <param name="replace"></param>
        /// <returns></returns>
        public static string ReplaceFirst(this string text, string search, string replace)
        {
            int pos = text.IndexOf(search);
            if (pos < 0)
            {
                return text;
            }
            return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
        }

        /// <summary>
        /// Returns a new stream containing the contents of the string, using UTF-8 encoding.
        /// The stream's Position property is set to zero.
        /// </summary>
        /// <param name="s">The string to convert into a stream.</param>
        /// <returns>A new stream.</returns>
        public static Stream ToStream(this string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);

            writer.Write(s);
            writer.Flush();

            stream.Position = 0;
            return stream;
        }

        /// <summary>
        /// Converts the string to an md5 hash
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToHash(this string s)
        {
            MD5CryptoServiceProvider x = new MD5CryptoServiceProvider();
            byte[] bs = Encoding.UTF8.GetBytes(s);
            return x.ComputeHash(bs).ToHexString();
        }
    }
}
