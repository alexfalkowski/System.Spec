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

namespace System.Spec.Runners
{
    using System.Spec.Formatter;

    public class DefaultExpressionRunner : IExpressionRunner
    {
        private IActionStrategy stratergy;

        public DefaultExpressionRunner(IActionStrategy stratergy)
        {
            this.stratergy = stratergy;
        }

        public ExpressionResult Execute(Expression expression, string example)
        {
            var expressionResult = new ExpressionResult { Name = expression.Name };
            var group = expression.FindGroup(example);

            if (group != null) {
                expressionResult.Examples.Add(this.ExecuteExampleGroup(group));
            } else {
                foreach (var exampleGroup in expression.Examples) {
                    expressionResult.Examples.Add(this.ExecuteExampleGroup(exampleGroup));
                }
            }

            return expressionResult;
        }

        private ExampleGroupResult ExecuteExampleGroup(ExampleGroup exampleGroup)
        {
            var result = new ExampleGroupResult { Reason = exampleGroup.Reason };
            
            this.stratergy.ExecuteAction(exampleGroup.BeforeAll);
            
            foreach (var example in exampleGroup.Examples) {
                result.Examples.Add(this.ExecuteExample(exampleGroup, example));
            }
            
            this.stratergy.ExecuteAction(exampleGroup.AfterAll);

            return result;
        }

        private ExampleResult ExecuteExample(ExampleGroup exampleGroup, Example example)
        {
            this.stratergy.ExecuteAction(exampleGroup.BeforeEach);
            var result = this.stratergy.ExecuteActionWithResult(example.Action).ToExampleResult(example.Reason);
            
            this.stratergy.ExecuteAction(exampleGroup.AfterEach);

            return result;
        }
    }
}