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
    using System.Collections.ObjectModel;
    using System.Collections.Generic;
    
    public class Expression
    {
        private IDictionary<string, ExampleGroup> exampleGroups = new Dictionary<string, ExampleGroup>();
        
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

        public ExampleGroup FindExampleGroup(string groupText)
        {
            var isValidExampleText = !string.IsNullOrWhiteSpace(groupText);

            if (isValidExampleText) {
                ExampleGroup group;
                var foundExampleGroup = this.exampleGroups.TryGetValue(groupText, out group);

                if (foundExampleGroup) {
                    return group;
                }
            }

            return null;
        }

        public Tuple<Example, ExampleGroup> FindExample(string exampleText)
        {
            foreach (var exampleGroup in this.exampleGroups.Values) {
                var example = exampleGroup.Find(exampleText);
                
                if (example != null) {
                    return Tuple.Create(example, exampleGroup);
                }
            }

            return null;
        }
    }
}