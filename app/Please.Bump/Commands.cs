using Please.Core.Models;
using Please.Bump.Tasks;
using Newtonsoft.Json;

namespace Please.Bump
{
    public class Commands
    {
        public static readonly Command<BumpVersion> Bump =
            new Command<BumpVersion> {
                Name = "bump version",
                Setup = (task, options) => {
                    task.In = JsonConvert.DeserializeObject<BumpVersion.Input>(options.Trim());
                }
            };
    }
}