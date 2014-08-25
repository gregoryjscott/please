using Simpler;

namespace Please
{
    public class Program
    {
        static int Main(string[] args)
        {
            var main = Task.New<Library.Main>();
            main.In.Args = args;
            main.Execute();
            return main.Out.ExitCode;
        }
    }
}
