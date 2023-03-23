using System;


namespace R5T.F0114
{
    public static class Instances
    {
        public static Z0040.IDotnetRuntimeDirectoryPaths DotnetRuntimeDirectoryPaths => Z0040.DotnetRuntimeDirectoryPaths.Instance;
        public static Z0040.IIDotnetMajorVersions DotnetMajorVersions => Z0040.IDotnetMajorVersions.Instance;
        public static F0000.IFileSystemOperator FileSystemOperator => F0000.FileSystemOperator.Instance;
        public static F0002.IPathOperator PathOperator => F0002.PathOperator.Instance;
        public static F0000.ISearchPatternGenerator SearchPatternGenerator => F0000.SearchPatternGenerator.Instance;
        public static F0000.IVersionOperator VersionOperator => F0000.VersionOperator.Instance;
    }
}