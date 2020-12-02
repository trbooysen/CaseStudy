using Ninject;
using Shared;
using Shared.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Timers;
using XUnitTestServiceA.MockServices;

namespace XUnitTestServiceA.Builders
{
  public class MockMessageQueueServiceBuilder : ISendMessage, IReceiveMessage
  {
    public IKernel Container = new StandardKernel();
    private ConcurrentDictionary<int, string> cache;
    private Timer timer;

    public MockMessageQueueServiceBuilder()
    {
      cache = new ConcurrentDictionary<int, string>();
      configureContainer();
      timer = new Timer();
      timer.Interval = 100;
      timer.Elapsed += Timer_Elapsed;
    }

    private void Timer_Elapsed(object sender, ElapsedEventArgs e)
    {
      if (cache != null && cache.Count > 0)
      {
        if (cache.TryRemove(1, out var message))
          MessageReceived?.Invoke(this, new MessageEventArgs() { Message = message });
      }
    }

    public event EventHandler<MessageEventArgs> MessageReceived;

    public MockMessageQueueService Build()
    {
      //Create the context using IoC
      var context = Container.Get<MockMessageQueueService>();
      return context;
    }

    public bool SendMessage(string message)
    {
      int count = cache.Count;
      count++;
      cache.AddOrUpdate(count, message, (i, s) => message);
      timer.Start();
      return true;
    }

    public bool SetupMessageListener()
    {
      return true;
    }

    private void configureContainer()
    {
      Container.Bind<ISendMessage>().To<MockMessageQueueService>();
      Container.Bind<ILogger>().To<Log4NetLogger<MockMessageQueueServiceBuilder>>();
    }
  }
}
