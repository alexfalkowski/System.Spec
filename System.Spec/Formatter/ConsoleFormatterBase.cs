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
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Monad.Maybe;
    using System.Xml;
    using System.Xml.Serialization;

    using NUnit.Framework;

    using System.Spec.Properties;

    [Serializable]
    public abstract class ConsoleFormatterBase : IConsoleFormatter
    {
        private IConsoleWritter writter;

        protected ConsoleFormatterBase(IConsoleWritter writter) 
        {
            this.writter = writter;
        }

        public abstract void WriteInformation(string message);

        public abstract void WriteSuccess(ExampleResult example);

        public abstract void WriteError(ExampleResult example);

        public virtual void WriteSummary(IEnumerable<ExpressionResult> expressions)
        {        
            var errorResults = expressions.AllErrors().ToList();

            this.writter.WriteLine();
            
            if (errorResults.Count > 0) {
                this.writter.WriteInformationLine(Resources.ConsoleFormatteFailuresMessage);
                this.writter.WriteLine();

                for (var index = 0; index < errorResults.Count; index++) {
                    var example = errorResults [index];
                    this.writter.WriteInformationLine(string.Format(CultureInfo.CurrentCulture,
                                                                    Resources.ConsoleFormatteErrorsMessage, 
                                                                    index + 1, example.Reason));
                    var prefix = new string(' ', index.ToString().Length + 2);
                    this.writter.WriteErrorLine(string.Format(CultureInfo.CurrentCulture,
                                                              Resources.ConsoleFormatteFailureMessage, 
                                                              prefix, example.StackTrace));
                    this.writter.WriteLine();
                }
            }

            var elapsdeTimeMessage = string.Format(CultureInfo.CurrentCulture, 
                                                   Resources.ConsoleFormatterElapsedTimeMessage, 
                                                   expressions.ElapsedTime() / 1000D);
            this.writter.WriteInformationLine(elapsdeTimeMessage);

            var summaryMessage = string.Format(
                CultureInfo.CurrentCulture,
                Resources.ConsoleFormatterSummaryMessage,
                expressions.AllSuccesses().Count() + errorResults.Count,
                errorResults.Count);

            if (errorResults.Count > 0) {
                this.writter.WriteErrorLine(summaryMessage);
            } else {
                this.writter.WriteSuccessLine(summaryMessage);
            }
        }
    }
}