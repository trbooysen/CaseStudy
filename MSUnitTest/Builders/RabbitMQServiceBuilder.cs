using Ninject;
using Shared;
using Shared.Interfaces;

namespace MSUnitTest.Builders
{
  /// <summary>
  /// Takes care of the creation of a <see cref="RabbitMQService"/> object.
  /// The class uses dependency injection to create instance of the required objects.
  /// </summary>
  public class RabbitMQServiceBuilder
  {
    private IKernel container = new StandardKernel();
    public RabbitMQServiceBuilder()
    {
      configureContainer(container);
    }

    /// <summary>
    /// Create the <see cref="RabbitMQService"/> object.
    /// </summary>
    /// <returns>The created <see cref="RabbitMQService"/> object</returns>
    public RabbitMQService Build()
    {
      //Create the context using IoC
      return container.Get<RabbitMQService>();
    }

    /// <summary>
    /// Get access to concrete classes using dependency injection
    /// </summary>
    /// <param name="container">The <see cref="IKernel"/> container</param>
    private void configureContainer(IKernel container)
    {
      container.Bind<ISendMessage>().To<RabbitMQService>();
      container.Bind<IReceiveMessage>().To<RabbitMQService>();
      container.Bind<ILogger>().To<Log4NetLogger<RabbitMQServiceBuilder>>();
    }
  }
}
