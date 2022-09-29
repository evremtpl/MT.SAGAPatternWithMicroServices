using Automatonymous;
using MassTransit;
using MT.RabbitMqMessage.Event;

using System;


namespace MT.RabbitMqSaga.StateMachine
{
    public class ReportStateMachine : MassTransitStateMachine<ReportStateInstance>
    {
        public ReportStateMachine()
        { 
                Event(() => ReportStartedEvent, x => x.CorrelateBy(state => state.ReportId, context=> context.Message.ReportId)
            .SelectId(s => Guid.NewGuid()));
                Event(() => ReportCancelledEvent, x => x.CorrelateById(m => m.Message.CorrelationId));

                InstanceState(x => x.CurrentState);

        Initially(
           When(ReportStartedEvent)
                .Then(context =>
                {
                    context.Instance.ReportCreatedDate = DateTime.Now;
                    context.Instance.ReportId = context.Data.ReportId;
                    context.Instance.UUId = context.Data.UUId;

                })
                 .ThenAsync(
                 context => Console.Out.WriteLineAsync($" {context.Data.ReportId} report Id is received..")

                 )
               .TransitionTo(ReportStarted)
               .Publish(context => new ReportValidateEvent(context.Instance)));

            During(ReportStarted,
                When(ReportCancelledEvent)
                    .Then(context => context.Instance.ReportCancelledDate =
                        DateTime.Now)
                    .ThenAsync(
                    context => Console.Out.WriteLineAsync($" {context.Data.ReportId} report Id is cancelled.."))
                     .TransitionTo(ReportCancelled)
                .Finalize()
              );
            SetCompletedWhenFinalized();
        }



            public State ReportStarted { get; private set; }
            public State ReportCancelled { get; private set; }

            public Event<IReportStartedEvent> ReportStartedEvent { get; private set; }
            public Event<IReportCancelledEvent> ReportCancelledEvent { get; private set; }
    }
    
}
