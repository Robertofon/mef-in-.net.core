using System;
using System.Composition;
using SzenarioTester.Interfaces;

namespace Extension1
{
    [Export(typeof(ITestScenario))]
    [ExportMetadata("ZweiA", "Home")]
    public class GrandeTestA : ITestScenario
    {
        public void Test()
        {
            Console.WriteLine($"Test in: {nameof(GrandeTestA)}" );
        }
    }
}
