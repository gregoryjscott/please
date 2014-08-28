using System.Data.Common;

namespace Please.Run.Models
{
    public class RunException : DbException
    {
        public RunException(string message) : base (message) {}
    }
}
