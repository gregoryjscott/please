using System;
using System.IO;
using NUnit.Framework;
using Simpler;
using Simpler.Data;
using Please.Run.Tasks;

namespace Tests.Run.Tasks
{
    [TestFixture]
    public class InsertInstalledVersionTest
    {
        [Test]
        public void should_insert_version()
        {
            // Arrange
            Database.Restore();

            var createVersionTable = Task.New<CreateVersionTable>();
            createVersionTable.In.ConnectionName = Database.Name;
            createVersionTable.Execute();

            var insertInstalledVersion = Task.New<InsertInstalledVersion>();
            insertInstalledVersion.In.ConnectionName = Database.Name;
            insertInstalledVersion.In.Version = new Please.Run.Models.Version { Id = "999" };

            // Act
            insertInstalledVersion.Execute();

            // Assert
            Assert.That(insertInstalledVersion.Out.RowsAffected, Is.EqualTo(1));
        }

        [Test]
        public void should_insert_version_as_string()
        {
            // Arrange
            Database.Restore();

            var createVersionTable = Task.New<CreateVersionTable>();
            createVersionTable.In.ConnectionName = Database.Name;
            createVersionTable.Execute();

            var insertInstalledVersion = Task.New<InsertInstalledVersion>();
            insertInstalledVersion.In.ConnectionName = Database.Name;
            insertInstalledVersion.In.Version = new Please.Run.Models.Version { Id = "01" };

            // Act
            insertInstalledVersion.Execute();

            // Assert
            using (var connection = Db.Connect(Database.Name))
            {
                var count = Db.GetScalar(connection, "select version from db_version where version = '01';");
                Assert.That(Convert.ToInt32(count), Is.EqualTo(1));
            }
        }
    }
}
