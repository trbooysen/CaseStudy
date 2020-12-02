﻿using Ninject;
using Shared;
using Shared.Interfaces;
using System;

namespace ServiceB
{
  class Program
  {
    static void Main(string[] args)
    {
      var container = new StandardKernel();
      configureContainer(container);

      var logger = container.Get<ILogger>();
      var messageService = container.Get<IReceiveMessage>();
      //This should normally be done from the UI (login screen etc.)
      //could also ask for credentials from console
      if (messageService.SetupChannel("192.168.100.119", "rabbitUser", "rabbitPwd"))
      {
        var receiver = new Receiver(logger, messageService);
  
        Console.WriteLine(" Press [enter] to exit.");
        Console.ReadLine();
      }
      else
      {
        Console.WriteLine(" Oops, something went wrong, check the logs for more detail.");
        Console.ReadLine();
      }
    }

    /// <summary>
    /// Get access to concrete classes using dependency injection
    /// </summary>
    /// <param name="container"></param>
    private static void configureContainer(IKernel container)
    {
      container.Bind<IReceiveMessage>().To<RabbitMQService>();
      container.Bind<ILogger>().To<Log4NetLogger<Program>>();
    }

  }
}
