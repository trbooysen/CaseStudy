using Ninject;
using Shared;
using Shared.Interfaces;
using System;

namespace ServiceA
{
  /// <summary>
  /// The entry point for ServiceA
  /// </summary>
  class Program
  {
    static void Main(string[] args)
    {
      var container = new StandardKernel();
      configureContainer(container);

      var logger = container.Get<ILogger>();
      var messageService = container.Get<ISendMessage>();
      //This should normally be done from the UI (login screen etc.)
      //could also ask for credentials from console
      if (messageService.SetupChannel("192.168.100.119", "rabbitUser", "rabbitPwd"))
      {
        var sender = new Sender(logger, messageService);

        Console.WriteLine(" Enter your name followed by [enter] (quit to exit).");
        string name = Console.ReadLine();
        while (name != "quit")
        {
          if (string.IsNullOrEmpty(name))
            name = EnterValidName();
          else
          {
            sender.SendMessage(name);
            Console.WriteLine(" Enter your name followed by [enter] (quit to exit).");
            name = Console.ReadLine();
          }
        }
      }
      else
      {
        Console.WriteLine(" Oops, something went wrong, check the logs for more detail.");
        Console.ReadLine();
      }
    }

    private static string EnterValidName()
    {
      Console.WriteLine(" Enter a valid name followed by [enter] (quit to exit).");
      return Console.ReadLine();
    }

    /// <summary>
    /// Get access to concrete classes using dependency injection
    /// </summary>
    /// <param name="container"></param>
    private static void configureContainer(IKernel container)
    {
      container.Bind<ISendMessage>().To<RabbitMQService>();
      container.Bind<ILogger>().To<Log4NetLogger<Program>>();
    }
  }
}
