// ResourceCompiler - Compiles web assets so you dont have to.
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

    public class TinyIocContainerImpl : IContainer
    {
        private TinyIoCContainer container = new TinyIoCContainer();

        public void Register(Type registerType)
        {
            container.Register(registerType);
        }

        public void Register(Type registerType, Type registerImplementation)
        {
            container.Register(registerType, registerImplementation);
        }

        public void Register(Type registerType, Func<IContainer, object> factory)
        {
            container.Register(registerType, factory);
        }

        public void Register(Type registerType, Type registerImplementation, object instance)
        {
            throw new NotImplementedException();
        }

        public void Register<Type>()
        {
            throw new NotImplementedException();
        }

        public void Register<RegisterType, RegisterImplementation>()
        {
            throw new NotImplementedException();
        }

        public void Register<RegisterType>(RegisterType instance)
        {
            throw new NotImplementedException();
        }

        public void Register<RegisterType, RegisterImplementation>(RegisterImplementation instance)
        {
            throw new NotImplementedException();
        }

        public void Register<RegisterType>(Func<IContainer, RegisterType> factory)
        {
            throw new NotImplementedException();
        }

        public void Resolve(Type resolveType)
        {
            throw new NotImplementedException();
        }

        public ResolveType Resolve<ResolveType>() where ResolveType : class
        {
            throw new NotImplementedException();
        }
    }
}
