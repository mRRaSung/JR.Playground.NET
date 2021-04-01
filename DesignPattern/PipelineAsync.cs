using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DesignPattern.PipelineAsync
{
    public interface IOperation<T>
    {
        void SetNext(IOperation<T> next);

        void Invoke(T data);
    }

    //Collection
    public class Pipeline<T> : IOperation<T>
    {
        private readonly List<IOperation<T>> operations = new List<IOperation<T>>();

        // empty last
        private readonly IOperation<T> operationEnd = new Operation<T>(data => true);

        public Pipeline() {}

        void IOperation<T>.SetNext(IOperation<T> next)
        {
            operationEnd.SetNext(next);
        }

        // append an operation at the end of the pipeline
        public void RegisterOperation(IOperation<T> operation)
        {
            // when the operation is finished, it will call terminate
            operation.SetNext(operationEnd);

            // link the last registered operation with the newly registered one
            if (operations.Any())
                operations.Last().SetNext(operation);

            operations.Add(operation);
        }

        public void Invoke(T data)
        {
            //from first or end directly
            IOperation<T> operation = operations.FirstOrDefault() ?? operationEnd;
            operation.Invoke(data);
        }
    }

    //Single
    public class Operation<T> : IOperation<T>
    {
        private readonly Func<T, bool> _action;

        private IOperation<T> _next;

        public Operation(Func<T, bool> action)
        {
            _action = action;
        }

        public void SetNext(IOperation<T> next)
        {
            this._next = next;
        }

        public void Invoke(T data)
        {
            if (_action(data)) 
                _next?.Invoke(data);
        }
    }

    public class WriteOperation : IOperation<string>
    {
        private IOperation<string> _next;

        public void SetNext(IOperation<string> next)
        {
            _next = next;
        }

        public void Invoke(string data)
        {
            Task.Run(() =>
            {
                Console.WriteLine("Writing data to the disk...");
                Thread.Sleep(100); // just kidding !
                Console.WriteLine("Data successfully written to the disk !");

                _next?.Invoke(data);
            });
        }
    }

    public static class PipelineDemo
    {
        public static void Execute()
        {
            // the main pipeline
            var pipeline = new Pipeline<string>();
            pipeline.RegisterOperation(new Operation<string>(data =>
            {
                Console.WriteLine($"Everyone likes {data} !");
                return true;
            }));
            pipeline.RegisterOperation(new WriteOperation());
            //pipeline.RegisterOperation(new Operation<string>(data => false));
            //pipeline.RegisterOperation(new Operation<string>(data => Console.WriteLine("This operation should not be called !")));

            //// a verbose pipeline to wrap the main pipeline
            var verbose = new Pipeline<string>();
            verbose.RegisterOperation(new Operation<string>(data =>
            {
                Console.WriteLine("Beginning of the pipeline...");
                return true;
            }));
            verbose.RegisterOperation(pipeline);
            //verbose.RegisterOperation(new Operation<string>(data => Console.WriteLine("End of the pipeline...")));
            verbose.Invoke("banana");
            //pipeline.Invoke("banana");

            Console.WriteLine("The pipeline is asynchronous, so we should have more messages after this one : ");
        }

        public static void JobExecute()
        {
            List<Client> clients = new List<Client>
            {
                new Client{ Id = Guid.NewGuid(), Name = "AAA", Age = 10 },
                new Client{ Id = Guid.NewGuid(), Name = "BBB", Age = 20 },
                new Client{ Id = Guid.NewGuid(), Name = "CCC", Age = 30 },
            };

            Pipeline<Client> jobs = new Pipeline<Client>();
            jobs.RegisterOperation(new ProductOperation());
            jobs.RegisterOperation(new KeywordOperation());

            clients.ForEach(client 
                => jobs.Invoke(client));
        }
    }

    public class Client
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }
    }

    public class ProductOperation : IOperation<Client>
    {
        private IOperation<Client> _next;

        public void SetNext(IOperation<Client> next)
        {
            _next = next;
        }

        public void Invoke(Client data)
        {
            Console.WriteLine("ProductOperation => {0}", data.Name);
            _next?.Invoke(data);
        }
    }
    public class KeywordOperation : IOperation<Client>
    {
        private IOperation<Client> _next;

        public void SetNext(IOperation<Client> next)
        {
            _next = next;
        }

        public void Invoke(Client data)
        {
            Console.WriteLine("KeywordOperation => {0}", data.Name);
            _next?.Invoke(data);
        }
    }
}
