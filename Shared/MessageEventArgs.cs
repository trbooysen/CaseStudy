using System;

namespace Shared
{
  /// <summary>
  /// The class to hold the type of message data that can be recieved.
  /// This class can be extended for more complex data like XML
  /// </summary>
  public class MessageEventArgs : EventArgs
  {
    public string Message { get; set; }
  }
}
