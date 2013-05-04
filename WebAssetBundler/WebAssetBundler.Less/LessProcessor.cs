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
    using System.Linq;

    public class LessProcessor : IPipelineProcessor<StyleSheetBundle>
    {
        private ICompiler compiler;

        public LessProcessor(ILessCompiler compiler)
        {
            this.compiler = compiler;
        }

        public void Process(StyleSheetBundle bundle)
        {          
            bundle.Assets.ForEach((asset) => {               
                if (asset.Source.EndsWith(".less", StringComparison.OrdinalIgnoreCase))
                {
                    asset.Modifiers.Add(new CompilerModifier(compiler));
                }
            });
        }
    }
}
