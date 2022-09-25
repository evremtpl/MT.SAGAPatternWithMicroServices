using MassTransit;
using MT.RabbitMqMessage;
using MT.RabbitMqMessage.Event;
using System.Threading.Tasks;

namespace MT.ReportService.API.Consumers
{
    public class StartReportConsumer : IConsumer<IStartReport>
    {
        public StartReportConsumer()
        {

        }
        public async Task Consume(ConsumeContext<IStartReport> context)
        {
            await context.Publish<IReportStartedEvent>(new
            {
                context.Message.ReportId
               ,
                context.Message.UUId
            

            });
        }
    }
}
