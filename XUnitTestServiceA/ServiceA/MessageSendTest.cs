
using System.Threading;
using Xunit;

namespace XUnitTestServiceA.ServiceA
{
  public class MessageSendTest : BaseContextTestFixture
  {

    [Fact]
    public void MessageQueueServiceIsNotNull()
    {
      Xunit.Assert.NotNull(RabbitMQSendService);
    }

    [Fact]
    public void MockMessageQueueServiceIsNotNull()
    {
      Xunit.Assert.NotNull(MockMessageQueueService);
    }

    [Fact]
    public void MessageQueueServiceSetupMessageListener()
    {
      bool result = RabbitMQReceiveService.SetupMessageListener();
      Xunit.Assert.True(result);
    }

    [Fact]
    public void MessageQueueServiceReceiveEvent()
    {
      var locker = new object();
      EventWaitHandle waitHandle = new AutoResetEvent(false);

      var result = RabbitMQSendService.SendMessage("test");
      Assert.True(result);

      var called = false;
      result = RabbitMQReceiveService.SetupMessageListener();
      RabbitMQReceiveService.MessageReceived += (sender, args) =>
      {
        lock (locker)
        {
          called = true;
          waitHandle.Set();
        }
      };

      while (true)
      {
        waitHandle.WaitOne();
        Assert.True(result);
        Assert.True(called);
      }
    }

    [Fact]
    public void MockMessageQueueServiceSendMessage()
    {
      Xunit.Assert.True(MockMessageQueueService.SendMessage("Mock message"));
    }

  }
}
