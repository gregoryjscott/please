using System;
using Simpler;
using Simpler.Data;
using Chic;

namespace Library.Scripts.Tasks
{
    public class InsertInstalledVersion : InOutTask<InsertInstalledVersion.Input, InsertInstalledVersion.Output>
    {
        public class Input
        {
            public string ConnectionName { get; set; }
            public Version Version { get; set; }
        }

        public class Output
        {
            public int RowsAffected { get; set; }
        }

        public override void Execute()
        {
            var sql = String.Format(this.Sql(), In.Version.Id);

            using (var db = this.Connect(In.ConnectionName))
            {
                Out.RowsAffected = Db.GetResult(db, sql);
            }
        }
    }
}
