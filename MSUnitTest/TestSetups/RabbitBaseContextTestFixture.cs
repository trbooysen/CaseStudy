using MSUnitTest.Builders;
using Shared;

namespace MSUnitTest
{
  /// <summary>
  /// All artifacts required for the test fixture can be created in this base class 
  /// </summary>
  public class RabbitBaseContextTestFixture
  {
    /// <summary>
    /// The send queue that will be tested
    /// </summary>
    public RabbitMQService RabbitMQSendService { get; private set; }
    
    /// <summary>
    /// The receive queue that will be tested
    /// </summary>
    public RabbitMQService RabbitMQReceiveService { get; private set; }

    /// <summary>
    /// Create the message queue services
    /// </summary>
    public RabbitBaseContextTestFixture()
    {
      var messageSendServiceBuilder = new RabbitMQServiceBuilder();
      RabbitMQSendService = messageSendServiceBuilder.Build();
      var messageReceiveServiceBuilder = new RabbitMQServiceBuilder();
      RabbitMQReceiveService = messageReceiveServiceBuilder.Build();
    }
  }

}
