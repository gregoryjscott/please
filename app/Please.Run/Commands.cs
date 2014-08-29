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
            };

        public static readonly Command<RunSomething> RunPy =
            new Command<RunSomething>
            {
                Name = "run py",
                Options = new[] {
                    new Option<RunSomething> {
                        Action2 = (task, options) => {
                            task.In = JsonConvert.DeserializeObject<RunSomething.Input>(options.Trim());
                            task.In.Extensions = new[] {".py"};
                        }
                    }
                }
            };

        public static readonly Command<RunSomething> RunAll =
            new Command<RunSomething>
            {
                Name = "run all",
                Options = new[] {
                    new Option<RunSomething> {
                        Action2 = (task, options) => {
                            task.In = JsonConvert.DeserializeObject<RunSomething.Input>(options.Trim());
                            task.In.Extensions = new[] {".sql", ".py"};
                        }
                    }
                }
            };
    }
}