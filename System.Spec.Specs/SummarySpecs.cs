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
    using Globalization;
    using IO;
    using Linq;
    using NUnit.Framework;
    using Reports;
    using Runners;
    using System;
    using System.IO;
    using Xml;
    using Xml.Serialization;
    
    [TestFixture]
    public class SummarySpecs
    {
        private ISpecificationRunner runner;
        private resultType resultType;
        
        [TestFixtureSetUp]
        public void BeforeAll()
        {
            var finder = new DefaultSpecificationFinder(new DefaultFileSystem());
            var expressionRunner = new DefaultExpressionRunnerFactory().CreateExpressionRunner(false);
            var formatter = new SilentConsoleFormatter(new DefaultConsoleWritter());
            runner = new DefaultSpecificationRunner(expressionRunner, finder, formatter);

            var location = new Uri(typeof(TestSpecificationConfigurationManager).Assembly.CodeBase).LocalPath;
            var appDomain = new SpecificationAppDomain(runner);
            var results = appDomain.ExecuteSpecifications(location);
            
            using (var stream = new MemoryStream()) {
                var reporter = new NUnitSpecificationReporter();
                reporter.Write(stream, results);
                stream.Seek(0, SeekOrigin.Begin);

                var serializer = new XmlSerializer(typeof(resultType));
                using (var reader = XmlReader.Create(stream)) {
                    resultType = (resultType)serializer.Deserialize(reader);
                }
            }
        }

        [Test]
        public void ShouldHaveNunitVersion()
        {
            var expectedVersion = typeof(TestAttribute).Assembly.GetName().Version.ToString();
            resultType.environment.nunitversion.Should().Be(expectedVersion);
        }

        [Test]
        public void ShouldHaveClrVersion()
        {
            resultType.environment.clrversion.Should().Be(Environment.Version.ToString());
        }

        [Test]
        public void ShouldHaveOsVersion()
        {
            resultType.environment.osversion.Should().Be(Environment.OSVersion.VersionString);
        }

        [Test]
        public void ShouldHaveMachineName()
        {
            resultType.environment.machinename.Should().Be(Environment.MachineName);
        }

        [Test]
        public void ShouldHavePlatform()
        {
            var expected = Enum.GetName(typeof(PlatformID), Environment.OSVersion.Platform);
            resultType.environment.platform.Should().Be(expected);
        }

        [Test]
        public void ShouldHaveUser()
        {
            resultType.environment.user.Should().Be(Environment.UserName);
        }

        [Test]
        public void ShouldHaveDomain()
        {
            resultType.environment.userdomain.Should().Be(Environment.UserDomainName);
        }

        [Test]
        public void ShouldHaveCurrentCulture()
        {
            resultType.cultureinfo.currentculture.Should().Be(CultureInfo.CurrentCulture.ToString());
            resultType.cultureinfo.currentuiculture.Should().Be(CultureInfo.CurrentUICulture.ToString());
        }

        [Test]
        public void ShouldHaveExcecuted()
        {
            resultType.testsuite.executed.Should().Be(bool.TrueString);
        }

        [Test]
        public void ShouldHaveFailure()
        {
            resultType.testsuite.result.Should().Be("Failure");
        }

        [Test]
        public void ShouldHaveTime()
        {
            double.Parse(resultType.testsuite.time).Should().BeGreaterOrEqualTo(0.0);
        }

        [Test]
        public void ShouldHaveErrors()
        {
            var results = resultType.testsuite.results.AsEnumerable();
            var query = from testsuiteType expression in results
                        from testsuiteType @group in expression.results
                        from testcaseType example in @group.results
                        where example.result == "Failure"
                        select example;
            var testcaseTypes = query as testcaseType[] ?? query.ToArray();
            testcaseTypes.Should().HaveCount(2);
            testcaseTypes.Should().Contain(result => double.Parse(result.time) >= 0D);
        }

        [Test]
        public void ShouldHaveSuccess()
        {
            var results = resultType.testsuite.results.AsEnumerable();
            var query = from testsuiteType expression in results
                        from testsuiteType @group in expression.results
                        from testcaseType example in @group.results
                        where example.result == "Success"
                        select example;
            var testcaseTypes = query as testcaseType[] ?? query.ToArray();
            testcaseTypes.Should().HaveCount(9);
            testcaseTypes.Should().Contain(result => double.Parse(result.time) >= 0D);
        }
    }
}