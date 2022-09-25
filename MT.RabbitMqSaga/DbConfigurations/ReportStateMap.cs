using MassTransit;
using MassTransit.EntityFrameworkCoreIntegration.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace MT.RabbitMqSaga.DbConfigurations
{
    public class ReportStateMap : SagaClassMap<ReportStateInstance>
    {
        protected override void Configure(EntityTypeBuilder<ReportStateInstance> entity, ModelBuilder model)
        {
            entity.Property(x => x.CurrentState).HasMaxLength(64);
            entity.Property(x => x.ReportCreatedDate);
            
        }
    }
}
