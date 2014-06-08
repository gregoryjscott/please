using Simpler;
using Simpler.Data;
using Chic;

namespace Library.Scripts.Tasks
{
    public class CreateVersionTable : InOutTask<CreateVersionTable.Input, CreateVersionTable.Output>
    {
        public class Input
        {
            public string ConnectionName { get; set; }
        }

        public class Output
        {
            public int RowsAffected { get; set; }
        }

        public override void Execute()
        {
            using (var db = this.Connect(In.ConnectionName))
            {
                Out.RowsAffected = Db.GetResult(db, this.Sql());
            }
        }
    }
}
