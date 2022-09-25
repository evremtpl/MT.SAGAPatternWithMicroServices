using MassTransit;
using MT.RabbitMqMessage.Event;
using System.Threading.Tasks;

namespace MT.PersonService.API.Consumers
{
    public class ReportValidateConsumer :
      IConsumer<IReportValidateEvent>
    {
        public async Task Consume(ConsumeContext<IReportValidateEvent> context)
        {
            var data = context.Message;

            if (data.UUId==2)
            {
                await context.Publish<IReportCancelledEvent>(
          new { ReportId = context.Message.ReportId, UUId = context.Message.UUId });
            }
            else
            {
                // send to next microservice
            }
        }
    }
}
