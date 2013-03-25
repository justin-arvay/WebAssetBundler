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

    public interface IContainer
    {
        /// <summary>
        /// Creates or replaces a container class.
        /// </summary>
        /// <param name="registerType"></param>
        /// <returns></returns>
        void Register(Type registerType);

        /// <summary>
        /// Creates/replaces a named container class.
        /// </summary>
        /// <param name="registerType"></param>
        /// <param name="name"></param>
        void Register(Type registerType, string name);

        /// <summary>
        /// Creates/replaces a container class registration with a given implementation
        /// </summary>
        /// <param name="registerType"></param>
        /// <param name="registerImplementation"></param>
        void Register(Type registerType, Type registerImplementation);

        /// <summary>
        /// Creates/replaces a named container class registration with a given implementation.
        /// </summary>
        /// <param name="registerType"></param>
        /// <param name="registerImplementation"></param>
        /// <param name="name"></param>
        void Register(Type registerType, Type registerImplementation, string name);

        /// <summary>
        /// Creates/replaces a container class registration with a user specified factory
        /// </summary>
        /// <param name="registerType">Type to register</param>
        /// <param name="factory">Factory/lambda that returns an instance of RegisterType</param>
        /// <returns>RegisterOptions for fluent API</returns>
        void Register(Type registerType, Func<IContainer, object> factory);

        /// <summary>
        /// Creates/replaces a container class registration with a user specified factory.
        /// </summary>
        /// <param name="registerType"></param>
        /// <param name="factory"></param>
        /// <param name="name"></param>
        void Register(Type registerType, Func<IContainer, object> factory, string name);

        /// <summary>
        /// Creates/replaces a named container class registration with a specific, strong referenced, instance.
        /// </summary>
        /// <param name="registerType"></param>
        /// <param name="instance"></param>
        /// <param name="name"></param>
        void Register(Type registerType, object instance, string name);

        /// <summary>
        /// Creates/replaces a container class registration with a specific, strong referenced, instance.
        /// </summary>
        /// <param name="registerType"></param>
        /// <param name="registerImplementation"></param>
        /// <param name="instance"></param>
        void Register(Type registerType, Type registerImplementation, object instance);

        /// <summary>
        /// Creates/replaces a named container class registration with a specific, strong referenced, instance.
        /// </summary>
        /// <param name="registerType"></param>
        /// <param name="registerImplementation"></param>
        /// <param name="instance"></param>
        /// <param name="name"></param>
        void Register(Type registerType, Type registerImplementation, object instance, string name);

        /// <summary>
        /// Creates or replaces a container class.
        /// </summary>
        /// <typeparam name="Type"></typeparam>
        void Register<Type>();      
 
        /// <summary>
        /// Creates/replaces a named container class registration.
        /// </summary>
        /// <typeparam name="RegisterType"></typeparam>
        /// <param name="name"></param>
        void Register<RegisterType>(string name);

        /// <summary>
        /// Creates/replaces a container class registration with a given implementation.
        /// </summary>
        /// <typeparam name="RegisterType"></typeparam>
        /// <typeparam name="RegisterImplementation"></typeparam>
        void Register<RegisterType, RegisterImplementation>();

        /// <summary>
        /// Creates/replaces a named container class registration with a given implementation
        /// </summary>
        /// <typeparam name="RegisterType"></typeparam>
        /// <typeparam name="RegisterImplementation"></typeparam>
        /// <param name="name"></param>
        void Register<RegisterType, RegisterImplementation>(string name);

        /// <summary>
        /// Creates/replaces a container class registration with a specific, strong referenced, instance.
        /// </summary>
        /// <typeparam name="RegisterType"></typeparam>
        /// <param name="instance"></param>
        void Register<RegisterType>(RegisterType instance);

        /// <summary>
        /// Creates/replaces a named container class registration with a specific, strong referenced, instance.
        /// </summary>
        /// <typeparam name="RegisterType"></typeparam>
        /// <param name="instance"></param>
        /// <param name="name"></param>
        void Register<RegisterType>(RegisterType instance, string name);

        /// <summary>
        /// Creates/replaces a container class registration with a specific, strong referenced, instance.
        /// </summary>
        /// <typeparam name="RegisterType"></typeparam>
        /// <typeparam name="RegisterImplementation"></typeparam>
        /// <param name="instance"></param>
        void Register<RegisterType, RegisterImplementation>(RegisterImplementation instance);

        /// <summary>
        /// Creates/replaces a named container class registration with a specific, strong referenced, instance.
        /// </summary>
        /// <typeparam name="RegisterType"></typeparam>
        /// <typeparam name="RegisterImplementation"></typeparam>
        /// <param name="instance"></param>
        /// <param name="name"></param>
        void Register<RegisterType, RegisterImplementation>(RegisterImplementation instance, string name);

        /// <summary>
        /// Creates/replaces a container class registration with a user specified factory.
        /// </summary>
        /// <typeparam name="RegisterType"></typeparam>
        /// <param name="factory"></param>
        void Register<RegisterType>(Func<IContainer, RegisterType> factory);

        /// <summary>
        /// Creates/replaces a named container class registration with a user specified factory.
        /// </summary>
        /// <typeparam name="RegisterType"></typeparam>
        /// <param name="factory"></param>
        /// <param name="name"></param>
        void Register<RegisterType>(Func<TinyIoCContainer, NamedParameterOverloads, RegisterType> factory, string name);
    }
}
