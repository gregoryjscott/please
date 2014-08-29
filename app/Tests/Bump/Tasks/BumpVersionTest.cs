﻿using NUnit.Framework;
using Simpler;
using Please.Bump.Tasks;
using Please.Bump.Models;

namespace Tests.Bump.Tasks
{
    [TestFixture]
    public class BumpVersionTest
    {
        [Test]
        public void should_bump_given_nuspec_file()
        {
            // Arrange
            var bump = Task.New<BumpVersion>();
            bump.In.BumpType = BumpType.Patch;
            bump.In.FileType = FileType.Nuspec;
            bump.In.FileName = @"C:\Somewhere\Foo.nuspec";
            bump.BumpAssemblyInfo = Fake.Task<BumpAssemblyInfo>();
            bump.BumpNuspec = Fake.Task<BumpNuspec>();
            bump.BumpScript = Fake.Task<BumpScript>();

            // Act
            bump.Execute();

            // Assert
            Assert.That(bump.BumpNuspec.Stats.ExecuteCount, Is.EqualTo(1));
            Assert.That(bump.BumpAssemblyInfo.Stats.ExecuteCount, Is.EqualTo(0));
            Assert.That(bump.BumpScript.Stats.ExecuteCount, Is.EqualTo(0));
        }

        [Test]
        public void should_bump_given_AssemblyInfo_file()
        {
            // Arrange
            var bump = Task.New<BumpVersion>();
            bump.In.BumpType = BumpType.Patch;
            bump.In.FileType = FileType.AssemblyInfo;
            bump.In.FileName = @"C:\Somewhere\AssemblyInfo.cs";
            bump.BumpAssemblyInfo = Fake.Task<BumpAssemblyInfo>();
            bump.BumpNuspec = Fake.Task<BumpNuspec>();
            bump.BumpScript = Fake.Task<BumpScript>();

            // Act
            bump.Execute();

            // Assert
            Assert.That(bump.BumpAssemblyInfo.Stats.ExecuteCount, Is.EqualTo(1));
            Assert.That(bump.BumpNuspec.Stats.ExecuteCount, Is.EqualTo(0));
            Assert.That(bump.BumpScript.Stats.ExecuteCount, Is.EqualTo(0));
        }

        [Test]
        public void should_bump_given_script_file()
        {
            // Arrange
            var bump = Task.New<BumpVersion>();
            bump.In.BumpType = BumpType.Patch;
            bump.In.FileType = FileType.Script;
            bump.In.FileName = @"C:\SomewhereElse\scriptC.exe";
            bump.BumpAssemblyInfo = Fake.Task<BumpAssemblyInfo>();
            bump.BumpNuspec = Fake.Task<BumpNuspec>();
            bump.BumpScript = Fake.Task<BumpScript>();

            // Act
            bump.Execute();

            // Assert
            Assert.That(bump.BumpScript.Stats.ExecuteCount, Is.EqualTo(1));
            Assert.That(bump.BumpNuspec.Stats.ExecuteCount, Is.EqualTo(0));
            Assert.That(bump.BumpAssemblyInfo.Stats.ExecuteCount, Is.EqualTo(0));
        }
    }
}