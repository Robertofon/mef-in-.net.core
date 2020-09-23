using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Composition;
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
    public sealed class TestRunner
    {
        [ImportMany]
        public Lazy<ITestScenario,IDictionary<string, object>>[] TestPlugins { get; set; }

        public void Initialize(params string[] assemblypaths)
        {
            try
            {
                var catalog = new AggregateCatalog();
                foreach (string path in assemblypaths)
                {

                    var assemblies = Directory.GetFiles(path, "*.dll", SearchOption.AllDirectories)
                        //.Select(AssemblyLoadContext.Default.LoadFromAssemblyPath);
                        .Select(o => new AssemblyCatalog(o));
                    foreach (var ac in assemblies)
                    {
                        catalog.Catalogs.Add(ac);
                    }
                }
                

                //var catalog = new AggregateCatalog();
                //foreach (string path in assemblypaths)
                //{
                //    catalog.Catalogs.Add(new DirectoryCatalog(path));
                //    catalog.Catalogs.Add(new DirectoryCatalog(Path.Combine(path, "netstandard2.0")));
                //}
                // Opt:
                //catalog.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly()));
                //catalog.Catalogs.Add(new AssemblyCatalog(@"C:\Users\rob\source\repos\SzenarioTester\ExtensionAssemblies\Extension1.dll"));

                var container = new CompositionContainer(catalog);
                
                var resolvedType = container.GetExportedValue<ITestScenario>();
                container.ComposeParts(this);
            }
            catch (Exception ex)
            {
                throw new UserException("Did not succedd ", ex);
            }

        }

        /// <summary>
        /// Run the test.
        /// </summary>
        public void Run()
        {
            
        }
    }
}

/*
    var configuration = new ContainerConfiguration().WithAssembly( typeof( MyType ).Assembly ) );
   var container = configuration.CreateContainer();


    und

 var directory = AppDomain.CurrentDomain.BaseDirectory;
    var assemblies = Directory.GetFiles( directory, "*.dll" )
                    .Select(  AssemblyLoadContext.Default.LoadFromAssemblyPath );

    var configuration = new ContainerConfiguration().WithAssemblies( assemblies );
    var container = configuration.CreateContainer();
*/
