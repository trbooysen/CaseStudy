using ServiceA;
using Shared;
using System;
using System.Collections.Generic;
using System.Text;
using XUnitTestServiceA.Builders;
using XUnitTestServiceA.MockServices;

namespace XUnitTestServiceA
{
  /// <summary>
  /// All artifacts required for the test fixture can be created in this base class 
  /// </summary>
  public class BaseContextTestFixture
  {
    /// <summary>
    /// The message queue that will be tested
    /// </summary>
    public RabbitMQService RabbitMQSendService { get; private set; }
    public RabbitMQService RabbitMQReceiveService { get; private set; }

    public MockMessageQueueService MockMessageQueueService { get; private set; }

    /// <summary>
    /// Create the message queue services
    /// </summary>
    public BaseContextTestFixture()
    {
      var messageSendServiceBuilder = new MessageQueueServiceBuilder();
      RabbitMQSendService = messageSendServiceBuilder.Build();
      var messageReceiveServiceBuilder = new MessageQueueServiceBuilder();
      RabbitMQReceiveService = messageReceiveServiceBuilder.Build();

      //var mockMessageQueueServiceBuilder = new MockMessageQueueServiceBuilder();
      //MockMessageQueueService = mockMessageQueueServiceBuilder.Build();
    }
  }
}
