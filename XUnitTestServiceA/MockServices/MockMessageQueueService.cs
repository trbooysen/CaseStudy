using ServiceA;
using Shared;
using Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace XUnitTestServiceA.MockServices
{
  public class MockMessageQueueService : ISendMessage
  {
    public string ReceiveMessage()
    {
      throw new NotImplementedException();
    }

    public bool SendMessage(string message)
    {
      Console.WriteLine($"send message ({message}) using an sms");
      //Send using a gateway;
      return true;
    }
  }

}
