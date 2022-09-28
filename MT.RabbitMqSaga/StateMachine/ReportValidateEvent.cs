
using MT.RabbitMqMessage.Event;

using System;


namespace MT.RabbitMqSaga.StateMachine
{
    public class ReportValidateEvent : IReportValidateEvent
    {

        private readonly ReportStateInstance _reportStateInstance;

        public ReportValidateEvent(ReportStateInstance reportStateInstance)
        {
            _reportStateInstance = reportStateInstance;
        }
        
        public string ReportId => _reportStateInstance.ReportId;

        public int UUId => _reportStateInstance.UUId;

        public Guid CorrelationId => _reportStateInstance.CorrelationId;


    }
}
