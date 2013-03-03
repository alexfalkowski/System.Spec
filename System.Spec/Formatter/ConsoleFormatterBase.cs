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
    using System.Xml;
    using System.Xml.Serialization;

    using NUnit.Framework;

    using System.Spec.Properties;

    public abstract class ConsoleFormatterBase : IConsoleFormatter
    {
        public abstract void WriteInformation(string message);

        public abstract void WriteSuccess(ExampleResult example);

        public abstract void WriteError(ExampleResult example);

        public virtual void WriteSummary(IEnumerable<ExpressionResult> expressions)
        {        
            var errorResults = expressions.AllErrors().ToList();
            
            if (errorResults.Count > 0) {
                Console.WriteLine(Environment.NewLine + "Failures:" + Environment.NewLine);
                for (var index = 0; index < errorResults.Count; index++) {
                    var example = errorResults [index];
                    var numberFormatValue = string.Format("{0}) ", index + 1);
      
                    Console.WriteLine(numberFormatValue + example.Reason);
                    var prefix = new string(' ', numberFormatValue.Length);
                    Console.WriteLine(prefix + "Failure/Error: " + example.Exception.Message.Clean() + Environment.NewLine);
                }
            }

            var elapsdeTimeMessage = string.Format(CultureInfo.CurrentCulture, 
                                                   Resources.ConsoleFormatterElapsedTimeMessage, 
                                                   expressions.ElapsedTime() / 1000D);
            Console.WriteLine(elapsdeTimeMessage);

            var summaryMessage = string.Format(
                CultureInfo.CurrentCulture,
                Resources.ConsoleFormatterSummaryMessage,
                expressions.AllSuccesses().Count() + errorResults.Count,
                errorResults.Count);
            Console.WriteLine(summaryMessage);
        }
    }
}