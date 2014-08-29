using Please.Core.Models;
using Please.Run.Tasks;
using Newtonsoft.Json;

namespace Please.Run
{
    public class Commands
    {
        public static readonly Command<RunSomething> RunSql =
            new Command<RunSomething> {
                Name = "run sql",
                Setup = (task, options) => {
                    task.In = JsonConvert.DeserializeObject<RunSomething.Input>(options.Trim());
                    task.In.Extensions = new[] { ".sql" };
                }
            };

        public static readonly Command<RunSomething> RunPy =
            new Command<RunSomething> {
                Name = "run py",
                Setup = (task, options) => {
                    task.In = JsonConvert.DeserializeObject<RunSomething.Input>(options.Trim());
                    task.In.Extensions = new[] { ".py" };
                }
            };

        public static readonly Command<RunSomething> RunAll =
            new Command<RunSomething> {
                Name = "run all",
                Setup = (task, options) => {
                    task.In = JsonConvert.DeserializeObject<RunSomething.Input>(options.Trim());
                    task.In.Extensions = new[] { ".sql", ".py" };
                }
            };
    }
}