// WebAssetBundler - Bundles web assets so you dont have to.
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
    using System.Reflection;
    using System.Collections.Generic;
    using System.Linq;

    public class DefaultConfigurationFactory<T, TBundle> : IConfigurationFactory
    {

        public IList<T> Create(BuilderContext context)
        {
            var configs = new List<T>();

            //TODO:: improve this to ignore obvious assemblies
            //TODO::improve this by caching so we dont run it every request
            var types = AppDomain.CurrentDomain.GetAssemblies().ToList()
                .SelectMany(s => s.GetTypes())
                .Where(p => typeof(T).IsAssignableFrom(p) && p.IsAbstract == false);
                //.Where(myType => myType.IsClass && myType.IsAbstract && myType.IsSubclassOf(typeof(T)));

            foreach (Type type in types)
            {
                configs.Add((T)Activator.CreateInstance(type));
            }

            return configs;
        }
    }
}
