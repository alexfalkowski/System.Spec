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
    using Collections.Generic;
    using Monad.Maybe;
    using System;

	public class ExampleGroup
	{
        private readonly IDictionary<string, Example> examples = new Dictionary<string, Example>();
        
        public string Reason { get; set; }
        
        public Action BeforeAll { get; set; }
        
        public Action AfterAll { get; set; }
        
        public Action BeforeEach { get; set; }
        
        public Action AfterEach { get; set; }

        public IEnumerable<Example> Examples {
            get {
                return examples.Values;
            }
        }

        public void Add(Example example)
        {
            examples.SafeAdd(example.Reason, example, Resources.ExampleErrorMessage);
        }

        public IOption<Example> Find(string exampleText)
        {
            var option = exampleText.SomeStringOrNone();

            return option.Into(value => {
                Example example;
                examples.TryGetValue(value, out example);
                
                return example.SomeOrNone();
            });
        }
	}
}