using MassTransit;

using MassTransit.Saga;

using MT.RabbitMq;

using MT.RabbitMqSaga.StateMachine;
using System;
using System.Reflection;

namespace MT.StateMachineRun
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Saga";
            var orderSaga = new ReportStateMachine();
            var repo = new InMemorySagaRepository<ReportStateInstance>();

            var bus = RabbitMqBus.Instance
                 .ConfigureBus((cfg, host) =>
                 {
                     cfg.ReceiveEndpoint(BusConstants.SagaBusQueue, e =>
                     {
                         e.StateMachineSaga(orderSaga, repo);
                     });


                 });
            bus.StartAsync();
            Console.WriteLine("Report Saga Started");
            Console.ReadLine();

        }
    }
}
