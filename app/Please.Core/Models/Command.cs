using Simpler;
using Simpler.Core.Tasks;
using System;

namespace Please.Core.Models
{
    public class Command<TTask> : ICommand where TTask : Task
    {
        public string Name { get; set; }
        public Task Task { get; set; }
        public Action<TTask, string> Setup { get; set; }

        public void Run(string options)
        {
            if (Task == null)
            {
                var createTask = new CreateTask { In = { TaskType = typeof(TTask) } };
                createTask.Execute();
                Task = (TTask)createTask.Out.TaskInstance;
            }
            Setup((TTask)Task, options);
            Task.Execute();
        }
    }
}
