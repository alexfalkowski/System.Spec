// Author:
//       alex.falkowski <alexrfalkowski@gmail.com>
//
//  Copyright (c) 2013 alex.falkowski
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

namespace System.Spec
{
    using System.Spec.Formatter;

    public class DefaultExpressionRunner : IExpressionRunner
    {
        private IActionStrategy stratergy;

        public DefaultExpressionRunner(IActionStrategy stratergy)
        {
            this.stratergy = stratergy;
        }

        public ExpressionResult Execute(Expression expression)
        {
            var expressionResult = new ExpressionResult { Name = expression.Name };

            foreach (var exampleGroup in expression.Examples) {
                var exampleGroupResult = new ExampleGroupResult { Reason = exampleGroup.Reason };

                this.stratergy.ExecuteActionWithResult(exampleGroup.BeforeAll);
                
                foreach (var example in exampleGroup.Examples) {
                    this.stratergy.ExecuteActionWithResult(exampleGroup.BeforeEach);
                    var result = this.stratergy.ExecuteActionWithResult(example.Action).ToExampleResult(example.Reason);
                  
                    this.stratergy.ExecuteActionWithResult(exampleGroup.AfterEach);
                    exampleGroupResult.Examples.Add(result);
                }
                
                this.stratergy.ExecuteActionWithResult(exampleGroup.AfterAll);
                expressionResult.Examples.Add(exampleGroupResult);
            }

            return expressionResult;
        }
    }
}