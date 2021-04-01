using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPattern
{
    public interface ITask
    {
        void Start();

        void Process();
    }

    public class ProductTask : ITask
    {
        public void Start()
        {
            Console.WriteLine("Start Product");
        }

        public void Process()
        {
            Console.WriteLine("Process Product");
        }
    }

    public class KeywordTask : ITask
    {
        public void Start()
        {
            Console.WriteLine("Start Keyword");
        }

        public void Process()
        {
            Console.WriteLine("Process Keyword");
        }
    }

    public abstract class TaskCommand
    {
        protected ITask _task;

        public TaskCommand(ITask task)
        {
            _task = task;
        }

        public abstract void Execute();
    }

    public class ProductCommand : TaskCommand
    {
        public ProductCommand(ITask task) : base(task) {}

        public override void Execute()
        {
            _task.Start();
        }
    }

    public class KeywordCommand : TaskCommand
    {
        public KeywordCommand(ITask task) : base(task) { }

        public override void Execute()
        {
            _task.Start();
        }
    }

    public class CommandInvoker
    {
        List<TaskCommand> commands = new List<TaskCommand>();

        public void SetCommand(TaskCommand command)
        {
            commands.Add(command);
        }

        public void Execute()
        {
            foreach (TaskCommand command in commands)
                command.Execute();
        }
    }

    public static class Command
    {
        public static void Run()
        {
            TaskCommand commadProduct = new ProductCommand(new ProductTask());
            TaskCommand commadKeyword = new KeywordCommand(new KeywordTask());

            CommandInvoker invoker = new CommandInvoker();
            invoker.SetCommand(commadProduct);
            invoker.SetCommand(commadKeyword);
            invoker.Execute();
        }
    }
}
