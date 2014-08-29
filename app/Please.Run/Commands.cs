using Please.Core.Models;
using Please.Run.Tasks;
using Newtonsoft.Json;

namespace Please.Run
{
    public class Commands
    {
        const string Path = @"(?:[a-zA-Z]\:\\|\\\\|\.\\)*([^/?*:;{}\\]+\\)*";
        const string FileOrDirectory = @"(?:[^/?*:;{}\\]+)";

        public static readonly Command<RunSomething> RunSql =
            new Command<RunSomething>
            {
                Name = "run sql",
                Options = new[] {
                    new Option<RunSomething> {
                        Action2 = (task, options) => {
                            task.In = JsonConvert.DeserializeObject<RunSomething.Input>(options.Trim());
                            task.In.Extensions = new[] {".sql"};
                        }
                    }
                }
//                Options =
//                    new[]
//                    {
//                        new Option<RunSomething>
//                        {
//                            Pattern = "with versioning",
//                            Action = (task, match) =>
//                                     {
//                                         task.In.Extensions = new[] {".sql"};
//                                         task.In.WithVersioning = true;
//                                     }
//                        },
//                        new Option<RunSomething>
//                        {
//                            Pattern = @"on (?<ConnectionName>\w+)",
//                            Action = (task, match) =>
//                                     {
//                                         task.In.Extensions = new[] {".sql"};
//                                         task.In.ConnectionName = match.Groups["ConnectionName"].Value;
//                                     }
//                        },
//                        new Option<RunSomething>
//                        {
//                            Pattern = @"file (?<File>" + Path + FileOrDirectory + ")",
//                            Action = (task, match) =>
//                                     {
//                                         task.In.Extensions = new[] {".sql"};
//                                         task.In.File = match.Groups["File"].Value.Trim();
//                                     }
//                        },
//                        new Option<RunSomething>
//                        {
//                            Pattern = @"in (?<Directory>" + Path + FileOrDirectory + ")",
//                            Action = (task, match) =>
//                                     {
//                                         task.In.Extensions = new[] {".sql"};
//                                         task.In.Directory = match.Groups["Directory"].Value.Trim();
//                                     }
//                        },
//                        new Option<RunSomething>
//                        {
//                            Pattern = @"include (?<Whitelist>" + Path + FileOrDirectory + ")",
//                            Action = (task, match) =>
//                                     {
//                                         task.In.Extensions = new[] {".sql"};
//                                         task.In.WhitelistFile = match.Groups["Whitelist"].Value.Trim();
//                                     }
//                        }
//                    }
            };

        public static readonly Command<RunSomething> RunPy =
            new Command<RunSomething>
            {
                Name = "run py",
                Options =
                    new[]
                    {
                        new Option<RunSomething>
                        {
                            Pattern = "with versioning",
                            Action = (task, match) =>
                                     {
                                         task.In.Extensions = new[] {".py"};
                                         task.In.WithVersioning = true;
                                     }
                        },
                        new Option<RunSomething>
                        {
                            Pattern = @"on (?<ConnectionName>\w+)",
                            Action = (task, match) =>
                                     {
                                         task.In.Extensions = new[] {".py"};
                                         task.In.ConnectionName = match.Groups["ConnectionName"].Value;
                                     }
                        },
                        new Option<RunSomething>
                        {
                            Pattern = @"file (?<File>" + Path + FileOrDirectory + ")",
                            Action = (task, match) =>
                                     {
                                         task.In.Extensions = new[] {".py"};
                                         task.In.File = match.Groups["File"].Value.Trim();
                                     }
                        },
                        new Option<RunSomething>
                        {
                            Pattern = @"in (?<Directory>" + Path + FileOrDirectory + ")",
                            Action = (task, match) =>
                                     {
                                         task.In.Extensions = new[] {".py"};
                                         task.In.Directory = match.Groups["Directory"].Value.Trim();
                                     }
                        },
                        new Option<RunSomething>
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

        public static readonly Command<RunSomething> RunAll =
            new Command<RunSomething>
            {
                Name = "run all",
                Options =
                    new[]
                    {
                        new Option<RunSomething>
                        {
                            Pattern = "with versioning",
                            Action = (task, match) =>
                                     {
                                         task.In.Extensions = new[] {".sql", ".py"};
                                         task.In.WithVersioning = true;
                                     }
                        },
                        new Option<RunSomething>
                        {
                            Pattern = @"on (?<ConnectionName>\w+)",
                            Action = (task, match) =>
                                     {
                                         task.In.Extensions = new[] {".sql", ".py"};
                                         task.In.ConnectionName = match.Groups["ConnectionName"].Value;
                                     }
                        },
                        new Option<RunSomething>
                        {
                            Pattern = @"in (?<Directory>" + Path + FileOrDirectory + ")",
                            Action = (task, match) =>
                                     {
                                         task.In.Extensions = new[] {".sql", ".py"};
                                         task.In.Directory = match.Groups["Directory"].Value.Trim();
                                     }
                        },
                        new Option<RunSomething>
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
    }
}