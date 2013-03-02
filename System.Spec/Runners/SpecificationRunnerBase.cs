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
    using System.Spec.Formatter;
    using System.Spec.IO;

    public abstract class SpecificationRunnerBase : ISpecificationRunner
    {
        private readonly IExpressionRunner runner;

        private readonly ISpecificationFinder finder;
        
        private IConsoleFormatter formatter;
        
        public SpecificationRunnerBase(
            IExpressionRunner runner,
            ISpecificationFinder finder,
            IConsoleFormatter formatter)
        {
            this.runner = runner;
            this.finder = finder;
            this.formatter = formatter;
        }

        public IEnumerable<ExpressionResult> ExecuteSpecificationsInPath(string path, 
                                                                         string pattern, 
                                                                         string example)
        {
            return this.ExecuteSpecifications(this.finder.FindSpecifications(path, pattern, example));
        }

        protected abstract IEnumerable<ExpressionResult> ExecuteSpecifications(IEnumerable<Specification> specifications);

        protected ExpressionResult ExecuteSpecification(Specification specification)
        {
            var result = this.runner.Execute(specification.BuildExpression());
            
            foreach (var exampleGroup in result.Examples) {
                this.formatter.WriteInformation(exampleGroup.Reason);
                
                foreach (var example in exampleGroup.Examples) {
                    if (example.Status == ResultStatus.Success) {
                        this.formatter.WriteSuccess(example);
                    } else {
                        this.formatter.WriteError(example);
                    }
                }
            }
            
            return result;
        }
    }
}

