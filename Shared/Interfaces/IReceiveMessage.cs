using System;

namespace Shared.Interfaces
{
  public interface IReceiveMessage
  {
    /// <summary>
    /// Not specific to any provider so data can be encapsulated in the <see cref="EventArgs" property./>
    /// </summary>
    event EventHandler<MessageEventArgs> MessageReceived;

    bool SetupMessageListener();

    bool SetupChannel(string hostName, string userName, string password);

  }

}
