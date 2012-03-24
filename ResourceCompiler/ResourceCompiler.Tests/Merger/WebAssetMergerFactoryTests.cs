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

namespace ResourceCompiler.Web.Mvc.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class WebAssetMergerFactoryTests
    {
        [Test]
        public void Should_Return_Combined_Merger()
        {
            var group = new WebAssetGroup("test", false)
            {
                Combined = true
            };

            var factory = new WebAssetMergerFactory();

            Assert.IsInstanceOf<CombinedWebAssetGroupMerger>(factory.Create(group));
        }

        [Test]
        public void Should_Return_Versioned_Merger()
        {
            var group = new WebAssetGroup("test", false)
            {
                Version = "1.1"
            };

            var factory = new WebAssetMergerFactory();

            Assert.IsInstanceOf<VersionedWebAssetGroupMerger>(factory.Create(group));
        }

        [Test]
        public void Should_Return_Empty_Merger()
        {
            var group = new WebAssetGroup("test", false);
            var factory = new WebAssetMergerFactory();

            Assert.IsInstanceOf<EmptyWebAssetGroupMerger>(factory.Create(group));
        }
    }
}
