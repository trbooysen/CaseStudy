using Ninject;
using ServiceA;
using System;
using Xunit;

namespace XUnitTestServiceA
{
  public class UnitTest1
  {
    public IKernel Container = new StandardKernel();


    [Fact]
    public void LogExceptionToFile()
    {
      //Container.Get<FileLogger<UnitTest1>>().Error("", new Exception("Test exception"));
      //new ExceptionLogger<UnitTest1>().LogIntoFile("", new Exception("Test exception"));

    }

  }
}
