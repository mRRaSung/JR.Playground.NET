using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Workflow
{
    public class Program
    {
        static void Main(string[] args)
        {
            //Configuration
            IServiceCollection services = new ServiceCollection();
            services.AddLogging();
            services.AddWorkflow();

            //Service
            IServiceProvider provider = services.BuildServiceProvider();

            //Workflow get and trigger
            IWorkflowHost host = provider.GetService<IWorkflowHost>();
            host.RegisterWorkflow<HelloWorldWorkflow, MyData>();
            host.Start();

            //Start flow which by flowId
            host.StartWorkflow("HelloWorld", new MyData { Value1 = 1, Value2 = 99 });

            Console.ReadKey();

            host.Stop();
        }
    }

    public class MyData
    {
        public int Value1 { get; set; }
        public int Value2 { get; set; }
        public int Value3 { get; set; }
    }

    public class HelloWorldWorkflow : IWorkflow<MyData>
    {
        public string Id => "HelloWorld";

        public int Version => 1;

        public void Build(IWorkflowBuilder<MyData> builder)
        {
            //builder
            //    .StartWith<AddNumbers>()
            //    .Then<GoodbyeWorld>();

            //builder
            //.StartWith<AddNumbers>()
            //    .Input(step => step.Input1, data => data.Value1)
            //    .Input(step => step.Input2, data => data.Value2)
            //    .Output(data => data.Value3, step => step.Output)
            //.Then<CustomMessage>()
            //    .Name("Print custom message")
            //    .Input(step => step.Message, data => "The answer is " + data.Value3.ToString());

            var branch1 = builder.CreateBranch()
                                 .StartWith<AddNumbers>()
                                 .Input(step => step.Input1, data => data.Value1)
                                 .Input(step => step.Input2, data => data.Value2)
                                 .Output(data => data.Value3, step => step.Output);

            var branch2 = builder.CreateBranch()

                                 .StartWith<CustomMessage>()
                                 .Input(cust => cust.Message, data => data.Value3.ToString());

            builder
                .StartWith<HelloWorld>()
                .Decide(data => data.Value3)
                    .Branch(1, branch1)
                    .Branch(2, branch2);
        }
    }

    public class CustomMessage : StepBody
    {
        public string Message { get; set; }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Console.WriteLine(Message);
            return ExecutionResult.Next();
        }
    }

    public class AddNumbers : StepBody
    {
        public int Input1 { get; set; }

        public int Input2 { get; set; }

        public int Output { get; set; }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Output = (Input1 + Input2);
            return ExecutionResult.Next();
        }
    }

    public class HelloWorld : StepBody
    {
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Console.WriteLine("Hello world");
            return ExecutionResult.Next();
        }
    }

    public class GoodbyeWorld : StepBody
    {
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Console.WriteLine("Goodbye world");
            return ExecutionResult.Next();
        }
    }




    public class DailyUpdateWorkflow : IWorkflow
    {
        public string Id => throw new NotImplementedException();

        public int Version => throw new NotImplementedException();



        public void Build(IWorkflowBuilder<object> builder)
        {
            //builder
                //.StartWith<ProductFlow>()
                    
        }
    }

    public class GroupDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }

    public class ProductFlow : StepBody
    {
        public GroupDTO Info { get; set; }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Console.WriteLine($"Product|{0}|{1}", Info.Id, Info.Name);
            return ExecutionResult.Next();
        }
    }

    public class KeywordFlow : StepBody
    {
        public GroupDTO Info { get; set; }

        public KeywordFlow() //P4PContext context, AlibabaRemote serviceRemote
        {
            //P4PContext context, AlibabaRemote serviceRemote
        }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Console.WriteLine($"Keyword|{0}|{1}", Info.Id, Info.Name);
            return ExecutionResult.Next();
        }
    }
}
