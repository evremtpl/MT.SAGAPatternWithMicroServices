using System;


namespace MT.RabbitMqMessage
{
    public interface IStartReport
    {
     
        public string ReportId { get; }
        public int UUId { get; }

    }
}
