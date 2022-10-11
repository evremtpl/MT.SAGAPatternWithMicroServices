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

            //string connectionString = "server=.\\MERVESERVER; database=SAGAStateMachineDb; user id=sa; password=951413Mt.X";

            //var builder = new HostBuilder()
            //   .ConfigureServices((hostContext, services) =>
            //   {
            //       services.AddMassTransit(cfg =>
            //       {
            //           cfg.AddSagaStateMachine<ReportStateMachine, ReportStateInstance>()

            //            .EntityFrameworkRepository(r =>
            //            {
            //                r.ConcurrencyMode = ConcurrencyMode.Pessimistic; // or use Optimistic, which requires RowVersion

            //                r.AddDbContext<DbContext, ReportStateDbContext>((provider, builder) =>
            //                {
            //                    builder.UseSqlServer(connectionString
            //                        , m =>
            //                        {
            //                            m.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
            //                            m.MigrationsHistoryTable($"__{nameof(ReportStateDbContext)}");
            //                        }
            //                    );
            //                });
            //            });

            //           cfg.AddBus(provider => RabbitMqBus.ConfigureBus(provider));
            //       });


            //   });



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
