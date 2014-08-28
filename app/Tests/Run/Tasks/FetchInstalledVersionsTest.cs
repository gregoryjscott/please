using System;
using System.IO;
using NUnit.Framework;
using Simpler;
using Please.Run.Tasks;
using Please.Run.Models;

namespace Tests.Run.Tasks
{
    [TestFixture]
    public class FetchInstalledVersionsTest
    {
        [Test]
        public void should_fetch_installed_versions()
        {
            // Arrange
            Database.Restore();

            var createVersionTable = Task.New<CreateVersionTable>();
            createVersionTable.In.ConnectionName = Database.Name;
            createVersionTable.Execute();

            var runScripts = Task.New<RunScripts>();
            runScripts.In.ConnectionName = Database.Name;
            runScripts.In.Scripts = new[] {new Script {FileName = Config.Scripts.Files.Sql.InsertVersion}};
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                runScripts.Execute();
            }

            var fetchInstalledVersions = Task.New<FetchInstalledVersions>();
            fetchInstalledVersions.In.ConnectionName = Database.Name;

            // Act
            fetchInstalledVersions.Execute();

            // Assert
            Assert.That(fetchInstalledVersions.Out.Versions.Length, Is.EqualTo(1));
        }
    }
}
