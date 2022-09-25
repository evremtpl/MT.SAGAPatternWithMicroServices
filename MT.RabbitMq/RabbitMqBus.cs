using MassTransit;
using MassTransit.RabbitMqTransport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.RabbitMq
{
    public class RabbitMqBus
    {
        private static readonly Lazy<RabbitMqBus> _Instance = new Lazy<RabbitMqBus>(() => new RabbitMqBus());

        public RabbitMqBus()
        {

        }
        public static RabbitMqBus Instance => _Instance.Value;
        public static IBusControl ConfigureBus(IServiceProvider provider, Action<IRabbitMqBusFactoryConfigurator, IRabbitMqHost>
         registrationAction = null)
        {
            return Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri(BusConstants.RabbitMqUri), hst =>
                {
                    hst.Username(BusConstants.UserName);
                    hst.Password(BusConstants.Password);
                });

                cfg.ConfigureEndpoints(provider);

                registrationAction?.Invoke(cfg, host);
            });
        }


        public IBusControl ConfigureBus(Action<IRabbitMqBusFactoryConfigurator, IRabbitMqHost>
 registrationAction = null)
        {
            return Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri(BusConstants.RabbitMqUri), hst =>
                {
                    hst.Username(BusConstants.UserName);
                    hst.Password(BusConstants.Password);
                });



                registrationAction?.Invoke(cfg, host);
            });
        }
    }
}
