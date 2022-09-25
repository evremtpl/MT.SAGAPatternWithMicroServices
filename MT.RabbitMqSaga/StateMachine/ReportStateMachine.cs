using Automatonymous;
using MassTransit;
using MT.RabbitMqMessage.Event;
using MT.RabbitMqSaga.DbConfigurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.RabbitMqSaga.StateMachine
{
    public class ReportStateMachine : MassTransitStateMachine<ReportStateInstance>
    {
        public ReportStateMachine()
        { 
                Event(() => ReportStartedEvent, x => x.CorrelateById(m => m.Message.ReportId));

                Event(() => ReportCancelledEvent, x => x.CorrelateById(m => m.Message.ReportId));

                InstanceState(x => x.CurrentState);

        Initially(
           When(ReportStartedEvent)
                .Then(context =>
                {
            context.Instance.ReportCreatedDate = DateTime.Now;
            context.Instance.ReportId = context.Data.ReportId;
            context.Instance.UUId = context.Data.UUId;
            
        })
               .TransitionTo(ReportStarted)
               .Publish(context => new ReportValidateEvent(context.Instance)));

            During(ReportStarted,
                When(ReportCancelledEvent)
                    .Then(context => context.Instance.ReportCancelledDate =
                        DateTime.Now)
                     .TransitionTo(ReportCancelled)
                
              );
        }



            public State ReportStarted { get; private set; }
            public State ReportCancelled { get; private set; }

            public Event<IReportStartedEvent> ReportStartedEvent { get; private set; }
            public Event<IReportCancelledEvent> ReportCancelledEvent { get; private set; }
    }
    
}
