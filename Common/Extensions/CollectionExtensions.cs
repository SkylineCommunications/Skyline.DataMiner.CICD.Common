namespace Skyline.DataMiner.CICD.Common.Extensions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    internal static class CollectionExtensions
    {
        /// <summary>
        /// Finds the index of the specified value.
        /// </summary>
        /// <typeparam name="T">The item type.</typeparam>
        /// <param name="source"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int IndexOf<T>(this IEnumerable<T> source, T value)
        {
            if (source is IList list)
            {
                return list.IndexOf(value);
            }

            if (source is IList<T> listT)
            {
                return listT.IndexOf(value);
            }

            int i = 0;
            foreach (T element in source)
            {
                if (Equals(element, value))
                {
                    return i;
                }

                i++;
            }

            return -1;
        }

        /// <summary>
        /// Checks if the specified value is part of the collection.
        /// </summary>
        /// <param name="source">Collection of strings.</param>
        /// <param name="value">Value to find.</param>
        /// <param name="comparison">String Comparison.</param>
        /// <returns>True if the value is found. Otherwise false.</returns>
        public static bool Contains(this IEnumerable<string> source, string value, StringComparison comparison)
        {
            foreach (string item in source)
            {
                if (String.Equals(item, value, comparison))
                {
                    return true;
                }
            }

            return false;
        }
    }
}