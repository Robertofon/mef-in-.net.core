using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using SzenarioTester.Interfaces;

namespace SzenarioTester
{
    class Program
    {
        private const string ExtensionsDir = "ExtensionAssemblies";

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
#if DEBUG
            string location = Assembly.GetEntryAssembly().Location;
            List<string> pathparts = location.Split(Path.DirectorySeparatorChar).SkipLast(5).ToList();
            string asmpath = string.Join(Path.DirectorySeparatorChar, pathparts);
#else
            string asmpath = Assembly.GetEntryAssembly().Location;
#endif
            string extdir = Path.Combine(asmpath, ExtensionsDir);

            var testrunner = new TestRunner();
            testrunner.Initialize(extdir);
            testrunner.Run();
        }
    }
}
