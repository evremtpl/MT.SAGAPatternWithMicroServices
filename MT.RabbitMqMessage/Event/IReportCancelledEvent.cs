﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.RabbitMqMessage.Event
{
    public interface IReportCancelledEvent
    {
        public Guid CorrelationId { get; }
        public string ReportId { get;  }
        public int UUId { get;  }
    
    }
}
