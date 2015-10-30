namespace DirComp
{
    /// <summary>
    /// Immutable path info for a file.
    /// </summary>
    using System.IO;

    class Fi
    {
        /// <summary>
        /// Store the path info from the FileInfo.
        /// </summary>
        /// <param name="fi"></param>
        public Fi(FileInfo fi)
        {
            Path = fi.FullName;
            Dir = fi.DirectoryName;
            Name = fi.Name;
        }

        public string Dir { get; private set; }
        public string Name { get; private set; }
        public string Path { get; private set; }
    }
}
