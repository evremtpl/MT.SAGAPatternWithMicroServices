using Microsoft.Extensions.Hosting;
using System.Reflection;
using System.Threading.Tasks;

using MassTransit;

using Microsoft.EntityFrameworkCore;

using MT.RabbitMq;
using MT.RabbitMqSaga.DbConfigurations;
using MT.RabbitMqSaga.StateMachine;


using Microsoft.Extensions.DependencyInjection;
using MassTransit.EntityFrameworkCoreIntegration;

namespace MT.SagaMachine
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string connectionString = "server=.\\MERVESERVER; database=SAGAStateMachineDb; user id=sa; password=951413Mt.X";

            var builder = new HostBuilder()
               .ConfigureServices((hostContext, services) =>
               {
                   services.AddMassTransit(cfg =>
                   {
                       cfg.AddSagaStateMachine<ReportStateMachine, ReportStateInstance>()

                        .EntityFrameworkRepository(r =>
                        {
                            r.ConcurrencyMode = ConcurrencyMode.Pessimistic; // or use Optimistic, which requires RowVersion

                            r.AddDbContext<DbContext, ReportStateDbContext>((provider, builder) =>
                            {
                                builder.UseSqlServer(connectionString
                                    , m =>
                                {
                                    m.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
                                    m.MigrationsHistoryTable($"__{nameof(ReportStateDbContext)}");
                                }
                                );
                            });
                        });

                       cfg.AddBus(provider => RabbitMqBus.ConfigureBus(provider));
                   });

                   services.AddMassTransitHostedService();
               });

            await builder.RunConsoleAsync();
        }
    }
}
