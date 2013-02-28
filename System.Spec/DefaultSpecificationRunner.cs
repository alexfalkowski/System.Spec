// Author:
//       alex.falkowski <alexrfalkowski@gmail.com>
//
//  Copyright (c) 2013 alex.falkowski
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

namespace System.Spec
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;

    using System.Spec.Formatter;

    public class DefaultSpecificationRunner : ISpecificationRunner
    {
        private readonly ISpecificationFinder finder;

        private readonly ExpressionRunner runner;

        public DefaultSpecificationRunner(
            IActionStrategy exampleStrategy,
            ISpecificationFinder finder,
            IConsoleFormatter formatter)
        {
            this.finder = finder;
            this.runner = new ExpressionRunner(exampleStrategy, formatter);
        }
       
        public void ExecuteSpecificationsInPath(string path, string search)
        {
            var specifications = this.finder.FindSpecifications(path, search);

            foreach (var specification in specifications) {
                this.runner.Execute(specification.BuildExpression());
            }
        }
    }
}