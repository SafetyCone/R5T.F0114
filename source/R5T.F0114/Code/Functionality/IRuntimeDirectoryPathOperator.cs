using System;
using System.Collections.Generic;
using System.Linq;

using R5T.T0132;
using R5T.T0180;


namespace R5T.F0114
{
    [FunctionalityMarker]
    public partial interface IRuntimeDirectoryPathOperator : IFunctionalityMarker
    {
        /// <summary>
        /// For a <inheritdoc cref="Documentation.DotnetMajorRuntimeVersion" path="/name"/> (<see cref="Documentation.DotnetMajorRuntimeVersion"/>), and a runtimes directory path for that version, get all runtime directories for that version.
        /// </summary>
        public string[] GetRuntimeDirectoryPathsForMajorRuntime(
            IDirectoryPath runtimesDirectoryPath,
            int majorRuntime)
        {
            var majorRuntimeAsString = majorRuntime.ToString();

            var pattern = Instances.SearchPatternGenerator.Directories_StartingWith(majorRuntimeAsString);

            var directoryPaths = Instances.FileSystemOperator.EnumerateChildDirectoryPaths(
                runtimesDirectoryPath.Value,
                pattern)
                .Now();

            return directoryPaths;
        }

        /// <summary>
        /// Using directory names as version numbers, gets the directory name with the highest version.
        /// </summary>
        public string GetLatestVersionRuntimeDirectory(
            IEnumerable<string> runtimeDirectoryPaths)
        {
            var directoryPathsByVersion = new Dictionary<Version, string>();

            foreach (var runtimeDirectoryPath in runtimeDirectoryPaths)
            {
                var directoryName = Instances.PathOperator.Get_DirectoryName(runtimeDirectoryPath);

                var version = Instances.VersionOperator.From_Major_Minor_Build(directoryName);

                directoryPathsByVersion.Add(version, runtimeDirectoryPath);
            }

            var latestVersion = directoryPathsByVersion.Keys.Max();

            var latestVersionDirectoryPath = directoryPathsByVersion[latestVersion];
            return latestVersionDirectoryPath;
        }

        /// <summary>
        /// For a <inheritdoc cref="Documentation.DotnetMajorRuntimeVersion" path="/name"/> (<see cref="Documentation.DotnetMajorRuntimeVersion"/>) and runtimes directory path for the version, determine the directory of latest runtime for that major version.
        /// </summary>
        public string DetermineLatestRuntimeDirectory(
            IDirectoryPath runtimesDirectoryPath,
            int majorVersion)
        {
            var runtimeDirectoryPaths = this.GetRuntimeDirectoryPathsForMajorRuntime(
                runtimesDirectoryPath,
                majorVersion);

            var latestVersionRuntimeDirectory = this.GetLatestVersionRuntimeDirectory(
                runtimeDirectoryPaths);

            return latestVersionRuntimeDirectory;
        }

        public string DetermineLatestNetCoreRuntimeDirectory_6()
        {
            var runtimeDirectoryPath = this.DetermineLatestRuntimeDirectory(
                Instances.DotnetRuntimeDirectoryPaths.NetCoreApp_RuntimesDirectoryPath,
                Instances.DotnetMajorVersions.Version_6);

            return runtimeDirectoryPath;
        }

        public string DetermineLatestAspNetCoreRuntimeDirectory_6()
        {
            var runtimeDirectoryPath = this.DetermineLatestRuntimeDirectory(
                Instances.DotnetRuntimeDirectoryPaths.AspNetCoreApp_RuntimesDirectoryPath,
                Instances.DotnetMajorVersions.Version_6);

            return runtimeDirectoryPath;
        }

        public string DetermineWindowsDesktopAppLatestRuntimeDirectory_6()
        {
            var runtimeDirectoryPath = this.DetermineLatestRuntimeDirectory(
                Instances.DotnetRuntimeDirectoryPaths.WindowsDesktopApp_RuntimesDirectoryPath,
                Instances.DotnetMajorVersions.Version_6);

            return runtimeDirectoryPath;
        }

        public DotnetRuntimeDirectoryPaths GetDotnetRuntimeDirectoryPaths()
        {
            var netCoreApp = this.DetermineLatestNetCoreRuntimeDirectory_6();
            var aspNetCoreApp = this.DetermineLatestAspNetCoreRuntimeDirectory_6();
            var windowsDesktopApp = this.DetermineWindowsDesktopAppLatestRuntimeDirectory_6();

            var output = new DotnetRuntimeDirectoryPaths
            {
                AspNetCoreApp = aspNetCoreApp,
                NetCoreApp = netCoreApp,
                WindowsDesktopApp = windowsDesktopApp,
            };

            return output;
        }
    }
}
