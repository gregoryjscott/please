using Please.Core.Models;
using Please.Bump.Tasks;
using Please.Bump;
using Please.Bump.Models;

namespace Please.Bump
{
    public class Commands
    {
        const string Path = @"(?:[a-zA-Z]\:\\|\\\\|\.\\)*([^/?*:;{}\\]+\\)*";
        const string FileOrDirectory = @"(?:[^/?*:;{}\\]+)";

        public static readonly Command<BumpVersion> Bump =
            new Command<BumpVersion>
            {
                Name = "bump",
                Options =
                    new[]
                    {
                        new Option<BumpVersion>
                        {
                            Pattern = @"major version\s",
                            Action = (task, match) => task.In.BumpType = BumpType.Major
                        },
                        new Option<BumpVersion>
                        {
                            Pattern = @"minor version\s",
                            Action = (task, match) => task.In.BumpType = BumpType.Minor
                        },
                        new Option<BumpVersion>
                        {
                            Pattern = @"patch version\s",
                            Action = (task, match) => task.In.BumpType = BumpType.Patch
                        },
                        new Option<BumpVersion>
                        {
                            Pattern = @"in (?<File>" + Path + @"AssemblyInfo\.cs)",
                            Action = (task, match) =>
                                     {
                                         task.In.FileType = FileType.AssemblyInfo;
                                         task.In.FileName = match.Groups["File"].Value.Trim();
                                     }
                        },
                        new Option<BumpVersion>
                        {
                            Pattern = @"in (?<File>" + Path + FileOrDirectory + @"\.nuspec)",
                            Action = (task, match) =>
                                     {
                                         task.In.FileType = FileType.Nuspec;
                                         task.In.FileName = match.Groups["File"].Value.Trim();
                                     }
                        },
                        new Option<BumpVersion>
                        {
                            Pattern = @"in (?<File>" + Path + FileOrDirectory + ")",
                            Action = (task, match) =>
                                     {
                                         task.In.FileType = FileType.Script;
                                         task.In.FileName = match.Groups["File"].Value.Trim();
                                     }
                        }
                    }
            };
    }
}