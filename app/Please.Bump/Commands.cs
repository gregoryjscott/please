using Please.Core.Models;
using Please.Bump.Tasks;
using Newtonsoft.Json;

namespace Please.Bump
{
    public class Commands
    {
        const string Path = @"(?:[a-zA-Z]\:\\|\\\\|\.\\)*([^/?*:;{}\\]+\\)*";
        const string FileOrDirectory = @"(?:[^/?*:;{}\\]+)";

        public static readonly Command<BumpVersion> Bump =
            new Command<BumpVersion>
            {
                Name = "bump version",
                Options = new[] {
                    new Option<BumpVersion> {
                        Action2 = (task, options) => {
                            task.In = JsonConvert.DeserializeObject<BumpVersion.Input>(options.Trim());
                        }
                    }
                }
            };
    }
}