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
                Assert.That(runSql.In.Extensions.Length, Is.EqualTo(1));
                Assert.That(runSql.In.Extensions[0], Is.EqualTo(".sql"));
                Assert.That(runSql.In.WithVersioning, Is.True);
            }
        }

        [Test]
        public void should_send_input_to_run_py()
        {
            dynamic inputs = fixtures.RunSql.Inputs;
            foreach (var input in inputs)
            {
                var commandText = String.Format("run py {0}", input);
                RunSomething runSql = ShouldExecute<RunSomething>(commandText);

                Assert.That(runSql.Stats.ExecuteCount, Is.EqualTo(1));
                Assert.That(runSql.In.ConnectionName, Is.EqualTo(input.ConnectionName));
                Assert.That(runSql.In.Directory, Is.EqualTo(input.Directory));
                Assert.That(runSql.In.Extensions.Length, Is.EqualTo(1));
                Assert.That(runSql.In.Extensions[0], Is.EqualTo(".py"));
                Assert.That(runSql.In.WithVersioning, Is.True);
            }
        }

        [Test]
        public void should_send_input_to_run_all()
        {
            dynamic inputs = fixtures.RunSql.Inputs;
            foreach (var input in inputs)
            {
                var commandText = String.Format("run all {0}", input);
                RunSomething runSql = ShouldExecute<RunSomething>(commandText);

                Assert.That(runSql.Stats.ExecuteCount, Is.EqualTo(1));
                Assert.That(runSql.In.ConnectionName, Is.EqualTo(input.ConnectionName));
                Assert.That(runSql.In.Directory, Is.EqualTo(input.Directory));
                Assert.That(runSql.In.Extensions.Length, Is.EqualTo(2));
                Assert.That(runSql.In.Extensions, Contains.Item(".sql"));
                Assert.That(runSql.In.Extensions, Contains.Item(".py"));
                Assert.That(runSql.In.WithVersioning, Is.True);
            }
        }

        [Test]
        public void should_send_input_to_timestamp()
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
