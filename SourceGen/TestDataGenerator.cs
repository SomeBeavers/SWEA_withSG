using Microsoft.CodeAnalysis;

namespace SourceGen
{
    [Generator]
    public class TestDataGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
            // Commenting out this line make it resolve
            context.RegisterForSyntaxNotifications(() => new Pizza());
        }

        public void Execute(GeneratorExecutionContext context)
        {
          var noError = context.Compilation.GetTypeByMetadataName("NoError");
          var warn = context.Compilation.GetTypeByMetadataName("Warn");
          if (noError == null)
          {
            context.AddSource(
              "Pizza.cs",
              "namespace TestNS" +
              "{" +
              "public class TestCls" +
              "{" +
              "public static string TestMethod()" +
              "{" +
              "System.Console.WriteLine(\"Generator\");" +
              "}" +
              "#if NETCOREAPP3_1\r\npublic class CoreClass\r\n{\r\n    private int Test()\r\n    {\r\n    }\r\n}\r\n#endif"+
              "}" +
              "}"
            );
          }
          else if (warn != null)
          {
            context.AddSource(
              "Pizza.cs",
              "namespace TestNS{public class TestCls{private int unused;private string? unused2;public static void TestMethod(){System.Console.WriteLine(\"Generator\");}}}"
            );
          }
          else
          {
            context.AddSource(
              "Pizza.cs",
              "namespace TestNS{public class TestCls{public static void TestMethod(){System.Console.WriteLine(\"Generator\");}}}"
            );
          }
        }
    }

    public class Pizza : ISyntaxContextReceiver
    {
        public void OnVisitSyntaxNode(GeneratorSyntaxContext context)
        {
        }
    }
}