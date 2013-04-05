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
    using Collections.Generic;
    using Formatter;
    using IO;
    using Linq;
    using Monad.Maybe;

    [Serializable]
    public abstract class SpecificationRunnerBase : ISpecificationRunner
    {
        private readonly IExpressionRunner runner;

        private readonly ISpecificationFinder finder;
        
        private readonly IConsoleFormatter formatter;

        protected SpecificationRunnerBase(
            IExpressionRunner runner,
            ISpecificationFinder finder,
            IConsoleFormatter formatter)
        {
            this.runner = runner;
            this.finder = finder;
            this.formatter = formatter;
        }

        public IEnumerable<ExpressionResult> ExecuteSpecificationsInPath(string path, string example = null)
        {
            var result = finder.GetSpecifications(path, example);

            if (!result.FoundType) {
                foreach (var exampleText in example.SomeStringOrNone()) {
                    foreach (var specification in FindSpecification(result.Specifications, exampleText)) {
                        return ExecuteSpecifications(new [] { specification }, exampleText);
                    }
                    
                    return Enumerable.Empty<ExpressionResult>();
                }
            }

            return ExecuteSpecifications(result.Specifications, example);
        }
       
        protected abstract IEnumerable<ExpressionResult> ExecuteSpecifications(IEnumerable<Specification> specifications, 
                                                                               string example);

        protected ExpressionResult ExecuteSpecification(Specification specification, string exampleText)
        {
            var result = runner.Execute(specification.BuildExpression(), exampleText);
            
            foreach (var exampleGroup in result.Examples) {
                formatter.WriteInformation(exampleGroup.Reason);
                
                foreach (var example in exampleGroup.Examples) {
                    if (example.Status == ResultStatus.Success) {
                        formatter.WriteSuccess(example);
                    } else {
                        formatter.WriteError(example);
                    }
                }
            }
            
            return result;
        }

        private static IEnumerable<Specification> FindSpecification(IEnumerable<Specification> specifications, string example)
        {
            var enumerable = specifications as Specification[] ?? specifications.ToArray();
            foreach (var specification in enumerable.FindByExampleGroupName(example)) {
                return specification.SomeOrNone();
            }

            return enumerable.FindByExampleName(example).Into(specification => specification);
        }
    }
}