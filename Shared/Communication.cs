using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Shared.Interfaces;
using System;
using System.Text;

namespace Shared
{

  public class RabbitMQService : ISendMessage, IReceiveMessage
  {
    private readonly ILogger logger;
    private IModel channel;

    private readonly string hostName;
    private readonly string userName;
    private readonly string password;

    /// <summary>
    /// Setup the RabbitMQ service.
    /// </summary>
    /// <param name="logger">Injected from client</param>
    public RabbitMQService(ILogger logger)
    {
      //Guard clauses
      if (logger == null)
        throw new NotImplementedException("A logger is required");

      this.logger = logger;
      //setupChannel();
    }

    public bool SetupChannel(string hostName, string userName, string password)
    {
      if (channel != null)
        return true;

      try
      {
        var factory = new ConnectionFactory()
        {
          HostName = hostName,
          UserName = userName,
          Password = password,
          VirtualHost = "/",
          Port = AmqpTcpEndpoint.UseDefaultPort
        };
        var connection = factory.CreateConnection();
        {
          channel = connection.CreateModel();
          channel.QueueDeclare(queue: "hello",
                               durable: false,
                               exclusive: false,
                               autoDelete: false,
                               arguments: null);
        }

      return true;
      }
      catch(Exception ex)
      {
        logger.Error("SetupChannel error", ex);
        return false;
      }
    }

    /// <summary>
    /// Event that will be raised on any new messages received.
    /// </summary>
    public event EventHandler<MessageEventArgs> MessageReceived;

    public bool SendMessage(string message)
    {
      Console.WriteLine($"Send message '{message}' using RabbitMQ");
      //Send using a queue
      if (handleMessageSend(message))
        return true;

      return false;
    }

    public bool SetupMessageListener()
    {
      if (channel == null)
      {
        logger.Error("The channel is null and needs to be set up again.", null);
        return false;
      }

      var consumer = new EventingBasicConsumer(channel);
      consumer.Received += (model, ea) =>
      {
        //Not required to do null check if done inside this lambda expression
        //if (ea == null)
        //  logger.Error("MessageReceived: Event is null", null);

        var body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);

        //If any clients are registered for this event - send the message.
        MessageReceived?.Invoke(model, new MessageEventArgs() { Message = message });
      };
      try
      {
        channel.BasicConsume(queue: "hello",
                             autoAck: true,
                             consumer: consumer);
      }
      catch (RabbitMQ.Client.Exceptions.AlreadyClosedException ex)
      {

        return false;
      }
      return true;
    }

    /// <summary>
    /// RabbitMQ implementation
    /// </summary>
    /// <param name="message"></param>
    private bool handleMessageSend(string message)
    {
      if (channel == null)
      {
        logger.Error("The channel is null and needs to be set up again.", null);
        return false;
      }

      var body = Encoding.UTF8.GetBytes(message);
      channel.BasicPublish(exchange: "",
                           routingKey: "hello",
                           basicProperties: null,
                           body: body);
      return true;
    }

    /// <summary>
    /// Create the channel and subscribe to message receiver.
    /// </summary>
    private void setupChannel()
    {
      var factory = new ConnectionFactory()
      {
        HostName = "192.168.100.119",
        UserName = "rabbitUser",
        Password = "rabbitPwd",
        VirtualHost = "/",
        Port = AmqpTcpEndpoint.UseDefaultPort
      };
      var connection = factory.CreateConnection();
      {
        channel = connection.CreateModel();
        channel.QueueDeclare(queue: "hello",
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);
      }
    }
  }
}
