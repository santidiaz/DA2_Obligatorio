using FixtureContracts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace FixtureProvider
{
    internal static class AlgorithmsHelper
    {
        public static IList<IFixture> GetAssemblyFixtures()
        {
            IList<IFixture> loadedAlgorithms;
            try
            {
                string rootPath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\"));
                string algorithmsPath = string.Concat(rootPath, @"\\Resources\\\Algorithms");

                // Load assemblies.
                List<Assembly> algorithmsAssemblies = GetAlgorithmsAssemblies(algorithmsPath);

                // Get only types that implements IFixture.
                IEnumerable<Type> allowedTypes = GetAllowedTypes(algorithmsAssemblies);

                // Instantiate types.
                loadedAlgorithms = InstantiateTypes(allowedTypes);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return loadedAlgorithms;
        }

        private static List<Assembly> GetAlgorithmsAssemblies(string path)
        {
            List<Assembly> algorithmsAssemblies = new List<Assembly>();

            foreach (string dll in Directory.GetFiles(path, "*.dll"))
                algorithmsAssemblies.Add(Assembly.LoadFile(dll));

            return algorithmsAssemblies;
        }

        private static IEnumerable<Type> GetAllowedTypes(List<Assembly> assemblies)
        {
            // Filter and get all assemblys types that implements IFixture
            return assemblies
                    .SelectMany(s => s.GetTypes())
                    .Where(p => (typeof(IFixture)).IsAssignableFrom(p));
        }

        private static IList<IFixture> InstantiateTypes(IEnumerable<Type> types)
        {
            IList<IFixture> instantiatedAlgorithms = new List<IFixture>();
            foreach (Type currentType in types)
            {
                object fixtureInstance = Activator.CreateInstance(currentType);
                instantiatedAlgorithms.Add((IFixture)fixtureInstance);
            }
            return instantiatedAlgorithms;
        }
    }
}
