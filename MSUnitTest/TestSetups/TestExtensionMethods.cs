using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace MSUnitTest
{
  /// <summary>
  /// Extensions that enable tests to access property and field values of objects.
  /// This is helpfull to get access to items not publically visible.
  /// </summary>
  [ExcludeFromCodeCoverage]
  public static class TestExtensionMethods
  {
    public static T GetPropertyValue<T>(this object sut, string name)
    {
      var field = sut
          .GetType()
          .GetProperty(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

      return (T)field?.GetValue(sut);
    }

    public static T GetFieldValue<T>(this object sut, string name)
    {
      var field = sut
          .GetType()
          .GetField(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

      return (T)field?.GetValue(sut);
    }
  }
}
