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
        private ICommand command;
        private ISpecificationVisitor specificationVisitor;
        private IConsoleFormatter consoleFormatter;
        private IFileSystem fileSystem;
        private IActionStrategy strategy;
        private resultType resultType;
        
        [SetUp]
        public void BeforeEach()
        {
            this.fileSystem = new DefaultFileSystem();
            this.strategy = new DefaultActionStrategy();
            this.consoleFormatter = new SilentConsoleFormatter();
            this.specificationVisitor = new DefaultSpecificationVisitor(this.consoleFormatter);
            this.command = new DefaultCommand(
                this.specificationVisitor, this.strategy, this.strategy, this.consoleFormatter, this.fileSystem);

            var location = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
            var result = this.command.ExecuteSpecificationsInPath(location, StringHelper.SpecsSearch);
            result.Should().Be(2);
            
            using (var stream = new MemoryStream()) {
                this.consoleFormatter.WriteSummaryToStream(stream);
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
    }
}

