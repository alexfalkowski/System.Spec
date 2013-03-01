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

namespace System.Spec.Formatter
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    using System.Spec.Properties;

    public class NestedConsoleFormatter : ConsoleFormatterBase
    {
        public override void WriteInformation(string message)
        {
            WriteWithColour(ConsoleColor.Green, () => Console.WriteLine(message));
        }

        public override void WriteSuccess(ExampleResult example)
        {
            WriteWithColour(ConsoleColor.Green, () => Console.WriteLine(StringHelper.Tab + example.Reason));
        }

        public override void WriteError(ExampleResult example)
        {
            WriteWithColour(ConsoleColor.Red, () =>
                Console.WriteLine(string.Format(CultureInfo.CurrentCulture,
                        StringHelper.Tab + Resources.NestedConsoleFormatterErrorMessage,
                        example.Reason,
                        example.Exception)));
        }

        public override void WriteSummary(IEnumerable<ExpressionResult> expressions)
        {
            if (expressions.HasErrors() || expressions.HasSuccesses()) {
                Console.WriteLine();
            }

            base.WriteSummary(expressions);
        }

        /// <remarks>Idea from http://blogs.msdn.com/b/abhinaba/archive/2006/01/05/509581.aspx</remarks>
        private static void WriteWithColour(ConsoleColor colour, Action action)
        {
            var originalColour = Console.ForegroundColor;

            try {
                Console.ForegroundColor = colour;
                action();
            } finally {
                Console.ForegroundColor = originalColour;
            }
        }
    }
}