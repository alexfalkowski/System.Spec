﻿//  Author:
//       alexfalkowski <alexrfalkowski@gmail.com>
//
//  Copyright (c) 2013 alexfalkowski
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

namespace System.Spec.Command
{
	using System.Spec.Formatter;

	using PowerArgs;

	public class Arguments
	{
        [ArgPosition(0)]
        [ArgDescription("The path to search all the Spec assemblies.")]
        [DefaultValue("spec")]
        public string Path { get; set; }

		[ArgShortcut("e")]
		[ArgDescription("The example to execute. This could be a type, group or single example.")]
		public string Example { get; set; }

        [ArgShortcut("P")]
        [ArgDescription("Load files matching pattern. The default is Specs.")]
        [DefaultValue(StringHelper.SpecsSearch)]
        public string Pattern { get; set; }

        [ArgShortcut("o")]
        [ArgDescription("The output path of the test results. The default is test-results.xml.")]
        [DefaultValue("test-results.xml")]
        public string Output { get; set; }

		[ArgShortcut("f")]
		[ArgDescription("Specifies what format to use for output.")]
		public ConsoleFormatterType Format { get; set; }

		[ArgShortcut("d")]
		[ArgDescription("Invokes formatters without executing the examples.")]
		public bool DryRun { get; set; }

        [ArgShortcut("p")]
        [ArgDescription("Execute example(s) in parrallel.")]
        [ArgIgnore]
        public bool Parrallel { get; set; }

        [ArgShortcut("v")]
        [ArgDescription("Display the version.")]
        public bool Version { get; set; }

		[ArgShortcut("h")]
		[ArgDescription("You're looking at it.")]
		public bool Help { get; set; }
	}
}