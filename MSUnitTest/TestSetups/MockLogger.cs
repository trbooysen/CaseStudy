using Shared.Interfaces;
using System;
using System.Collections.Generic;

namespace MSUnitTest
{
  /// <summary>
  /// Create an in memory container (lists) for the messages created in tests.
  /// Use the <see cref="TestExtensionMethods"/> to access these containers in the tests.
  /// <para>Use: 
  /// <see cref="TestExtensionMethods.GetPropertyValue{T}(object, string)"/> and 
  /// <see cref="TestExtensionMethods.GetFieldValue{T}(object, string)"/> 
  /// </para>
  /// </summary>
  public class MockLogger : ILogger
  {
    public List<object> DebugMessages { get; private set; }
    public List<object> InfoMessages { get; private set; }
    public List<object> ErrorMessages { get; private set; }

    public MockLogger()
    {
      DebugMessages = new List<object>();
      InfoMessages = new List<object>();
      ErrorMessages = new List<object>();
    }

    public void Debug(object message)
    {
      DebugMessages.Add(message);
    }

    public void Error(object message, Exception exception)
    {
      ErrorMessages.Add(message);
    }

    public void Info(object message)
    {
      InfoMessages.Add(message);
    }

  }
}
