using System;
using Library.Scripts.Tasks;
using NUnit.Framework;
using Simpler;
using Simpler.Data;
using Chic;

namespace Tests.Scripts.Tasks
{
    [TestFixture]
    public class CreateVersionTableTest
    {
        [Test]
        public void should_create_version_table()
        {
            // Arrange
            Database.Restore();

            var createVersionTable = Task.New<CreateVersionTable>();
            createVersionTable.In.ConnectionName = Database.Name;

            // Act
            createVersionTable.Execute();

            // Assert
            using (var db = createVersionTable.Connect(Database.Name))
            {
                var count = Db.GetScalar(db, "select count(1) from sqlite_master where type = 'table';");
                Assert.That(Convert.ToInt32(count), Is.EqualTo(1));
            }
        }
    }
}
