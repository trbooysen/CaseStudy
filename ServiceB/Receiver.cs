using Ninject;
using Shared;
using Shared.Interfaces;
using System;

namespace ServiceB
{
  /// <summary>
  /// Service that handles the receiving of messages using a message service 
  /// - RabbitMQ in this case.
  /// </summary>
  public class Receiver
  {
    private readonly ILogger logger;
    private readonly IReceiveMessage messageService;

    public Receiver(ILogger logger, IReceiveMessage messageService)
    {
      //Guard clauses
      if (logger == null)
        throw new NotImplementedException("A logger is required");
      if (messageService == null)
        throw new NotImplementedException("A receive message service is required");

      this.logger = logger;
      this.messageService = messageService;
      logger.Info("Instantiating reading message service");
      subscribeToMessages();
    }

    /// <summary>
    /// Subscribe to the event that fires when new messages arive.
    /// </summary>
    private void subscribeToMessages()
    {
      logger.Info("Subscribe to message receive event.");
      messageService.MessageReceived += MessageService_MessageReceived; 
      if (!messageService.SetupMessageListener())
        logger.Info("Could not set up the listener (see the error log for details).");
    }

    /// <summary>
    /// The event handler that is called when any messages are recieved.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e">Contains the message <see cref="MessageEventArgs.Message"/></param>
    private void MessageService_MessageReceived(object sender, MessageEventArgs e)
    {
      if (validMessage(e.Message))
      {
        string name = e.Message.Substring("Hello my name is,".Length).Trim();
        logger.Info($"Hello {name}, I am your father!");
      }
      else
      {
        logger.Info($"The message was invalid (see the error log for details).");
      }
    }

    /// <summary>
    /// Validate the actual message string received.
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    private bool validMessage(string message)
    {
      if (message.Contains("Hello my name is,"))
        return true;

      logger.Error($"Message invalid ({message})", null);
      return false;
    }
  }
}
