﻿// Web Asset Bundler - Bundles web assets so you dont have to.
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

namespace WebAssetBundler.Web.Mvc.Tests
{
    using NUnit.Framework;
    using Moq;

    [TestFixture]
    public class MergedAssetTests
    {

        [Test]
        public void Should_Set_Content()
        {
            var assets = new AssetCollection()
            {
                new AssetBaseImpl("function(){}"),
                new AssetBaseImpl("function(){}")

            };

            var asset = new MergedAsset(assets, ";");

            Assert.AreEqual("function(){};function(){};", asset.OpenStream().ReadToEnd());
        }

        [Test]
        public void Should_Return_New_Stream_And_Full_Content()
        {
            var assets = new AssetCollection()
            {
                new AssetBaseImpl("function(){}"),
                new AssetBaseImpl("function(){}")

            };

            var asset = new MergedAsset(assets, ";");

            //will throw exception if we do not return new stream

            var content = asset.OpenStream().ReadToEnd();
            var content2 = asset.OpenStream().ReadToEnd();

            Assert.AreEqual(content2, content);
        }
    }
}
