using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPattern.Pipeline
{
    class Group
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }
    }

    public interface IOperation<T>
    {
        void Invoke(T data);
    }

    public class Pipeline<T> : IOperation<T>
    {
        private readonly List<IOperation<T>> operations = new List<IOperation<T>>();

        // add operation at the end of the pipeline
        public void Register(IOperation<T> operation)
        {
            operations.Add(operation);
        }

        // invoke every operations
        public void Invoke(T data)
        {
            foreach (var operation in operations)
            {
                operation.Invoke(data);
            }
        }
    }

    public class UpperCaseOperation : IOperation<string>
    {
        public void Invoke(string data) 
            => Console.WriteLine($"The string is Uppercase : {data.ToUpper()}");
    }

    public class Operation<T> : IOperation<T>
    {
        private readonly Action<T> action;

        public Operation(Action<T> action)
        {
            this.action = action;
        }

        public void Invoke(T data) 
            => action(data);
    }

    public static class PipelineDemo
    {
        public static void Execute()
        {
            // build
            var pipeline = new Pipeline<string>();

            // lambda
            pipeline.Register(new Operation<string>(str => {
                Console.WriteLine($"The string {str} contains {str.Length} characters.");
            }));

            // class
            pipeline.Register(new UpperCaseOperation());

            // execute
            pipeline.Invoke("apple");
        }
    }
}
