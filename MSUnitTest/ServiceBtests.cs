using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceB;
using Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;

namespace MSUnitTest
{
  [TestClass]
  public class ServiceBtests: ServicesBaseContextTestFixture
  {
    [TestMethod]
    public void ReceiverNullLoggerGuards()
    {
      Mock<IReceiveMessage> mockService = new Mock<IReceiveMessage>();
      Assert.ThrowsException<NotImplementedException>(() => new Receiver(null, mockService.Object));
    }

    [TestMethod]
    public void ReceiverNullServiceGuards()
    {
      Mock<ILogger> mockLogger = new Mock<ILogger>();
      Assert.ThrowsException<NotImplementedException>(() => new Receiver(mockLogger.Object, null));
    }

    [TestMethod]
    [Timeout(4000)]
    public void ReceiverCanReceiveMessage()
    {
      //Send the message
      Assert.IsTrue(Sender.SendMessage("test"));

      //Wait to receive message
      var loggerrx = Receiver.GetFieldValue<MockLogger>("logger");
      bool found = false;
      while (!found)
      {
        //Wait for message to arrive or timeout
        var logMessage = loggerrx.GetPropertyValue<List<object>>("InfoMessages");
        if (logMessage.Contains("Hello test, I am your father!"))
          found = true;
      }
      Assert.IsTrue(found);
    }

  }
}
