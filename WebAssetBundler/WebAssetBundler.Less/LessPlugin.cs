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

namespace WebAssetBundler.Web.Mvc.Less
{
    using System;
    using TinyIoC;

    public class LessPlugin : IPlugin<StyleSheetBundle>
    {

        public void Initialize(TinyIoCContainer container)
        {
            container.Register<ILessCompiler, LessCompiler>();
            container.Register<LessProcessor>();            
        }

        public void ModifyPipeline(IBundlePipeline<StyleSheetBundle> pipeline)
        {
            pipeline.Insert<LessProcessor>(0);
        }

        public void ModifySearchPatterns(System.Collections.Generic.ICollection<string> patterns)
        {
            patterns.Add("*.less");
        }

        public void Dispose()
        {
            
        }
    }
}
