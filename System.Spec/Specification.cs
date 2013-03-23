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
    using Monad.Maybe;
    using Runtime.CompilerServices;

    public abstract class Specification
    {
        private readonly Expression expression;
        private IOption<ExampleGroup> currentExampleGroupOption;
        private bool hasExpressionBeenBuilt;

        protected Specification()
        {
            expression = new Expression { Name = GetType().FullName };
        }

        public void BeforeAll(Action action)
        {
            currentExampleGroupOption.Into(currentExampleGroup => currentExampleGroup.BeforeAll = action);
        }

        public void XBeforeAll(Action action)
        {
        }
        
        public void AfterAll(Action action)
        {
            currentExampleGroupOption.Into(currentExampleGroup => currentExampleGroup.AfterAll = action);
        }

        public void XAfterAll(Action action)
        {
        }
        
        public void BeforeEach(Action action)
        {
            currentExampleGroupOption.Into(currentExampleGroup => currentExampleGroup.BeforeEach = action);
        }

        public void XBeforeEach(Action action)
        {
        }
        
        public void AfterEach(Action action)
        {
            currentExampleGroupOption.Into(currentExampleGroup => currentExampleGroup.AfterEach = action);
        }

        public void XAfterEach(Action action)
        {
        }
        
        public void Describe(string reason, Action action)
        {
            var group = new ExampleGroup { Reason = reason };
            expression.Add(group);
            currentExampleGroupOption = group.SomeOrNone();

            action();
            
            currentExampleGroupOption = Option.None<ExampleGroup>();
        }

        public void XDescribe(string reason, Action action)
        {
        }

        public void It(string reason, Action action, 
                       [CallerFilePath] string callingFilePath = "", 
                       [CallerLineNumber] int callingFileLineNumber = 0)
        {
            currentExampleGroupOption.Into(@group => @group.Add(new Example
                {
                    Reason = reason,
                    Action = action,
                    FileName = callingFilePath,
                    LineNumber = callingFileLineNumber
                }));
        }

        public void XIt(string reason, Action action)
        {
        }
        
        public Expression BuildExpression()
        {
            if (!hasExpressionBeenBuilt) {

                Define();
                hasExpressionBeenBuilt = true;
            }
            
            return expression;
        }
        
        protected abstract void Define();
    }
}