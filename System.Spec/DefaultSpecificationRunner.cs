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
    using System.Spec.Formatter;

    public class DefaultSpecificationRunner : ISpecificationRunner
    {
        private readonly ISpecificationFinder finder;

        private readonly IExpressionRunner runner;

        private IConsoleFormatter formatter;

        public DefaultSpecificationRunner(
            IExpressionRunner runner,
            ISpecificationFinder finder,
            IConsoleFormatter formatter)
        {
            this.finder = finder;
            this.runner = runner;
            this.formatter = formatter;
        }
       
        public ExpressionResultCollection ExecuteSpecificationsInPath(string path, string search)
        {
            var specifications = this.finder.FindSpecifications(path, search);
            var results = new ExpressionResultCollection();

            foreach (var specification in specifications) {
                var result = this.runner.Execute(specification.BuildExpression());

                foreach (var exampleGroup in result.Examples) {
                    this.formatter.WriteInformation(exampleGroup.Reason);

                    foreach (var example in exampleGroup.Examples) {
                        if (example.Status == ResultStatus.Success) {
                            this.formatter.WriteSuccess(example.Reason, example);
                        } else {
                            this.formatter.WriteError(example.Reason, example);
                        }
                    }
                }

                results.Add(result);
            }

            return results;
        }
    }
}