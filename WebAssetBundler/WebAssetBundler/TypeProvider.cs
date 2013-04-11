// Web Asset Bundler - Bundles web assets so you dont have to.
// Copyright (C) 2012  Justin Arvay
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

namespace WebAssetBundler.Web.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Linq;

    public class TypeProvider : WebAssetBundler.ITypeProvider
    {
        private IEnumerable<Assembly> assemblies;
        private Type[] allTypes;

        public TypeProvider(IEnumerable<Assembly> assemblies)
        {
            this.assemblies = FilterAssemblies(assemblies);            
        }

        public Type[] GetAllTypes()
        {
            if (allTypes == null)
            {
                allTypes = (
                    from assembly in assemblies
                    where AssemblyIsNotIgnored(assembly) && IsNotDynamic(assembly)
                    from type in GetPublicTypesAndIgnoreLoaderExceptions(assembly)
                    where !type.IsAbstract
                    select type
                ).ToArray();
            }

            return allTypes;
        }

        public IEnumerable<Type> GetImplementationTypes(Type baseType)
        {
            return GetAllTypes().Where(baseType.IsAssignableFrom);
        }

        private IEnumerable<Assembly> FilterAssemblies(IEnumerable<Assembly> assemblies)
        {
            return assemblies
                .Where(assembly => AssemblyIsNotIgnored(assembly) && IsNotDynamic(assembly))
                .ToArray();
        }

        private IEnumerable<Type> GetPublicTypesAndIgnoreLoaderExceptions(Assembly assembly)
        {
            IEnumerable<Type> types;
            try
            {
                types = assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException ex)
            {
                types = ex.Types.Where(type => type != null);
            }
            return types.Where(t => t.IsPublic || t.IsNestedPublic);
        }

        private static bool AssemblyIsNotIgnored(Assembly assembly)
        {
            return !IgnoredAssemblies.Any(ignore => ignore(assembly));
        }

        private static readonly List<Func<Assembly, bool>> IgnoredAssemblies = new List<Func<Assembly, bool>>
        {
            assembly => assembly.FullName.StartsWith("Microsoft.", StringComparison.InvariantCulture),
            assembly => assembly.FullName.StartsWith("mscorlib,", StringComparison.InvariantCulture),
            assembly => assembly.FullName.StartsWith("System.", StringComparison.InvariantCulture),
            assembly => assembly.FullName.StartsWith("System,", StringComparison.InvariantCulture),
        };

        private static bool IsNotDynamic(Assembly assembly)
        {
#if NET35
            return !(assembly.ManifestModule is ModuleBuilder);
#else
            return !assembly.IsDynamic;
#endif
        }
    }
}
