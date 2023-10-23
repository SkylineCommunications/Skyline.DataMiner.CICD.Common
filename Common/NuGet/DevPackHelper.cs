namespace Skyline.DataMiner.CICD.Common.NuGet
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using global::NuGet.Common;
    using global::NuGet.Protocol;
    using global::NuGet.Protocol.Core.Types;

    /// <summary>
    /// Useful properties and methods related to DevPacks.
    /// </summary>
    public static class DevPackHelper
    {
        private const string CommonDevPackName = "Skyline.DataMiner.Dev.Common";

        private static string latestRevisionOfDevPack;
        private static DateTime retrievalTime;

        private static readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(10);

        /// <summary>
        /// Gets the DevPack prefix including the ending dot.
        /// </summary>
        public static readonly string DevPackPrefix = "Skyline.DataMiner.Dev.";

        /// <summary>
        /// Gets the DevPack dependencies Files prefix including the ending dot.
        /// </summary>
        public static readonly string FilesPrefix = "Skyline.DataMiner.Files.";

        /// <summary>
        /// Gets the names of all the NuGet DevPack packages.
        /// </summary>
        public static readonly IReadOnlyCollection<string> DevPackNuGetPackages = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "Skyline.DataMiner.Dev.Automation",
            CommonDevPackName,
            "Skyline.DataMiner.Dev.Protocol",
        };

        /// <summary>
        /// Gets the names of all the NuGet dependencies for the Common DevPack.
        /// </summary>
        public static readonly IReadOnlyCollection<string> CommonDevPackNuGetDependencies = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "Skyline.DataMiner.Files.protobufnet",
            "Skyline.DataMiner.Files.Skyline.DataMiner.Storage.Types",
            "Skyline.DataMiner.Files.SLLoggerUtil",
            "Skyline.DataMiner.Files.SLNetTypes",
            "Skyline.DataMiner.Files.SLProtoBufLibrary"
        };

        /// <summary>
        /// Gets the names of all the NuGet dependencies for the Protocol DevPack without the Common DevPack dependencies.
        /// </summary>
        public static readonly IReadOnlyCollection<string> ProtocolDevPackNuGetDependencies = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "Skyline.DataMiner.Files.Interop.SLDms",
            "Skyline.DataMiner.Files.QActionHelperBaseClasses",
            "Skyline.DataMiner.Files.SLManagedScripting"
        };

        /// <summary>
        /// Gets the names of all the NuGet dependencies for the Automation DevPack without the Common DevPack dependencies.
        /// </summary>
        public static readonly IReadOnlyCollection<string> AutomationDevPackNuGetDependencies = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "Skyline.DataMiner.Files.SLAnalyticsTypes",
            "Skyline.DataMiner.Files.SLManagedAutomation"
        };

        /// <summary>
        /// Gets the names of all the NuGet dependencies for the Protocol DevPack with the Common DevPack included.
        /// </summary>
        public static readonly IReadOnlyCollection<string> ProtocolDevPackNuGetDependenciesIncludingTransitive =
            new HashSet<string>(CommonDevPackNuGetDependencies.Concat(ProtocolDevPackNuGetDependencies), StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Gets the names of all the NuGet dependencies for the Automation DevPack with the Common DevPack included.
        /// </summary>
        public static readonly IReadOnlyCollection<string> AutomationDevPackNuGetDependenciesIncludingTransitive =
            new HashSet<string>(CommonDevPackNuGetDependencies.Concat(AutomationDevPackNuGetDependencies), StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Retrieves the latest revision of the DevPack packages for the specified DataMiner version.
        /// </summary>
        /// <param name="versionToCheck">The DataMiner version.</param>
        /// <returns>The latest revision of the DevPack packages for the specified DataMiner version.</returns>
        public static async Task<string> GetLatestRevisionOfDevPackAsync(DataMinerVersion versionToCheck)
        {
            if (latestRevisionOfDevPack != null && DateTime.Now - retrievalTime < CacheDuration)
            {
                return latestRevisionOfDevPack;
            }

            ILogger logger = NullLogger.Instance;
            CancellationToken cancellationToken = CancellationToken.None;

            int maxRevisionNumberFound = -1;

            SourceRepository repository = Repository.Factory.GetCoreV3("https://api.nuget.org/v3/index.json");
            PackageSearchResource resource = await repository.GetResourceAsync<PackageSearchResource>();
            SearchFilter searchFilter = new SearchFilter(includePrerelease: false);

            IEnumerable<IPackageSearchMetadata> results = await resource.SearchAsync(
                CommonDevPackName,
                searchFilter,
                skip: 0,
                take: 1,
                log: logger,
                cancellationToken: cancellationToken);

            foreach (IPackageSearchMetadata result in results)
            {
                var id = result.Identity.Id;

                if (!id.Equals(CommonDevPackName))
                {
                    continue;
                }

                var versions = await result.GetVersionsAsync();

                foreach (var v in versions)
                {
                    if (v.Version.IsPrerelease || !(v.Version.Major == versionToCheck.Major && v.Version.Minor == versionToCheck.Minor &&
                                                    v.Version.Patch == versionToCheck.Build))
                    {
                        continue;
                    }

                    int revision = v.Version.Revision;

                    if (revision > maxRevisionNumberFound)
                    {
                        maxRevisionNumberFound = revision;
                    }
                }
            }

            latestRevisionOfDevPack = $"{versionToCheck.Major}.{versionToCheck.Minor}.{versionToCheck.Build}.{maxRevisionNumberFound}";
            retrievalTime = DateTime.Now;

            return latestRevisionOfDevPack;
        }
    }
}