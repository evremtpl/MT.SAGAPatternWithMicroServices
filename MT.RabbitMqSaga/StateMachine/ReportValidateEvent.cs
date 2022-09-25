
using MT.RabbitMqMessage.Event;
using MT.RabbitMqSaga.DbConfigurations;
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
        
        public Guid ReportId => _reportStateInstance.ReportId;

        public int UUId => _reportStateInstance.UUId;

        

        
    }
}
