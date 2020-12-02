using Ninject;
using Shared;
using Shared.Interfaces;

namespace XUnitTestServiceA.Builders
{
  /// <summary>
  /// The class that uses dependacy injection to create an instance of the <see cref="MessageQueueService"/>
  /// </summary>
  public class MessageQueueServiceBuilder
  {
    public IKernel Container = new StandardKernel();
    public MessageQueueServiceBuilder()
    {
      configureContainer();
    }

    public RabbitMQService Build()
    {
      //Create the context using IoC
      return Container.Get<RabbitMQService>();
    }

    private void configureContainer()
    {
      Container.Bind<ISendMessage>().To<RabbitMQService>();
      Container.Bind<IReceiveMessage>().To<RabbitMQService>();
      Container.Bind<ILogger>().To<Log4NetLogger<MessageQueueServiceBuilder>>();
    }
  }
}
