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

namespace ResourceCompiler
{
    using System.Web.Mvc;
    using ResourceCompiler.Web.Mvc;

    public class ComponentBuilder
    {

        private StyleSheetManagerBuilder styleSheetManagerBuilder;
        private ScriptManagerBuilder scriptManagerBuilder;

        public ComponentBuilder(StyleSheetManagerBuilder styleSheetManagerBuilder, ScriptManagerBuilder scriptManagerBuilder )
        {
            this.scriptManagerBuilder = scriptManagerBuilder;
            this.styleSheetManagerBuilder = styleSheetManagerBuilder;

        }

        public StyleSheetManagerBuilder StyleSheetManager()
        {
            return styleSheetManagerBuilder;
        }

        public ScriptManagerBuilder ScriptManager()
        {
            return scriptManagerBuilder;
        }
    }
}
