using System;
using System.IO;
using Library;
using Please.Timestamp.Tasks;
using NUnit.Framework;
using Simpler;
using Please.Bump.Tasks;
using Please.Run.Tasks;

namespace Tests
{
    [TestFixture]
    public class MainTests
    {
        dynamic fixtures = Centroid.Config.FromFile("fixtures.json");

        [Test]
        public void should_send_input_to_bump_version()
        {
            dynamic inputs = fixtures.Bump.Inputs;
            foreach (var input in inputs)
            {
                var commandText = String.Format("bump version {0}", input);
                var bump = ShouldExecute<BumpVersion>(commandText);

                Assert.That(bump.Stats.ExecuteCount, Is.EqualTo(1));
                Assert.That(bump.In.BumpType.ToString(), Is.EqualTo(input.BumpType));
                Assert.That(bump.In.FileType.ToString(), Is.EqualTo(input.FileType));
                Assert.That(bump.In.FileName, Is.EqualTo(input.fileName));
            }
        }

        [Test]
        public void should_send_input_to_run_sql()
        {
            dynamic inputs = fixtures.RunSql.Inputs;
            foreach (var input in inputs)
            {
                var commandText = String.Format("run sql {0}", input);
                RunSomething runSql = ShouldExecute<RunSomething>(commandText);

                Assert.That(runSql.Stats.ExecuteCount, Is.EqualTo(1));
                Assert.That(runSql.In.ConnectionName, Is.EqualTo(input.ConnectionName));
                Assert.That(runSql.In.Directory, Is.EqualTo(input.Directory));
                Assert.That(runSql.In.Extensions[0], Is.EqualTo(".sql"));
                Assert.That(runSql.In.WithVersioning, Is.True);
            }
        }

        [Test]
        public void should_run_sql_with_versioning()
        {
            var run = ShouldExecute<RunSomething>("run sql with versioning");

            Assert.That(run.Stats.ExecuteCount, Is.EqualTo(1));
            Assert.That(run.In.WithVersioning, "Expected with versioning to be true.");
        }

        [Test]
        public void should_run_sql_without_versioning()
        {
            var run = ShouldExecute<RunSomething>("run sql");

            Assert.That(run.Stats.ExecuteCount, Is.EqualTo(1));
            Assert.That(!run.In.WithVersioning, "Expected with versioning to be false.");
        }

        [Test]
        public void should_run_sql_on_database()
        {
            var run = ShouldExecute<RunSomething>("run sql on DEV");

            Assert.That(run.Stats.ExecuteCount, Is.EqualTo(1));
            Assert.That(run.In.ConnectionName, Is.EqualTo("DEV"));
        }

        [Test]
        public void should_run_sql_in_directory()
        {
            var directories =
                new[]
                    {
                        @"SomeDirectory",
                        @"Some\Directory",
                        @".\SomeDirectory",
                        @"\\SomeDirectory",
                        @"c:\SomeDirectory",
                        @"Some Directory",
                        @".\Some Directory",
                        @"c:\Some Directory"
                    };

            foreach (var directory in directories)
            {
                var commandText = String.Format("run sql in {0} on DEV", directory);
                var run = ShouldExecute<RunSomething>(commandText);

                Assert.That(run.Stats.ExecuteCount, Is.EqualTo(1));
                Assert.That(run.In.Directory, Is.EqualTo(directory));
            }
        }

        [Test]
        public void should_run_sql_file()
        {
            var directories =
                new[]
                    {
                        @"SomeDirectory\test.sql",
                        @"Some\Directory\test.sql",
                        @".\SomeDirectory\test.sql",
                        @"\\SomeDirectory\test.sql",
                        @"c:\SomeDirectory\test.sql",
                        @"Some Directory\test.sql",
                        @".\Some Directory\test.sql",
                        @"c:\Some Directory\test.sql"
                    };

            foreach (var directory in directories)
            {
                var commandText = String.Format("run sql file {0} on DEV", directory);
                var run = ShouldExecute<RunSomething>(commandText);

                Assert.That(run.Stats.ExecuteCount, Is.EqualTo(1));
                Assert.That(run.In.File, Is.EqualTo(directory));
            }
        }

        [Test]
        public void should_run_sql_include_whitelist_in_directory()
        {
            const string whitelistFile = @".\whitelist.txt";
            var run = ShouldExecute<RunSomething>(@"run sql include " + whitelistFile + @" in .\Directory");

            Assert.That(run.Stats.ExecuteCount, Is.EqualTo(1));
            Assert.That(run.In.WhitelistFile, Is.EqualTo(whitelistFile));
        }

        [Test]
        public void should_run_py_with_versioning()
        {
            var run = ShouldExecute<RunSomething>("run py with versioning on DEV");

            Assert.That(run.Stats.ExecuteCount, Is.EqualTo(1));
            Assert.That(run.In.WithVersioning, "Expected with versioning to be true.");
        }

        [Test]
        public void should_run_py_without_versioning()
        {
            var run = ShouldExecute<RunSomething>("run py");

            Assert.That(run.Stats.ExecuteCount, Is.EqualTo(1));
            Assert.That(!run.In.WithVersioning, "Expected with versioning to be false.");
        }

        [Test]
        public void should_run_py_in_directory()
        {
            var directories =
                new[]
                    {
                        @"SomeDirectory",
                        @"Some\Directory",
                        @".\SomeDirectory",
                        @"\\SomeDirectory",
                        @"c:\SomeDirectory",
                        @"Some Directory",
                        @".\Some Directory",
                        @"c:\Some Directory"
                    };

            foreach (var directory in directories)
            {
                var commandText = String.Format("run py in {0}", directory);
                var run = ShouldExecute<RunSomething>(commandText);

                Assert.That(run.Stats.ExecuteCount, Is.EqualTo(1));
                Assert.That(run.In.Directory, Is.EqualTo(directory));
            }
        }

        [Test]
        public void should_run_py_file()
        {
            var directories =
                new[]
                    {
                        @"SomeDirectory\test.py",
                        @"Some\Directory\test.py",
                        @".\SomeDirectory\test.py",
                        @"\\SomeDirectory\test.py",
                        @"c:\SomeDirectory\test.py",
                        @"Some Directory\test.py",
                        @".\Some Directory\test.py",
                        @"c:\Some Directory\test.py"
                    };

            foreach (var directory in directories)
            {
                var commandText = String.Format("run py file {0}", directory);
                var run = ShouldExecute<RunSomething>(commandText);

                Assert.That(run.Stats.ExecuteCount, Is.EqualTo(1));
                Assert.That(run.In.File, Is.EqualTo(directory));
            }
        }

        [Test]
        public void should_run_all_with_versioning()
        {
            var run = ShouldExecute<RunSomething>("run all with versioning on DEV");

            Assert.That(run.Stats.ExecuteCount, Is.EqualTo(1));
            Assert.That(run.In.WithVersioning, "Expected with versioning to be true.");
        }

        [Test]
        public void should_run_all_without_versioning()
        {
            var run = ShouldExecute<RunSomething>("run all");

            Assert.That(run.Stats.ExecuteCount, Is.EqualTo(1));
            Assert.That(!run.In.WithVersioning, "Expected with versioning to be false.");
        }

        [Test]
        public void should_run_all_in_directory()
        {
            var directories =
                new[]
                    {
                        @"SomeDirectory",
                        @"Some\Directory",
                        @".\SomeDirectory",
                        @"\\SomeDirectory",
                        @"c:\SomeDirectory",
                        @"Some Directory",
                        @".\Some Directory",
                        @"c:\Some Directory"
                    };

            foreach (var directory in directories)
            {
                var commandText = String.Format("run all in {0}", directory);
                var run = ShouldExecute<RunSomething>(commandText);

                Assert.That(run.Stats.ExecuteCount, Is.EqualTo(1));
                Assert.That(run.In.Directory, Is.EqualTo(directory));
            }
        }

        [Test]
        public void should_run_py_include_whitelist_in_directory()
        {
            const string whitelistFile = @".\whitelist.txt";
            var run = ShouldExecute<RunSomething>(@"run sql include " + whitelistFile + @" in .\Directory");

            Assert.That(run.Stats.ExecuteCount, Is.EqualTo(1));
            Assert.That(run.In.WhitelistFile, Is.EqualTo(whitelistFile));
        }

        [Test]
        public void should_add_timestamp_in_directory()
        {
            dynamic inputs = fixtures.Directory.Inputs;
            foreach (var input in inputs)
            {
                var commandText = String.Format("add timestamp {0}", input);
                var addTimestamp = ShouldExecute<AddTimestamp>(commandText);

                Assert.That(addTimestamp.Stats.ExecuteCount, Is.EqualTo(1));
                Assert.That(addTimestamp.In.Directory, Is.EqualTo(input.Directory));
            }
        }

        [Test]
        public void should_return_0_on_success()
        {
            foreach (var command in Commands.All)
            {
                command.Task = Fake.Task<RunSomething>();
            }

            var main = Task.New<Main>();
            main.In.Args = "run sql".Split(' ');
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                main.Execute();
            }

            Assert.That(main.Out.ExitCode, Is.EqualTo(0));
        }

        [Test]
        public void should_return_1_on_failure()
        {
            foreach (var command in Commands.All)
            {
                command.Task = Fake.Task<RunSomething>();
            }

            var main = Task.New<Main>();
            main.In.Args = "this is wrong".Split(' ');
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                main.Execute();
            }

            Assert.That(main.Out.ExitCode, Is.EqualTo(1));
        }

        static TTask ShouldExecute<TTask>(string commandText) where TTask : Task
        {
            foreach (var command in Commands.All)
            {
                command.Task = Fake.Task<TTask>();
            }

            var main = Task.New<Main>();
            main.In.Args = commandText.Split(' ');
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                main.Execute();
            }

            var task = main.Out.Command.Task as TTask;
            if (task == null) throw new Exception("Unexpected command task was found.");
            return task;
        }
    }
}
