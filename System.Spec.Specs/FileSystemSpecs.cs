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
    using System.Spec.IO;

	using FluentAssertions;

	using NUnit.Framework;

	[TestFixture]
	public class FileSystemSpecs
	{
		private IFileSystem fileSystem;

		[SetUp]
		public void BeforeEach()
		{
			this.fileSystem = new DefaultFileSystem();
		}

		[Test]
		public void ShouldFindDlls()
		{
			var location = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
			var files = this.fileSystem.GetFilesWithExtension(location, ".dll");
			files.Should().NotBeEmpty();
		}

		[Test]
		public void ShouldGetCurrentPath()
		{
			var currentPath = this.fileSystem.CurrentPath;
			currentPath.Should().Contain("System.Spec");
		}
	}
}