using Ninject;
using Shared;
using Shared.Interfaces;
using System;

namespace ServiceA
{
  /// <summary>
  /// Service that handles the sending of messages using a message service 
  /// - RabbitMQ in this case.
  /// </summary>
  public class Sender
  {
    private readonly ILogger logger;
    private readonly ISendMessage messageService;

    public Sender(ILogger logger, ISendMessage messageService)
    {
      //Guard clauses
      if (logger == null)
        throw new NotImplementedException("A logger is required");
      if (messageService == null)
        throw new NotImplementedException("A send message service is required");

      this.logger = logger;
      this.messageService = messageService;
      logger.Info("Instantiating writing message service");
    }

    /// <summary>
    /// Send the message.
    /// </summary>
    /// <param name="name">The name to use in the message.</param>
    /// <returns>Flag indicating if the message was sent.</returns>
    public bool SendMessage(string name)
    {
      var message = $"Hello my name is, {name}";
      logger.Info($"Sending message to service '{message}'");
      if (!messageService.SendMessage(message))
      {
        logger.Info($"The message '{message}' was not send (see the error log for details).");
        return false;
      }

      return true;
    }

  }
}
