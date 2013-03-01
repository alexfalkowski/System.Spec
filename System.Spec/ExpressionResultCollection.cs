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
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    public class ExpressionResultCollection : Collection<ExpressionResult>
    {
        public long ElapsedTime { 
            get {
                var query = from result in this.Items
                            from exampleGroup in result.Examples
                            from example in exampleGroup.Examples
                            select example.ElapsedTime;

                return query.Sum();
            }
        }

        public bool HasErrors {
            get {
                return this.AllErrors.Any();
            }
        }

        public bool HasSuccess {
            get {
                return this.AllSuccess.Any();
            }
        }

        public IEnumerable<ExampleResult> AllErrors {
            get {
                return GetExamples((example) => example.Status == ResultStatus.Error);
            }
        }

        public IEnumerable<ExampleResult> AllSuccess {
            get {
                return GetExamples((example) => example.Status == ResultStatus.Success);
            }
        }

        private IEnumerable<ExampleResult> GetExamples(Predicate<ExampleResult> status)
        {
            return from result in this.Items
                   from exampleGroup in result.Examples
                   from example in exampleGroup.Examples
                   where status(example)
                   select example;
        }
    }
}