using System;

namespace Shared.Interfaces
{
  /// <summary>
  /// A logging interface (contract) to delegate all the logging responsibility.
  /// It will give the capability to log Info, Error and Debug messages.
  /// </summary>
  public interface ILogger
  {
    void Info(object message);
    void Debug(object message);
    void Error(object message, Exception exception);
  }

}
