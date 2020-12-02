using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;

namespace MSUnitTest
{

  [TestClass]
  public class RabitMQtests : RabbitBaseContextTestFixture
  {
    /// <summary>
    /// Tests the construction including all dependencies, and channel setup.
    /// </summary>
    [TestMethod]
    public void RabbitMQServiceIsNotNull()
    {
      Assert.IsNotNull(RabbitMQSendService);
      Assert.IsNotNull(RabbitMQReceiveService);
    }


    [TestMethod]
    public void MessageQueueServiceSetupMessageListener()
    {
      Assert.IsTrue(RabbitMQReceiveService.SetupMessageListener());
    }


    [TestMethod]
    [Timeout(4000)]
    public void MessageQueueServiceReceiveMessageEvent()
    {
      var locker = new object();
      EventWaitHandle waitHandle = new AutoResetEvent(false);

      var result = RabbitMQSendService.SendMessage("test");
      Assert.IsTrue(result);

      var called = false;
      result = RabbitMQReceiveService.SetupMessageListener();
      RabbitMQReceiveService.MessageReceived += (sender, args) =>
      {
        lock (locker)
        {
          called = true;
          Assert.AreEqual(args.Message, "test");
          waitHandle.Set();
        }
      };

      waitHandle.WaitOne();
      Assert.IsTrue(result);
      Assert.IsTrue(called);
    }


    //Todo test failures and services not started like erlang

  }
}
