using Please.Timestamp.Tasks;
using Please.Core.Models;
using Newtonsoft.Json;

namespace Please.Timestamp
{
    public class Commands
    {
        public static Command<AddTimestamp> Timestamp =
            new Command<AddTimestamp> {
                Name = "add timestamp",
                Options = new[] {
                    new Option<AddTimestamp> {
                        Action2 = (task, options) => {
                            task.In = JsonConvert.DeserializeObject<AddTimestamp.Input>(options.Trim());
                        }
                    }
                }
            };
    }
}