using Automatonymous;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.RabbitMqSaga.DbConfigurations
{
    public class ReportStateInstance : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }
        public string CurrentState { get; set; }
        public Guid ReportId { get; set; }
        public int UUId { get; set; }

    
        public DateTime? ReportCreatedDate { get; set; }
        public DateTime? ReportCancelledDate { get; set; }


    }
}
