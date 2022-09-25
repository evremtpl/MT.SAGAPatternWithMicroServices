
using MassTransit.EntityFrameworkCoreIntegration;
using MassTransit.EntityFrameworkCoreIntegration.Mappings;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;


namespace MT.RabbitMqSaga.DbConfigurations
{
    public class ReportStateDbContext : SagaDbContext
    {
        public ReportStateDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override IEnumerable<ISagaClassMap> Configurations
        {
            get { yield return new ReportStateMap();}
        }
    }
}
