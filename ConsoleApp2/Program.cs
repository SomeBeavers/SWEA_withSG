namespace ConsoleApp2
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            TestNS.TestCls.TestMethod();
        }

        string? M() => null;
    }
}

class NoError { }
class Warn { }

