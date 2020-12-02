using Ninject;
using ServiceA;
using ServiceB;
using Shared;
using Shared.Interfaces;

namespace MSUnitTest.Builders
{
  /// <summary>
  /// Takes care of the creation of <see cref="Sender"/> and or <see cref="Receiver"/> objects.
  /// The class uses dependency injection to create instance of the required objects.
  /// </summary>
  public class ServicesBuilder
  {
    private IKernel container = new StandardKernel();
    private readonly ILogger loggerTX;
    private readonly ILogger loggerRX;
    private readonly IReceiveMessage receiveMessageService;
    private readonly ISendMessage sendMessageService;

    public ServicesBuilder()
    {
      configureContainer(container);
      loggerTX = container.Get<ILogger>();
      loggerRX = container.Get<ILogger>();
      receiveMessageService = container.Get<IReceiveMessage>();
      sendMessageService = container.Get<ISendMessage>();
    }

    /// <summary>
    /// Create the <see cref="Sender"/> object.
    /// </summary>
    /// <returns>The created <see cref="Sender"/> object</returns>
    public Sender BuildSender()
    {
      //Create the object
      return new Sender(loggerTX, sendMessageService);
    }

    /// <summary>
    /// Create the <see cref="Receiver"/> object.
    /// </summary>
    /// <returns>The created <see cref="Receiver"/> object</returns>
    public Receiver BuildReceiver()
    {
      //Create the object
      return new Receiver(loggerRX, receiveMessageService);
    }

    /// <summary>
    /// Get access to concrete classes using dependency injection
    /// </summary>
    /// <param name="container">The <see cref="IKernel"/> container</param>
    private void configureContainer(IKernel container)
    {
      container.Bind<IReceiveMessage>().To<RabbitMQService>();
      container.Bind<ISendMessage>().To<RabbitMQService>();
      container.Bind<ILogger>().To<MockLogger>();
    }

  }
}
