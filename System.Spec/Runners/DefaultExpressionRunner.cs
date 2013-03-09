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
    using System.Linq;
    using System.Monad.Maybe;
    using System.Spec.Formatter;

    [Serializable]
    public class DefaultExpressionRunner : IExpressionRunner
    {
        private IOption<IActionStratergy> stratergyOption;

        public DefaultExpressionRunner(IOption<IActionStratergy> stratergyOption)
        {
            this.stratergyOption = stratergyOption;
        }

        public ExpressionResult Execute(Expression expression, string exampleText)
        {
            var expressionResult = new ExpressionResult { Name = expression.Name };

            foreach (var exampleGroup in expression.FindExampleGroup(exampleText)) {
                expressionResult.Examples.Add(this.ExecuteExampleGroup(exampleGroup));

                return expressionResult;
            }

            foreach (var example in expression.FindExample(exampleText)) {
                var exampleResult = new ExampleGroupResult { Reason = example.ExampleGroup.Reason };

                exampleResult.Examples.Add(this.ExecuteExample(example.ExampleGroup, example.Example));
                expressionResult.Examples.Add(exampleResult);

                return expressionResult;
            }

            foreach (var group in expression.Examples) {
                expressionResult.Examples.Add(this.ExecuteExampleGroup(group));
            }

            return expressionResult;
        }

        private ExampleGroupResult ExecuteExampleGroup(ExampleGroup exampleGroup)
        {
            var result = new ExampleGroupResult { Reason = exampleGroup.Reason };
            this.stratergyOption.Into(stratergy => stratergy.ExecuteAction(exampleGroup.BeforeAll));

            foreach (var example in exampleGroup.Examples) {
                result.Examples.Add(this.ExecuteExample(exampleGroup, example));
            }

            this.stratergyOption.Into(stratergy => stratergy.ExecuteAction(exampleGroup.AfterAll));

            return result;
        }

        private ExampleResult ExecuteExample(ExampleGroup exampleGroup, Example example)
        {
            this.stratergyOption.Into(stratergy => stratergy.ExecuteAction(exampleGroup.BeforeEach));
            var result = this.stratergyOption.Into(stratergy => stratergy.ExecuteActionWithResult(example.Action).ToExampleResult(example)).Or(example.ToExampleResult()).First();
            this.stratergyOption.Into(stratergy => stratergy.ExecuteAction(exampleGroup.AfterEach));

            return result;
        }
    }
}