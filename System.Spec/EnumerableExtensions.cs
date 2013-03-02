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
    using System.Linq;

    public static class EnumerableExtensions
    {
        public static Specification FindByExampleGroupName(this IEnumerable<Specification> specs, string name)
        {
            var query = from spec in specs
                        let expression = spec.BuildExpression()
                        let @group = expression.FindExampleGroup(name)
                        where @group != null
                        select spec;

            return query.FirstOrDefault();
        }

        public static Specification FindByExampleName(this IEnumerable<Specification> specs, string name)
        {
            var query = from spec in specs
                        let expression = spec.BuildExpression()
                        let example = expression.FindExample(name)
                        where example != null
                        select spec;
            
            return query.FirstOrDefault();
        }

        public static bool HasErrors(this IEnumerable<ExampleGroupResult> examples)
        {
            return GetExamples(examples, (example) => example.Status == ResultStatus.Error).Any();
        }

        public static bool HasErrors(this IEnumerable<ExampleResult> examples)
        {
            return examples.AllErrors().Any();
        }

        public static bool HasErrors(this IEnumerable<ExpressionResult> examples)
        {
            return GetExamples(examples, (example) => example.Status == ResultStatus.Error).Any();
        }
        
        public static bool HasSuccesses(this IEnumerable<ExpressionResult> examples)
        {
            return GetExamples(examples, (example) => example.Status == ResultStatus.Success).Any();
        }

        public static long ElapsedTime(this IEnumerable<ExampleGroupResult> examples)
        {
            var query = from exampleGroup in examples
                select exampleGroup.Examples.ElapsedTime();
            
            return query.Sum();
        }

        public static long ElapsedTime(this IEnumerable<ExpressionResult> expressions)
        {
            var query = from expression in expressions
                        select expression.Examples.ElapsedTime();

            return query.Sum();
        }

        public static long ElapsedTime(this IEnumerable<ExampleResult> examples)
        {
            var query = from example in examples
                        select example.ElapsedTime;
            
            return query.Sum();
        }

        public static IEnumerable<ExampleResult> AllErrors(this IEnumerable<ExpressionResult> expressions)
        {
            return GetExamples(expressions, (example) => example.Status == ResultStatus.Error);
        }

        public static IEnumerable<ExampleResult> AllSuccesses(this IEnumerable<ExpressionResult> expressions)
        {
            return GetExamples(expressions, (example) => example.Status == ResultStatus.Success);
        }

        public static IEnumerable<ExampleResult> AllErrors(this IEnumerable<ExampleResult> examples)
        {
            return GetExamples(examples, (example) => example.Status == ResultStatus.Error);
        }
        
        public static IEnumerable<ExampleResult> AllSuccesses(this IEnumerable<ExampleResult> examples)
        {
            return GetExamples(examples, (example) => example.Status == ResultStatus.Success);
        }

        private static IEnumerable<ExampleResult> GetExamples(IEnumerable<ExampleGroupResult> examples, 
                                                              Predicate<ExampleResult> status)
        {
            return from exampleGroup in examples
                   from example in exampleGroup.Examples
                   where status(example)
                   select example;
        }

        private static IEnumerable<ExampleResult> GetExamples(IEnumerable<ExampleResult> examples, 
                                                              Predicate<ExampleResult> status)
        {
            return from example in examples
                   where status(example)
                   select example;
        }

        private static IEnumerable<ExampleResult> GetExamples(IEnumerable<ExpressionResult> expressions,
                                                              Predicate<ExampleResult> status)
        {
            return from result in expressions
                   from exampleGroup in result.Examples
                   from example in exampleGroup.Examples
                   where status(example)
                   select example;
        }
    }
}