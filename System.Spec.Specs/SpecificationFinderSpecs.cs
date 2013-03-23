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
    using IO;
    using NSubstitute;
    using NUnit.Framework;
    using Reflection;
    using System;
    
    [TestFixture]
    public class SpecificationFinderSpecs
    {
        private IFileSystem fileSystem;
        private ISpecificationFinder finder;
        
        [SetUp]
        public void BeforeEach()
        {
            fileSystem = Substitute.For<IFileSystem>();
            finder = new DefaultSpecificationFinder(fileSystem);
        }

        [Test]
        public void ShouldFindSpecifications()
        {
            var location = new Uri(Assembly.GetAssembly(typeof(TestSpecificationWithBeforeAll)).CodeBase).LocalPath;
            var result = finder.GetSpecifications(location);
            result.Specifications.Should().HaveCount(11);
        }

        [Test]
        public void ShouldGetSpecifications()
        {
            var location = new Uri(Assembly.GetAssembly(typeof(TestSpecificationWithBeforeAll)).CodeBase).LocalPath;
            const string testPath = "test";
            fileSystem.CurrentPath.Returns(testPath);
            fileSystem.GetFilesWithExtension(testPath, "Example.Spec.dll").Returns(new[] { location });
            
            var result = finder.GetSpecificationFiles(testPath, "Example.Spec");
            result.Should().HaveCount(1);
        }
    }
}