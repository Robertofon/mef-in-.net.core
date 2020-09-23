using System;
using System.Collections.Generic;
using System.Composition;
using System.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using SzenarioTester.Interfaces;

namespace SzenarioTester
{
    /// <summary>
    /// Harness to run test scenarios implementing <see cref="ITestScenario"/>.
    /// </summary>
    public sealed class TestRunner: IDisposable
    {
        private CompositionHost _container;
        private IEnumerable<ITestScenario> _resolvedTypes;

        ////[ImportMany]
        ////public Lazy<ITestScenario,IDictionary<string, object>>[] TestPlugins { get; set; }
        // https://blog.softwarepotential.com/porting-to-net-standard-2-0-part-2-porting-mef-1-0-to-mef-2-0-on-net-core/

        public void Initialize(params string[] assemblypaths)
        {
            try
            {
                var configuration = new ContainerConfiguration();
                //.WithAssembly(typeof(TestRunner).Assembly) );
                foreach (string path in assemblypaths)
                {

                    var assemblies = Directory.GetFiles(path, "*.dll", SearchOption.AllDirectories)
                        .Select(AssemblyLoadContext.Default.LoadFromAssemblyPath);
                        //.Select(o => new AssemblyCatalog(o));
                        configuration.WithAssemblies(assemblies);
                }
                
                CompositionHost container = configuration.CreateContainer();
                
                this._resolvedTypes = container.GetExports<ITestScenario>();
                this._container = container;
            }
            catch (Exception ex)
            {
                throw new UserException("Did not succeed ", ex);
            }

        }

        /// <summary>
        /// Run the test.
        /// </summary>
        public void Run()
        {
            Console.WriteLine("======= Start ============");
            foreach (ITestScenario scenario in _resolvedTypes)
            {
                scenario.Test();
                Console.WriteLine("==========");
            }

            Console.WriteLine("======= End ============");
        }

        public void Dispose()
        {
            _container?.Dispose();
        }
    }
}
