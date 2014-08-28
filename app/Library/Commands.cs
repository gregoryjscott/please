using Library.Scripts.Tasks;
using Please.Core.Models;

namespace Library
{
    public class Commands
    {
        const string Path = @"(?:[a-zA-Z]\:\\|\\\\|\.\\)*([^/?*:;{}\\]+\\)*";
        const string FileOrDirectory = @"(?:[^/?*:;{}\\]+)";

        static readonly Command<Run> RunSql =
            new Command<Run>
            {
                Name = "run sql",
                Options =
                    new[]
                    {
                        new Option<Run>
                        {
                            Pattern = "with versioning",
                            Action = (task, match) =>
                                     {
                                         task.In.Extensions = new[] {".sql"};
                                         task.In.WithVersioning = true;
                                     }
                        },
                        new Option<Run>
                        {
                            Pattern = @"on (?<ConnectionName>\w+)",
                            Action = (task, match) =>
                                     {
                                         task.In.Extensions = new[] {".sql"};
                                         task.In.ConnectionName = match.Groups["ConnectionName"].Value;
                                     }
                        },
                        new Option<Run>
                        {
                            Pattern = @"file (?<File>" + Path + FileOrDirectory + ")",
                            Action = (task, match) =>
                                     {
                                         task.In.Extensions = new[] {".sql"};
                                         task.In.File = match.Groups["File"].Value.Trim();
                                     }
                        },
                        new Option<Run>
                        {
                            Pattern = @"in (?<Directory>" + Path + FileOrDirectory + ")",
                            Action = (task, match) =>
                                     {
                                         task.In.Extensions = new[] {".sql"};
                                         task.In.Directory = match.Groups["Directory"].Value.Trim();
                                     }
                        },
                        new Option<Run>
                        {
                            Pattern = @"include (?<Whitelist>" + Path + FileOrDirectory + ")",
                            Action = (task, match) =>
                                     {
                                         task.In.Extensions = new[] {".sql"};
                                         task.In.WhitelistFile = match.Groups["Whitelist"].Value.Trim();
                                     }
                        }
                    }
            };

        static readonly Command<Run> RunPy =
            new Command<Run>
            {
                Name = "run py",
                Options =
                    new[]
                    {
                        new Option<Run>
                        {
                            Pattern = "with versioning",
                            Action = (task, match) =>
                                     {
                                         task.In.Extensions = new[] {".py"};
                                         task.In.WithVersioning = true;
                                     }
                        },
                        new Option<Run>
                        {
                            Pattern = @"on (?<ConnectionName>\w+)",
                            Action = (task, match) =>
                                     {
                                         task.In.Extensions = new[] {".py"};
                                         task.In.ConnectionName = match.Groups["ConnectionName"].Value;
                                     }
                        },
                        new Option<Run>
                        {
                            Pattern = @"file (?<File>" + Path + FileOrDirectory + ")",
                            Action = (task, match) =>
                                     {
                                         task.In.Extensions = new[] {".py"};
                                         task.In.File = match.Groups["File"].Value.Trim();
                                     }
                        },
                        new Option<Run>
                        {
                            Pattern = @"in (?<Directory>" + Path + FileOrDirectory + ")",
                            Action = (task, match) =>
                                     {
                                         task.In.Extensions = new[] {".py"};
                                         task.In.Directory = match.Groups["Directory"].Value.Trim();
                                     }
                        },
                        new Option<Run>
                        {
                            Pattern = @"include (?<Whitelist>" + Path + FileOrDirectory + ")",
                            Action = (task, match) =>
                                     {
                                         task.In.Extensions = new[] {".py"};
                                         task.In.WhitelistFile = match.Groups["Whitelist"].Value.Trim();
                                     }
                        }
                    }
            };

        static readonly Command<Run> RunAll =
            new Command<Run>
            {
                Name = "run all",
                Options =
                    new[]
                    {
                        new Option<Run>
                        {
                            Pattern = "with versioning",
                            Action = (task, match) =>
                                     {
                                         task.In.Extensions = new[] {".sql", ".py"};
                                         task.In.WithVersioning = true;
                                     }
                        },
                        new Option<Run>
                        {
                            Pattern = @"on (?<ConnectionName>\w+)",
                            Action = (task, match) =>
                                     {
                                         task.In.Extensions = new[] {".sql", ".py"};
                                         task.In.ConnectionName = match.Groups["ConnectionName"].Value;
                                     }
                        },
                        new Option<Run>
                        {
                            Pattern = @"in (?<Directory>" + Path + FileOrDirectory + ")",
                            Action = (task, match) =>
                                     {
                                         task.In.Extensions = new[] {".sql", ".py"};
                                         task.In.Directory = match.Groups["Directory"].Value.Trim();
                                     }
                        },
                        new Option<Run>
                        {
                            Pattern = @"include (?<Whitelist>" + Path + FileOrDirectory + ")",
                            Action = (task, match) =>
                                     {
                                         task.In.Extensions = new[] {".sql", ".py"};
                                         task.In.WhitelistFile = match.Groups["Whitelist"].Value.Trim();
                                     }
                        }
                    }
            };

        public static ICommand[] All = {Please.Bump.Commands.Bump, RunSql, RunPy, RunAll, Please.Timestamp.Commands.Timestamp};
    }
}