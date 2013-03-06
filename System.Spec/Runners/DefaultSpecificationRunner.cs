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

namespace System.Spec.Runners
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Spec.IO;
    using System.Spec.Formatter;

    [Serializable]
    public class DefaultSpecificationRunner : SpecificationRunnerBase
    {

        public DefaultSpecificationRunner(IExpressionRunner runner, 
                                          ISpecificationFinder finder, 
                                          IConsoleFormatter formatter) : base(runner, finder, formatter)
        {
        }

        protected override IEnumerable<ExpressionResult> ExecuteSpecifications(IEnumerable<Specification> specifications,
                                                                               string example)
        {
            var results = new Collection<ExpressionResult>();
            
            foreach (var specification in specifications) {
                var result = this.ExecuteSpecification(specification, example);
                
                results.Add(result);
            }
            
            return results;
        }
    }
}