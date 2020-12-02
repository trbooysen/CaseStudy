using log4net;
using log4net.Config;
using Shared.Interfaces;
using System;

namespace Shared
{
  /// <summary>
  /// 
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public class Log4NetLogger<T> : ILogger
  {
    private static readonly ILog log = LogManager.GetLogger(typeof(T));

    /// <summary>
    /// Constructor - setup everything required to log to a file.
    /// </summary>
    public Log4NetLogger()
    { 
      //Initialise log4net
      XmlConfigurator.Configure();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    public void Debug(object message)
    {
      log.Debug(message);
    }

    public void Error(object message, Exception exception)
    {
      log.Error(message, exception);
    }

    public void Info(object message)
    {
      log.Info(message);
    }
  }

  public class DbLogger : ILogger
  {
    public DbLogger()
    {
    }

    public void Debug(object message)
    {
      throw new NotImplementedException();
    }

    public void Error(object message, Exception exception)
    {
      throw new NotImplementedException();
    }

    public void Info(object message)
    {
      throw new NotImplementedException();
    }

  }

}
