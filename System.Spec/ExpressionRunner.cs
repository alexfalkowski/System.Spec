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
    public class ExpressionRunner
    {
        private IActionStrategy stratergy;

        public ExpressionRunner(IActionStrategy stratergy)
        {
            this.stratergy = stratergy;
        }

        public void Execute(Expression expression)
        {
            foreach (var exampleGroup in expression.Examples) {
                this.stratergy.ExecuteActionWithResult(exampleGroup.BeforeAll);
                
                foreach (var example in exampleGroup.Examples) {
                    this.stratergy.ExecuteActionWithResult(exampleGroup.BeforeEach);
                    this.stratergy.ExecuteActionWithResult(example.Action);
                    this.stratergy.ExecuteActionWithResult(exampleGroup.AfterEach);
                }
                
                this.stratergy.ExecuteActionWithResult(exampleGroup.AfterAll);
            }
        }
    }
}