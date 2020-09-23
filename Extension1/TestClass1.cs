using System;
using System.Composition;
using SzenarioTester.Interfaces;

namespace Extension1
{
    [Export(typeof(ITestScenario))]
    [ExportMetadata("Name", "Home")]
    //[PartCreationPolicy(CreationPolicy.NonShared)]
    public class TestClass1 : ITestScenario
    {
        public void Test()
        {
            Console.WriteLine($"Test in: {nameof(TestClass1)}" );
        }
    }
}
