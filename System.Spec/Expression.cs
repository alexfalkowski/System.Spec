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
    using Collections.Generic;
    using Linq;
    using Monad.Maybe;
    
    public class Expression
    {
        private readonly IDictionary<string, ExampleGroup> exampleGroups = new Dictionary<string, ExampleGroup>();
        
        public string Name { get; set; }

        public void Add(ExampleGroup example)
        {
            exampleGroups.Add(example.Reason, example);
        }

        public IEnumerable<ExampleGroup> Examples {
            get {
                return exampleGroups.Values;
            }
        }

        public IOption<ExampleGroup> FindExampleGroup(string groupText)
        {
            var option = groupText.SomeStringOrNone();

            return option.Into(value => {
                ExampleGroup group;
                exampleGroups.TryGetValue(value, out group);
                
                return group.SomeOrNone();
            });
        }

        public IOption<ExampleTuple> FindExample(string exampleText)
        {
            var query = from exampleGroup in exampleGroups.Values
                        from example in exampleGroup.Find(exampleText)
                        select new ExampleTuple { Example = example, ExampleGroup = exampleGroup };

            return query.FirstOrDefault().SomeOrNone();
        }
    }
}