namespace DirComp
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Indexes names and paths of files and folders recursively on creation, immutable.
    /// </summary>
    class Dir
    {
        /// <summary>
        /// Gets the path info for all contained files and directories recursively.
        /// </summary>
        /// <param name="di"></param>
        public Dir(DirectoryInfo di)
        {
            Name = di.Name;
            Path = di.FullName;
            Files = di.GetFiles().Select(a => new Fi(a)).ToList();
            SubDirs = di.GetDirectories().Select(a => new Dir(a)).ToList();
        }


        public IEnumerable<Dir> SubDirs { get; private set; }
        public IEnumerable<Fi> Files { get; private set; }
        public string Name { get; private set; }
        public string Path { get; private set; }

        /// <summary>
        /// Looks for dirs and files that exist in the other directory but not in this. Files inside missing dirs are not listed.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public IEnumerable<string> Missing(Dir other)
        {
            // Iterate the other collection to find items unique to it.
            var missingFiles = other.Files.Where(a => !Files.Any(b => a.Name == b.Name));
            var missingDirs = other.SubDirs.Where(a => !SubDirs.Any(b => a.Name == b.Name));

            var output = missingFiles.Select(a => a.Path);
            output = output.Concat(missingDirs.Select(a => a.Path));

            // Iterate through this collection so the we can look for matches.
            foreach (Dir dir in SubDirs)
            {
                var match = other.SubDirs.FirstOrDefault(a => a.Name == dir.Name);
                if (null != match)
                {
                    // If the dir exists, look for missing subdirs recursively.
                    output = output.Concat(dir.Missing(match));
                }
            }

            return output;
        }
    }
}
