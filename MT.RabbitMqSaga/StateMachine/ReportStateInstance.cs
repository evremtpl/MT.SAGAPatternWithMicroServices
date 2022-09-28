using Automatonymous;
using System;


namespace MT.RabbitMqSaga.StateMachine
{
    
        public class ReportStateInstance : SagaStateMachineInstance
        {
            public Guid CorrelationId { get; set; }
            public string CurrentState { get; set; }
            public string ReportId { get; set; }
            public int UUId { get; set; }


            public DateTime? ReportCreatedDate { get; set; }
            public DateTime? ReportCancelledDate { get; set; }


        }
    
}
