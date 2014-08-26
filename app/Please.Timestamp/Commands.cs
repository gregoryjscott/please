using Please.Timestamp.Tasks;
using Please.Core.Models;
using Newtonsoft.Json;

namespace Please.Timestamp
{
    public class Commands
    {
        const string Path = @"(?:[a-zA-Z]\:\\|\\\\|\.\\)*([^/?*:;{}\\]+\\)*";
        const string FileOrDirectory = @"(?:[^/?*:;{}\\]+)";

        public static Command<AddTimestamp> Timestamp =
            new Command<AddTimestamp> {
                Name = "add timestamp",
                Options = new[] {
                    new Option<AddTimestamp> {
                        Pattern = @"in (?<Directory>" + Path + FileOrDirectory + ")",
                        Action2 = (task, options) => {
                            var directory = options.Trim();
                            var json = @"{'Directory': '" + directory + "'}";
                            task.In = JsonConvert.DeserializeObject<AddTimestamp.Input>(json);
                        }
                    }
                }
            };
    }
}