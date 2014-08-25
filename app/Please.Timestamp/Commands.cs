using Please.Timestamp.Tasks;
using Please.Core.Models;

namespace Please.Timestamp
{
    public class Commands
    {
        const string Path = @"(?:[a-zA-Z]\:\\|\\\\|\.\\)*([^/?*:;{}\\]+\\)*";
        const string FileOrDirectory = @"(?:[^/?*:;{}\\]+)";

        public static Command<AddTimestamp> Timestamp =
            new Command<AddTimestamp>
            {
                Name = "add timestamp",
                Options =
                    new[]
                    {
                        new Option<AddTimestamp>
                        {
                            Pattern = @"in (?<Directory>" + Path + FileOrDirectory + ")",
                            Action = (task, match) => task.In.Directory = match.Groups["Directory"].Value.Trim()
                        }
                    }
            };
    }
}