namespace Skyline.DataMiner.CICD.Common
{
    using System;

    /// <summary>
    /// Defines global default values.
    /// </summary>
    public static class GlobalDefaults
    {
        /// <summary>
        /// The DataMiner version that introduced app package support.
        /// </summary>
        public static readonly string MinimumSupportDataMinerVersionForDMApp = DataMinerVersion.MinSupportedVersionForDmapp.ToStrictString();
    }
}