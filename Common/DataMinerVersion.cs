namespace Skyline.DataMiner.CICD.Common
{
    using System;

    /// <summary>
    /// Represents a DataMiner version.
    /// </summary>
    public sealed class DataMinerVersion : IComparable, IComparable<DataMinerVersion>, IEquatable<DataMinerVersion>
    {
        static DataMinerVersion()
        {
            Version_0_0_0 = new DataMinerVersion(0, 0, 0);
            
            MinSupportedVersionForNuGet = new DataMinerVersion(10, 0, 10);
            MinSupportedVersionForDmapp = new DataMinerVersion(new Version(10, 0, 9, 0), 9312);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataMinerVersion"/> class.
        /// </summary>
        /// <param name="version">The version.</param>
        /// <param name="iteration">The iteration or build number.</param>
        public DataMinerVersion(Version version, uint iteration)
        {
            Version = version;
            Iteration = iteration;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataMinerVersion"/> class.
        /// </summary>
        /// <param name="version">The version.</param>
        public DataMinerVersion(Version version)
        {
            Version = version;
            Iteration = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataMinerVersion"/> class.
        /// </summary>
        /// <param name="major">The major number.</param>
        /// <param name="minor">The minor number.</param>
        /// <param name="build">The build number.</param>
        public DataMinerVersion(int major, int minor, int build)
        {
            Version = new Version(major, minor, build);
            Iteration = 0;
        }

        /// <summary>
        /// Represents a "null" version.
        /// </summary>
        public static DataMinerVersion Version_0_0_0 { get; }
        
        /// <summary>
        /// Gets the minimum supported DataMiner version that supports the use of NuGet packages.
        /// </summary>
        /// <value>The minimum supported DataMiner version that supports the use of NuGet packages.</value>
        public static DataMinerVersion MinSupportedVersionForNuGet { get; }

        /// <summary>
        /// Gets the minimum supported DataMiner version that supports app packages.
        /// </summary>
        /// <value>The minimum supported DataMiner version that supports app packages.</value>
        public static DataMinerVersion MinSupportedVersionForDmapp { get; }

        /// <summary>
        /// Gets the version.
        /// </summary>
        /// <value>The version.</value>
        public Version Version { get; }

        /// <summary>
        /// Gets the iteration.
        /// </summary>
        /// <value>The iteration.</value>
        public uint Iteration { get; }

        /// <summary>
        /// Gets the value of the major component of <see cref="DataMinerVersion.Version"/>.
        /// </summary>
        /// <value>The value of the major component of <see cref="DataMinerVersion.Version"/>.</value>
        public int Major => Version.Major;

        /// <summary>
        /// Gets the value of the minor component of <see cref="DataMinerVersion.Version"/>.
        /// </summary>
        /// <value>The value of the minor component of <see cref="DataMinerVersion.Version"/>.</value>
        public int Minor => Version.Minor;

        /// <summary>
        /// Gets the value of the build component of <see cref="DataMinerVersion.Version"/>.
        /// </summary>
        /// <value>The value of the build component of <see cref="DataMinerVersion.Version"/>.</value>
        /// <returns>The build number, or -1 if the build number is undefined.</returns>
        public int Build => Version.Build;

        /// <summary>
        /// Gets the value of the revision component of <see cref="DataMinerVersion.Version"/>.
        /// </summary>
        /// <value>The value of the revision component of <see cref="DataMinerVersion.Version"/>.</value>
        /// <returns>The revision number, or -1 if the revision number is undefined.</returns>
        public int Revision => Version.Revision;

        /// <summary>
        /// Determines whether the two specified objects are equal.
        /// </summary>
        /// <param name="v1">The first value to compare.</param>
        /// <param name="v2">The second value to compare.</param>
        /// <returns><c>true</c> if the operands are equal; otherwise, <c>false</c>.</returns>
        public static bool operator ==(DataMinerVersion v1, DataMinerVersion v2)
        {
            if (v1 is null)
            {
                return v2 is null;
            }

            return v1.Equals(v2);
        }

        /// <summary>
        /// Determines whether the two specified objects are not equal.
        /// </summary>
        /// <param name="v1">The first object to compare.</param>
        /// <param name="v2">The second object to compare.</param>
        /// <returns><c>false</c> if the operands are equal; otherwise, <c>true</c>.</returns>
        public static bool operator !=(DataMinerVersion v1, DataMinerVersion v2)
        {
            return !(v1 == v2);
        }

        /// <summary>
		/// Returns a value that indicates whether a specified <see cref="DataMinerVersion"/> value is less than another specified <see cref="DataMinerVersion"/> value.
		/// </summary>
		/// <param name="v1">The first value to compare.</param>
		/// <param name="v2">The second value to compare.</param>
		/// <returns><c>true</c> if <paramref name="v1"/> is less than <paramref name="v2"/>; otherwise, <c>false</c>.</returns>
        public static bool operator <(DataMinerVersion v1, DataMinerVersion v2)
        {
            if (v1 == null || v2 == null)
            {
                return false;
            }

            return v1.CompareTo(v2) < 0;
        }

        /// <summary>
		/// Returns a value that indicates whether a specified <see cref="DataMinerVersion"/> value is less than or equal to another specified <see cref="DataMinerVersion"/> value.
		/// </summary>
		/// <param name="v1">The first value to compare.</param>
		/// <param name="v2">The second value to compare.</param>
		/// <returns><c>true</c> if <paramref name="v1"/> is less than or equal to <paramref name="v2"/>; otherwise, <c>false</c>.</returns>
        public static bool operator <=(DataMinerVersion v1, DataMinerVersion v2)
        {
            if (v1 == null && v2 == null)
            {
                return true;
            }

            if (v1 == null || v2 == null)
            {
                return false;
            }

            return v1.CompareTo(v2) <= 0;
        }

        /// <summary>
		/// Returns a value that indicates whether a specified <see cref="DataMinerVersion"/> value is greater than another specified <see cref="DataMinerVersion"/> value.
		/// </summary>
		/// <param name="v1">The first value to compare.</param>
		/// <param name="v2">The second value to compare.</param>
		/// <returns><c>true</c> if <paramref name="v1"/> is greater than <paramref name="v2"/>; otherwise, <c>false</c>.</returns>
        public static bool operator >(DataMinerVersion v1, DataMinerVersion v2)
        {
            if (v1 == null || v2 == null)
            {
                return false;
            }

            return v2 < v1;
        }

        /// <summary>
		/// Returns a value that indicates whether a specified <see cref="DataMinerVersion"/> value is greater than or equal to another specified <see cref="DataMinerVersion"/> value.
		/// </summary>
		/// <param name="v1">The first value to compare.</param>
		/// <param name="v2">The second value to compare.</param>
		/// <returns><c>true</c> if <paramref name="v1"/> is greater than or equal to <paramref name="v2"/>; otherwise, <c>false</c>.</returns>
        public static bool operator >=(DataMinerVersion v1, DataMinerVersion v2)
        {
            return v2 <= v1;
        }

        /// <summary>
        /// Tries to convert the string representation of a DataMiner version number to an equivalent <see cref="DataMinerVersion"/> object, and returns a value that indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="input">A string that contains a DataMiner version number to convert.</param>
        /// <param name="result">When this method returns, contains the <see cref="DataMinerVersion"/> equivalent of the number that is contained in input, if the conversion succeeded. If input is null, Empty, or if the conversion fails, result is null when the method returns.</param>
        /// <returns><c>true</c> if the input parameter was converted successfully; otherwise, <c>false</c>.</returns>
        public static bool TryParse(string input, out DataMinerVersion result)
        {
            if (String.IsNullOrWhiteSpace(input))
            {
                result = default;
                return false;
            }

            var parts = input.Split(new[] { '-' }, 2);

            if (parts.Length == 2)
            {
                if (Version.TryParse(parts[0].Trim(), out Version version)
                    && UInt32.TryParse(parts[1].Trim(), out uint iteration))
                {
                    result = new DataMinerVersion(version, iteration);
                    return true;
                }
            }
            else if (parts.Length == 1 && Version.TryParse(parts[0].Trim(), out Version version))
            {
                result = new DataMinerVersion(version);
                return true;
            }
            else
            {
                // Do nothing.
            }

            result = default;
            return false;
        }

        /// <summary>
        /// Converts the string representation of a DataMiner version number to an equivalent <see cref="DataMinerVersion"/> object.
        /// </summary>
        /// <param name="input">A string that contains a version number to convert.</param>
        /// <returns>An object that is equivalent to the version number specified in the <paramref name="input"/> parameter.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="input"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="input"/> is not a valid DataMiner version.</exception>
        public static DataMinerVersion Parse(string input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (!TryParse(input, out DataMinerVersion result))
            {
                throw new ArgumentException($"Input '{input}' is not a valid DataMiner version.", nameof(input));
            }

            return result;
        }

        /// <summary>
        /// Returns a value indicating whether the current <see cref="DataMinerVersion"/> object is equal to a specified object.
        /// </summary>
        /// <param name="obj">An object to compare with the current <see cref="DataMinerVersion"/> object, or <see langword="null"/>.</param>
        /// <returns><c>true</c> if the current <see cref="DataMinerVersion"/> object and obj are both <see cref="DataMinerVersion"/> objects, and every component of the current <see cref="DataMinerVersion"/> object matches the corresponding component of obj; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            DataMinerVersion v = obj as DataMinerVersion;
            return v != null && Equals(v);
        }

        /// <summary>
        /// Returns a value indicating whether the current <see cref="DataMinerVersion"/> object and a specified Version object represent the same value.
        /// </summary>
        /// <param name="other">A Version object to compare to the current <see cref="DataMinerVersion"/> object, or null.</param>
        /// <returns><c>true</c> if every component of the current <see cref="DataMinerVersion"/> object matches the corresponding component of the obj parameter; otherwise, <c>false</c>.</returns>
        public bool Equals(DataMinerVersion other)
        {
            return other != null && Version == other.Version && Iteration == other.Iteration;
        }

        /// <summary>
        /// Returns a hash code for the current <see cref="DataMinerVersion"/> object.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        {
            int hash = 17;

            unchecked
            {
                hash = (hash * 23) + Version.GetHashCode();
                hash = (hash * 23) + Iteration.GetHashCode();
            }

            return hash;
        }

        /// <summary>
        /// Compares the current <see cref="DataMinerVersion"/> object to a specified object and returns an indication of their relative values.
        /// </summary>
        /// <param name="obj">An object to compare, or <see langword="null"/>.</param>
        /// <returns>A signed integer that indicates the relative values of the two objects.</returns>
        /// <exception cref="ArgumentException">version is not of type <see cref="DataMinerVersion"/>.</exception>
        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }

            DataMinerVersion v = obj as DataMinerVersion;
            if (v == null)
            {
                throw new ArgumentException("Argument obj must be of type DataMinerVersion.");
            }

            return CompareTo(v);
        }

        /// <summary>
        /// Compares the current <see cref="DataMinerVersion"/> object to a specified Version object and returns an indication of their relative values.
        /// </summary>
        /// <param name="value">A <see cref="DataMinerVersion"/> object to compare to the current <see cref="DataMinerVersion"/> object, or <see langword="null"/>.</param>
        /// <returns>A signed integer that indicates the relative values of the two objects.</returns>
        public int CompareTo(DataMinerVersion value)
        {
            if (Version != value.Version)
            {
                return Version > value.Version ? 1 : -1;
            }

            if (Iteration != value.Iteration)
            {
                return Iteration > value.Iteration ? 1 : -1;
            }

            return 0;
        }

        /// <summary>
        /// Converts the value of the current <see cref="DataMinerVersion"/> object to its equivalent String representation.
        /// </summary>
        /// <returns>The String representation of this <see cref="DataMinerVersion"/> object.</returns>
        public override string ToString()
        {
            return Iteration > 0
                       ? $"{Version} - {Iteration}"
                       : Version.ToString();
        }

        /// <summary>
        /// Converts the value of the current <see cref="DataMinerVersion"/> object to its equivalent String representation.
        /// Spaces are removed, expected format: X.X.X.X or X.X.X.X-X.
        /// </summary>
        /// <returns>The String representation of this <see cref="DataMinerVersion"/> object.</returns>
        internal string ToStrictString()
        {
            return Iteration > 0
                ? $"{Version}-{Iteration}"
                : Version.ToString();
        }
    }
}