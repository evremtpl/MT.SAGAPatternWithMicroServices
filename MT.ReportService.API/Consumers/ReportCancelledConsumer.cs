using MassTransit;
using MT.RabbitMqMessage.Event;
using MT.ReportService.Core.Entity;
using MT.ReportService.Core.Interfaces.Services;
using System.Threading.Tasks;

namespace MT.ReportService.API.Consumers
{
    public class ReportCancelledConsumer : IConsumer<IReportCancelledEvent>
    {
        private readonly IGenericService<Report> _reportService;
        public ReportCancelledConsumer(IGenericService<Report> reportService)
        {
            _reportService = reportService;
        }
        public async Task Consume(ConsumeContext<IReportCancelledEvent> context)
        {
            var data = context.Message;
            Report report = new Report()
            {
                ReportState=FileStatus.Cancelled,
                ReportId= data.ReportId,
                UUID= data.UUId,

            };
            _reportService.Delete(report); 
        }
    }
}
