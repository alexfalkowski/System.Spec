//  Author:
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
	using Formatter;

	using PowerArgs;

    [ArgIgnoreCase(false)]
    [ArgExample("spec.(exe|sh) spec", "Execute all the specs in the folder spec.")]
    [TabCompletion]
	public class Arguments
	{
        [ArgPosition(0)]
        [ArgShortcut("p")]
        [ArgDescription("The path to search all the Spec assemblies. The default is spec.")]
        [DefaultValue("spec")]
        public string Path { get; set; }

		[ArgShortcut("e")]
		[ArgDescription("The example to execute. This could be a spec, example group or an example.")]
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

        [ArgShortcut("c")]
        [ArgDescription("Enable colour in the output.")]
        public bool Colour { get; set; }

		[ArgShortcut("h")]
		[ArgDescription("You're looking at it.")]
		public bool Help { get; set; }
	}
}