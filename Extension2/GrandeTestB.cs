using System;
using System.Composition;
using SzenarioTester.Interfaces;

namespace Extension1
{
    [Export(typeof(ITestScenario))]
    [ExportMetadata("ItalB", "Home")]
    public class GrandeTestB : ITestScenario
    {
        public void Test()
        {
            Console.WriteLine($"Test in: {nameof(GrandeTestB)}" );
        }
    }
}
