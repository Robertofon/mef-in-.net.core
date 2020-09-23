﻿using System;
using System.Composition;
using SzenarioTester.Interfaces;

namespace Extension1
{
    [Export(typeof(ITestScenario))]
    [ExportMetadata("Name", "Home")]
    public class TestClass1 : ITestScenario
    {
        public void Test()
        {
            Console.WriteLine($"Test in: {nameof(TestClass1)}" );
        }
    }
}
