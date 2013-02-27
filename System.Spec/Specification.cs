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
    public abstract class Specification
    {
        private Expression expression = new Expression();
        private ExampleGroup currentExampleGroup;
        
        public void BeforeAll(Action action)
        {
            HasCurrentExampleGroup();
            
            currentExampleGroup.BeforeAll = action;
        }
        
        public void AfterAll(Action action)
        {
            HasCurrentExampleGroup();
            
            currentExampleGroup.AfterAll = action;
        }
        
        public void BeforeEach(Action action)
        {
            HasCurrentExampleGroup();
            
            currentExampleGroup.BeforeEach = action;
        }
        
        public void AfterEach(Action action)
        {
            HasCurrentExampleGroup();
            
            currentExampleGroup.AfterEach = action;
        }
        
        public void Describe(string reason, Action action)
        {
            currentExampleGroup = new ExampleGroup { Reason = reason };
            expression.Examples.Add(currentExampleGroup);
            
            action();
            
            currentExampleGroup = null;
        }
        
        public void It(string reason, Action action)
        {
            HasCurrentExampleGroup();
            
            currentExampleGroup.Examples.Add(new Example { 
                Reason = reason, 
                Action = action 
            });
        }
        
        public Expression BuildExpression()
        {
            this.Define();
            
            return expression;
        }
        
        protected abstract void Define();
        
        private void HasCurrentExampleGroup()
        {
            if (currentExampleGroup == null) {
                throw new InvalidOperationException("There is no current describe");
            }
        }
    }
}