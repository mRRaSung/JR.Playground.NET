using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPattern.PipelineBreak
{
    public interface IOperation<T>
    {
        bool Invoke(T data);
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
        public bool Invoke(T data)
        {
            foreach (var operation in operations)
            {
                if (!operation.Invoke(data))
                    return false;
            }

            return true;
        }
    }

    public class Operation<T> : IOperation<T>
    {
        private readonly Func<T, bool> action;

        public Operation(Func<T, bool> action)
        {
            this.action = action;
        }

        public bool Invoke(T data) 
            => action(data);
    }

    public class UpperCaseOperation : IOperation<string>
    {
        public bool Invoke(string data)
        {
            Console.WriteLine($"The string is Uppercase : {data.ToUpper()}");
            return true;
        }
    }

    public static class PipelineDemo
    {
        public static void Execute()
        {
            // build
            var pipeline = new Pipeline<string>();

            // lambda
            pipeline.Register(new Operation<string>(str => 
            {
                Console.WriteLine($"The string {str} contains {str.Length} characters.");
                return true;
            }));

            // lambda
            pipeline.Register(new Operation<string>(str =>
            {
                if (str == "BBB")
                {
                    Console.WriteLine($"難吃 {str}");
                    return false;
                }

                Console.WriteLine($"好吃 {str}");
                return true;
            }));

            // class
            pipeline.Register(new UpperCaseOperation());


            List<Fruit> fruits = new List<Fruit>
            {
                new Fruit{ Id = Guid.NewGuid(), Name = "AAA", Age = 10 },
                new Fruit{ Id = Guid.NewGuid(), Name = "BBB", Age = 20 },
                new Fruit{ Id = Guid.NewGuid(), Name = "CCC", Age = 30 },
            };

            // execute

            fruits.ForEach(fruit => {
                pipeline.Invoke(fruit.Name);
                Console.WriteLine("-------------------");
            });
        }
    }

    public class Fruit
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }
    }
}
