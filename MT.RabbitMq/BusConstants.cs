

namespace MT.RabbitMq
{
    public class BusConstants
    {
        public const string RabbitMqUri = "rabbitmq://localhost/";
        public const string UserName = "guest";
        public const string Password = "guest";
        public const string ReportQueue = "validate-report-queue";
        public const string SagaBusQueue = "saga-bus-queue";
        public const string StartReportQueue = "start-report";
    }
}
