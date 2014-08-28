using System;
using System.IO;
using NUnit.Framework;
using Simpler;
using Simpler.Data;
using Please.Run.Tasks;

namespace Tests.Run.Tasks
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
            using (var connection = Db.Connect(Database.Name))
            {
                var count = Db.GetScalar(connection, "select count(1) from sqlite_master where type = 'table';");
                Assert.That(Convert.ToInt32(count), Is.EqualTo(1));
            }
        }
    }
}
