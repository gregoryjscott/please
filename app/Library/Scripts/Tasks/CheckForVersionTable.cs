using System;
using Simpler;
using Simpler.Data;
using Chic;

namespace Library.Scripts.Tasks
{
    public class CheckForVersionTable : InOutTask<CheckForVersionTable.Input, CheckForVersionTable.Output>
    {
        public class Input
        {
            public string ConnectionName { get; set; }
        }

        public class Output
        {
            public bool TableExists { get; set; }
        }

        public override void Execute()
        {
            using (var db = this.Connect(In.ConnectionName))
            {
                try
                {
                    Db.GetScalar(db, this.Sql());
                    Out.TableExists = true;
                }
                catch (Exception)
                {
                    Out.TableExists = false;
                }
            }
        }
    }
}
