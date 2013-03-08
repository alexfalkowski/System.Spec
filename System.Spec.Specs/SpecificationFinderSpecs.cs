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
    using System.Reflection;
    using System.Xml;
    using System.Xml.Serialization;
    
    using FluentAssertions;
    
    using System.Spec.Examples.Specs;
    using System.Spec.Formatter;
    using System.Spec.IO;
    
    using NSubstitute;
    
    using NUnit.Framework;
    
    [TestFixture]
    public class SpecificationFinderSpecs
    {
        private IFileSystem fileSystem;
        private ISpecificationFinder finder;
        
        [SetUp]
        public void BeforeEach()
        {
            this.fileSystem = Substitute.For<IFileSystem>();
            this.finder = new DefaultSpecificationFinder(this.fileSystem);
        }

        [Test]
        public void ShouldFindSpecifications()
        {
            var location = new Uri(Assembly.GetAssembly(typeof(TestSpecificationWithBeforeAll)).CodeBase).LocalPath;
            var result = this.finder.GetSpecifications(location);
            result.Specifications.Should().HaveCount(11);
        }

        [Test]
        public void ShouldGetSpecifications()
        {
            var location = new Uri(Assembly.GetAssembly(typeof(TestSpecificationWithBeforeAll)).CodeBase).LocalPath;
            const string TestPath = "test";
            this.fileSystem.CurrentPath.Returns(TestPath);
            this.fileSystem.GetFilesWithExtension(TestPath, "Example.Spec.dll").Returns(new[] { location });
            
            var result = this.finder.GetSpecificationFiles(TestPath, "Example.Spec");
            result.Should().HaveCount(1);
        }
    }
}