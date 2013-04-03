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

using System;

namespace System.Spec.Specs
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Xml;
    using System.Xml.Serialization;
    
    using FluentAssertions;

    using System.Spec.Examples.Specs;
    using System.Spec.Reports;
    using System.Spec.Formatter;
    using System.Spec.IO;
    using System.Spec.Runners;
    
    using NSubstitute;
    
    using NUnit.Framework;
    
    [TestFixture]
    public class SummarySpecs
    {
        private ISpecificationRunner runner;
        private IActionStrategy strategy;
        private resultType resultType;
        
        [TestFixtureSetUp]
        public void BeforeAll()
        {
            this.strategy = new DefaultActionStrategy();
            var finder = new DefaultSpecificationFinder(new DefaultFileSystem());
            var runner = new DefaultExpressionRunner(this.strategy);
            var formatter = new SilentConsoleFormatter(new DefaultConsoleWritter());
            this.runner = new DefaultSpecificationRunner(runner, finder, formatter);

            var location = new Uri(typeof(TestSpecificationConfigurationManager).Assembly.CodeBase).LocalPath;
            var appDomain = new SpecificationAppDomain(this.runner);
            var results = appDomain.ExecuteSpecifications(location);
            
            using (var stream = new MemoryStream()) {
                var reporter = new NUnitSpecificationReporter();
                reporter.Write(stream, results);
                stream.Seek(0, SeekOrigin.Begin);

                var serializer = new XmlSerializer(typeof(resultType));
                using (XmlReader reader = XmlReader.Create(stream)) {
                    this.resultType = (resultType)serializer.Deserialize(reader);
                }
            }
        }

        [Test]
        public void ShouldHaveNunitVersion()
        {
            var expectedVersion = typeof(TestAttribute).Assembly.GetName().Version.ToString();
            this.resultType.environment.nunitversion.Should().Be(expectedVersion);
        }

        [Test]
        public void ShouldHaveClrVersion()
        {
            this.resultType.environment.clrversion.Should().Be(Environment.Version.ToString());
        }

        [Test]
        public void ShouldHaveOsVersion()
        {
            this.resultType.environment.osversion.Should().Be(Environment.OSVersion.VersionString);
        }

        [Test]
        public void ShouldHaveMachineName()
        {
            this.resultType.environment.machinename.Should().Be(Environment.MachineName);
        }

        [Test]
        public void ShouldHavePlatform()
        {
            var expected = Enum.GetName(typeof(PlatformID), Environment.OSVersion.Platform);
            this.resultType.environment.platform.Should().Be(expected);
        }

        [Test]
        public void ShouldHaveUser()
        {
            this.resultType.environment.user.Should().Be(Environment.UserName);
        }

        [Test]
        public void ShouldHaveDomain()
        {
            this.resultType.environment.userdomain.Should().Be(Environment.UserDomainName);
        }

        [Test]
        public void ShouldHaveCurrentCulture()
        {
            this.resultType.cultureinfo.currentculture.Should().Be(CultureInfo.CurrentCulture.ToString());
            this.resultType.cultureinfo.currentuiculture.Should().Be(CultureInfo.CurrentCulture.ToString());
        }

        [Test]
        public void ShouldHaveExcecuted()
        {
            this.resultType.testsuite.executed.Should().Be(bool.TrueString);
        }

        [Test]
        public void ShouldHaveFailure()
        {
            this.resultType.testsuite.result.Should().Be("Failure");
        }

        [Test]
        public void ShouldHaveTime()
        {
            double.Parse(this.resultType.testsuite.time).Should().BeGreaterOrEqualTo(0.0);
        }

        [Test]
        public void ShouldHaveErrors()
        {
            var results = this.resultType.testsuite.results.AsEnumerable();
            var query = from testsuiteType expression in results
                        from testsuiteType @group in expression.results
                        from testcaseType example in @group.results
                        where example.result == "Failure"
                        select example;
            query.Should().HaveCount(2);
            query.Should().Contain(result => double.Parse(result.time) >= 0D);
        }

        [Test]
        public void ShouldHaveSuccess()
        {
            var results = this.resultType.testsuite.results.AsEnumerable();
            var query = from testsuiteType expression in results
                        from testsuiteType @group in expression.results
                        from testcaseType example in @group.results
                        where example.result == "Success"
                        select example;
            query.Should().HaveCount(9);
            query.Should().Contain(result => double.Parse(result.time) >= 0D);
        }
    }
}