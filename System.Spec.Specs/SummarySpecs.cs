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

    using System.Spec.Formatter;
    
    using NSubstitute;
    
    using NUnit.Framework;
    
    [TestFixture]
    public class SummarySpecs
    {
        private ISpecificationRunner runner;
        private IConsoleFormatter consoleFormatter;
        private IActionStrategy strategy;
        private resultType resultType;
        
        [SetUp]
        public void BeforeEach()
        {
            this.strategy = new DefaultActionStrategy();
            var finder = new DefaultSpecificationFinder(new DefaultFileSystem());
            var runner = new DefaultExpressionRunner(this.strategy);
            this.consoleFormatter = new SilentConsoleFormatter();
            this.runner = new DefaultSpecificationRunner(runner, finder, this.consoleFormatter);

            var location = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
            this.runner.ExecuteSpecificationsInPath(location, StringHelper.SpecsSearch);
            
            using (var stream = new MemoryStream()) {
                this.consoleFormatter.WriteSummaryToStream(stream, 10);
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
            double.Parse(this.resultType.testsuite.time).Should().BeGreaterThan(0.0);
        }

        [Test]
        public void ShouldHaveResults()
        {
            var results = this.resultType.testsuite.results;
            results.Should().NotBeNull();
            results.Should().HaveCount(10);
        }

        [Test]
        public void ShouldHaveErrors()
        {
            var results = this.resultType.testsuite.results.AsEnumerable();
            var query = from testsuiteType type in results
                        where type.result == "Failure"
                        select type;
            query.Should().HaveCount(2);
            query.Should().Contain(result => int.Parse(result.time) >= 0);
        }

        [Test]
        public void ShouldHaveSuccess()
        {
            var results = this.resultType.testsuite.results.AsEnumerable();
            var query = from testsuiteType type in results
                where type.result == "Success"
                    select type;
            query.Should().HaveCount(8);
            query.Should().Contain(result => int.Parse(result.time) >= 0);
        }
    }
}