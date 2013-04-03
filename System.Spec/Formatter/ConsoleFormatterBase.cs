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
    using Collections.Generic;
    using Globalization;
    using Linq;
    using Properties;
    using System;

    [Serializable]
    public abstract class ConsoleFormatterBase : IConsoleFormatter
    {
        private readonly IConsoleWritter writter;

        protected ConsoleFormatterBase(IConsoleWritter writter) 
        {
            this.writter = writter;
        }

        public abstract void WriteInformation(string message);

        public abstract void WriteSuccess(ExampleResult example);

        public abstract void WriteError(ExampleResult example);

        public virtual void WriteSummary(IEnumerable<ExpressionResult> expressions)
        {
            var expressionResults = expressions as ExpressionResult[] ?? expressions.ToArray();
            var errorResults = expressionResults.AllErrors().ToList();

            writter.WriteLine();
            
            if (errorResults.Count > 0) {
                writter.WriteInformationLine(Resources.ConsoleFormatteFailuresMessage);
                writter.WriteLine();

                for (var index = 0; index < errorResults.Count; index++) {
                    var example = errorResults [index];
                    writter.WriteInformationLine(string.Format(CultureInfo.CurrentCulture,
                                                                    Resources.ConsoleFormatteErrorsMessage, 
                                                                    index + 1, example.Reason));
                    var prefix = new string(' ', index.ToString(CultureInfo.InvariantCulture).Length + 2);
                    writter.WriteErrorLine(string.Format(CultureInfo.CurrentCulture,
                                                              Resources.ConsoleFormatteFailureMessage, 
                                                              prefix, example.ExceptionAsString));
                    writter.WriteLine();
                }
            }

            var elapsdeTimeMessage = string.Format(CultureInfo.CurrentCulture, 
                                                   Resources.ConsoleFormatterElapsedTimeMessage, 
                                                   expressionResults.ElapsedTime() / 1000D);
            writter.WriteInformationLine(elapsdeTimeMessage);

            var summaryMessage = string.Format(
                CultureInfo.CurrentCulture,
                Resources.ConsoleFormatterSummaryMessage,
                expressionResults.AllSuccesses().Count() + errorResults.Count,
                errorResults.Count);

            if (errorResults.Count > 0) {
                writter.WriteErrorLine(summaryMessage);
            } else {
                writter.WriteSuccessLine(summaryMessage);
            }
        }
    }
}