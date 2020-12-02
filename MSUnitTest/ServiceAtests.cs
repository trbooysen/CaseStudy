using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceA;
using Shared.Interfaces;
using System;

namespace MSUnitTest
{
  [TestClass]
  public class ServiceAtests: ServicesBaseContextTestFixture
  {
    [TestMethod]
    public void SenderNullLoggerGuards()
    {
      Mock<ISendMessage> mockService = new Mock<ISendMessage>();
      Assert.ThrowsException<NotImplementedException>(() => new Sender(null, mockService.Object));
    }

    [TestMethod]
    public void SenderNullServiceGuards()
    {
      Mock<ILogger> mockLogger = new Mock<ILogger>();
      Assert.ThrowsException<NotImplementedException>(() => new Sender(mockLogger.Object, null));
    }

    [TestMethod]
    public void SenderCanSendMessage()
    {
      Assert.IsTrue(Sender.SendMessage("test"));
    }

  }
}
