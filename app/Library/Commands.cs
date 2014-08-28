using Please.Core.Models;

namespace Library
{
    public class Commands
    {
        public static ICommand[] All = {
            Please.Bump.Commands.Bump,
            Please.Run.Commands.RunSql,
            Please.Run.Commands.RunPy,
            Please.Run.Commands.RunAll,
            Please.Timestamp.Commands.Timestamp
        };
    }
}