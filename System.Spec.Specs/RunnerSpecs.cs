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
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Xml;
    using System.Xml.Serialization;

    using FluentAssertions;

    using System.Spec;
    using System.Spec.Examples.Specs;
    using System.Spec.Formatter;
    using System.Spec.IO;
    using System.Spec.Runners;

    using NSubstitute;

    using NUnit.Framework;

    [TestFixture]
    public class RunnerSpecs
    {
        private ISpecificationRunner command;

        private string path;

        [SetUp]
        public void BeforeEach()
        {
            this.command = new DefaultSpecificationRunner(new DefaultExpressionRunner(new DefaultActionStratergy()), 
                                                          new DefaultSpecificationFinder(new DefaultFileSystem()),
                                                          new SilentConsoleFormatter(new DefaultConsoleWritter()));
            this.path = new Uri(Assembly.GetAssembly(typeof(TestSpecificationWithBeforeAll)).CodeBase).LocalPath;
        }

        [Test]
        public void ShouldExecuteAllSpecificationsInPath()
        {
            var results = this.command.ExecuteSpecificationsInPath(this.path);
            results.Should().HaveCount(11);
        }

        [Test]
        public void ShouldExecuteOneSpecificationInPath()
        {
            var results = this.command.ExecuteSpecificationsInPath(this.path, 
                                                                   typeof(TestSpecificationWithBeforeAll).FullName);
            results.Should().HaveCount(1);
        }

        [Test]
        public void ShouldExecuteOneDescribeInPath()
        {
            var results = this.command.ExecuteSpecificationsInPath(this.path, 
                                                                   "describe TestSpecificationWithMultipleIts");
            results.Should().HaveCount(1);
            var result = results.First();
            result.Examples.Should().HaveCount(1);
            result.Examples.First().Reason.Should().Be("describe TestSpecificationWithMultipleIts");
        }

        [Test]
        public void ShouldExecuteOneItInPath()
        {
            var results = this.command.ExecuteSpecificationsInPath(this.path, 
                                                                   "it should do one thing");
            results.Should().HaveCount(1);
            var result = results.First();
            result.Examples.Should().HaveCount(1);
            var group = result.Examples.First();
            group.Reason.Should().Be("describe TestSpecificationWithMultipleIts");
            group.Examples.Should().HaveCount(1);
            group.Examples.First().Reason.Should().Be("it should do one thing");
        }
    }
}