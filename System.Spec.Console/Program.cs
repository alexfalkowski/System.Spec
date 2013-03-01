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

namespace System.Spec.Console
{
	using System;
	
	using System.Spec.Formatter;
    using System.Spec.Reports;
	using System.Spec.Command;
	
	using PowerArgs;
	
	public static class Program
	{
		public static int Main(string[] args)
		{
			var command = new SpecCommand(args,
			                              new DefaultConsoleFormatterFactory(),
                                          new NUnitSpecificationReporter(),
			                              new DefaultFileSystem());
			return command.Perform();
		}
	}
}
