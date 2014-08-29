using System;
using Simpler;
using Please.Run.Tasks;
using Please.Run.Models;

namespace Please.Run.Tasks
{
    // TODOs
    // * Change WhitelistFile to Whitelist (multiple files)
    // * Get rid of File option, Whitelist should cover it.
    // * Should Whitelist and Extenstions be combined into just wildcard type search?
    // * Switch from using ConnectionName to Connection
    // * Pass in Timeout so that Config can be eliminated
    public class RunSomething : InTask<RunSomething.Input>
    {
        public class Input
        {
            public string ConnectionName { get; set; }
            public string Directory { get; set; }
            public string[] Extensions { get; set; }
            public string File { get; set; }
            public bool WithVersioning { get; set; }
            public string WhitelistFile { get; set; }
        }

        public CheckForVersionTable CheckForVersionTable { get; set; }
        public CreateVersionTable CreateVersionTable { get; set; }
        public GetScripts GetScripts { get; set; }
        public FetchInstalledVersions FetchInstalledVersions { get; set; }
        public RunMissingVersions RunMissingVersions { get; set; }
        public RunScripts RunScripts { get; set; }

        public override void Execute()
        {
            if (In.File != null)
            {
                Console.WriteLine("{0} script was found.", In.File);
                RunScripts.In.ConnectionName = In.ConnectionName;
                RunScripts.In.Scripts = new[] { new Script { FileName = In.File, IsVersioned = false} };
                RunScripts.Execute();
                return;
            }
            
            if (In.WithVersioning)
            {
                CheckForVersionTable.In.ConnectionName = In.ConnectionName;
                CheckForVersionTable.Execute();

                if (!CheckForVersionTable.Out.TableExists)
                {
                    CreateVersionTable.In.ConnectionName = In.ConnectionName;
                    CreateVersionTable.Execute();
                }

                FetchInstalledVersions.In.ConnectionName = In.ConnectionName;
                FetchInstalledVersions.Execute();
            }


            GetScripts.In.Directory = In.Directory;
            GetScripts.In.Extensions = In.Extensions;
            GetScripts.In.CheckForVersionedFilesOnly = In.WithVersioning;
            GetScripts.In.WhitelistFile = In.WhitelistFile;
            GetScripts.Execute();

            if (In.WithVersioning)
            {
                RunMissingVersions.In.ConnectionName = In.ConnectionName;
                RunMissingVersions.In.Scripts = GetScripts.Out.Scripts;
                RunMissingVersions.In.InstalledVersions = FetchInstalledVersions.Out.Versions;
                RunMissingVersions.Execute();
            }
            else
            {
                Console.WriteLine("{0} scripts were found in {1}.", GetScripts.Out.Scripts.Length, In.Directory);
                RunScripts.In.ConnectionName = In.ConnectionName;
                RunScripts.In.Scripts = GetScripts.Out.Scripts;
                RunScripts.Execute();
            }
        }
    }
}
