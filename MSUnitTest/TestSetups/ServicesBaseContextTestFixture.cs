using MSUnitTest.Builders;
using ServiceA;
using ServiceB;

namespace MSUnitTest
{
  /// <summary>
  /// All artifacts required for the test fixture can be created in this base class 
  /// </summary>
  public class ServicesBaseContextTestFixture
  {
    /// <summary>
    /// The send message service that will be tested
    /// </summary>
    public Sender Sender { get; private set; }

    /// <summary>
    /// The receive message service that will be tested
    /// </summary>
    public Receiver Receiver { get; private set; }

    /// <summary>
    /// Create the message services
    /// </summary>
    public ServicesBaseContextTestFixture()
    {
      var builder = new ServicesBuilder();
      Sender = builder.BuildSender();
      Receiver = builder.BuildReceiver();
    }
  }

}
