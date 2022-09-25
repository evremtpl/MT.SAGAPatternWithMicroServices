using System;


namespace MT.RabbitMqMessage
{
    public interface IStartReport
    {
        public Guid ReportId { get;  }
        public int UUId { get;  }
      
    }
}
