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

namespace System.Spec.Specs
{
    using Examples.Specs;
    using FluentAssertions;
    using Formatter;
    using IO;
    using Linq;
    using NUnit.Framework;
    using Reflection;
    using Runners;
    using System;

    [TestFixture]
    public class RunnerSpecs
    {
        private ISpecificationRunner command;

        private string path;

        [SetUp]
        public void BeforeEach()
        {
            command = new DefaultSpecificationRunner(new DefaultExpressionRunnerFactory().CreateExpressionRunner(false), 
                                                          new DefaultSpecificationFinder(new DefaultFileSystem()),
                                                          new SilentConsoleFormatter(new DefaultConsoleWritter()));
            path = new Uri(Assembly.GetAssembly(typeof(TestSpecificationWithBeforeAll)).CodeBase).LocalPath;
        }

        [Test]
        public void ShouldExecuteAllSpecificationsInPath()
        {
            var results = command.ExecuteSpecificationsInPath(path);
            results.Should().HaveCount(11);
        }

        [Test]
        public void ShouldExecuteOneSpecificationInPath()
        {
            var results = command.ExecuteSpecificationsInPath(path, 
                                                                   typeof(TestSpecificationWithBeforeAll).FullName);
            results.Should().HaveCount(1);
        }

        [Test]
        public void ShouldExecuteOneDescribeInPath()
        {
            var results = command.ExecuteSpecificationsInPath(path, 
                                                                   "describe TestSpecificationWithMultipleIts");
            var expressionResults = results as ExpressionResult[] ?? results.ToArray();
            expressionResults.Should().HaveCount(1);
            var result = expressionResults.First();
            result.Examples.Should().HaveCount(1);
            result.Examples.First().Reason.Should().Be("describe TestSpecificationWithMultipleIts");
        }

        [Test]
        public void ShouldExecuteOneItInPath()
        {
            var results = command.ExecuteSpecificationsInPath(path, 
                                                                   "it should do one thing");
            var expressionResults = results as ExpressionResult[] ?? results.ToArray();
            expressionResults.Should().HaveCount(1);
            var result = expressionResults.First();
            result.Examples.Should().HaveCount(1);
            var group = result.Examples.First();
            group.Reason.Should().Be("describe TestSpecificationWithMultipleIts");
            group.Examples.Should().HaveCount(1);
            group.Examples.First().Reason.Should().Be("it should do one thing");
        }
    }
}