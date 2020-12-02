namespace Shared.Interfaces
{
  public interface ISendMessage  
  {
    bool SendMessage(string message);
    bool SetupChannel(string hostName, string userName, string password);
  }
}
