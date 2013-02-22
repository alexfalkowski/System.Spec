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
    using System.Globalization;
    using System.Linq;

    using System.Spec.Properties;

    public class ProgressConsoleFormatter : ConsoleFormatterBase
    {
        public override void WriteSuccess(string reason, ExampleResult example)
        {
            Console.Write(Resources.ConsoleFormatterSuccessMessage);

            base.WriteSuccess(reason, example);
        }

        public override void WriteError(string reason, ExampleResult example)
        {
            Console.Write(Resources.ConsoleFormatterErrorMessage);

            base.WriteError(reason, example);
        }

        public override void WriteSummary(long elapsedMilliseconds)
        {
            var exampleResults = this.ExampleResults;

            if (exampleResults.HasErrors || exampleResults.HasSuccess) {
                Console.WriteLine(Environment.NewLine);
            }

            var errorResults = exampleResults.AllErrors.ToList();

            if (errorResults.Count > 0) {
                for (var index = 0; index < errorResults.Count; index++) {
                    var example = errorResults [index];
                    var errorMessage = string.Format(
                        CultureInfo.CurrentCulture,
                        Resources.ConsoleFormatteErrorsMessage,
                        index + 1,
                        example.Reason,
                        example.Exception);

                    Console.WriteLine(errorMessage);
                    Console.WriteLine();
                }
            }

            base.WriteSummary(elapsedMilliseconds);
        }
    }
}