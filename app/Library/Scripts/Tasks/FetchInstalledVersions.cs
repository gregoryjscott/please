using Simpler;
using Simpler.Data;
using Chic;

namespace Library.Scripts.Tasks
{
    public class FetchInstalledVersions : InOutTask<FetchInstalledVersions.Input, FetchInstalledVersions.Output>
    {
        public class Input
        {
            public string ConnectionName { get; set; }
        }

        public class Output
        {
            public Version[] Versions { get; set; }
        }

        public override void Execute()
        {
            using (var db = this.Connect(In.ConnectionName))
            {
                Out.Versions = Db.GetMany<Version>(db, this.Sql());
            }
        }
    }
}
