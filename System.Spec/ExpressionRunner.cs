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

    public class ExpressionRunner
    {
        private IActionStrategy stratergy;
        private IConsoleFormatter formatter;

        public ExpressionRunner(IActionStrategy stratergy, IConsoleFormatter formatter)
        {
            this.stratergy = stratergy;
            this.formatter = formatter;
        }

        public void Execute(Expression expression)
        {
            foreach (var exampleGroup in expression.Examples) {
                this.formatter.WriteInformation(exampleGroup.Reason);
                this.stratergy.ExecuteActionWithResult(exampleGroup.BeforeAll);
                
                foreach (var example in exampleGroup.Examples) {
                    this.stratergy.ExecuteActionWithResult(exampleGroup.BeforeEach);
                    var result = this.stratergy.ExecuteActionWithResult(example.Action);
                    var exampleResult = new ExampleResult {
                        Reason = example.Reason,
                        Status = result.Status,
                        Exception = result.Exception,
                        ElapsedTime = result.ElapsedTime
                    };

                    if (result.Status == ResultStatus.Success) {
                        this.formatter.WriteSuccess(example.Reason, exampleResult);
                    } else {
                        this.formatter.WriteError(example.Reason, exampleResult);
                    }
                  
                    this.stratergy.ExecuteActionWithResult(exampleGroup.AfterEach);
                }
                
                this.stratergy.ExecuteActionWithResult(exampleGroup.AfterAll);
            }
        }
    }
}