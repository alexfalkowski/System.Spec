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
    using System.Xml.Serialization;

    using NUnit.Framework;

    using System.Spec.Properties;

    public abstract class ConsoleFormatterBase : IConsoleFormatter
    {
        private readonly ExampleGroupResultCollection exampleResults = new ExampleGroupResultCollection();

        protected ExampleGroupResultCollection ExampleResults {
            get {
                return this.exampleResults;
            }
        }

        public bool HasErrors {
            get {
                return this.exampleResults.HasErrors;
            }
        }

        public virtual void WriteInformation(string message)
        {
            this.exampleResults.Add(new ExampleGroupResult { Name = message });
        }

        public virtual void WriteSuccess(string reason, ExampleResult example)
        {
            this.exampleResults.Last().SuccessResults.Add(example);
        }

        public virtual void WriteError(string reason, ExampleResult example)
        {
            this.exampleResults.Last().ErrorResults.Add(example);
        }

        public virtual void WriteSummary(long elapsedMilliseconds)
        {
            var elapsdeTimeMessage = string.Format(
                CultureInfo.CurrentCulture, Resources.ConsoleFormatterElapsedTimeMessage, elapsedMilliseconds / 1000D);
            Console.WriteLine(elapsdeTimeMessage);

            var errorCount = this.exampleResults.AllErrors.Count();
            var summaryMessage = string.Format(
                CultureInfo.CurrentCulture,
                Resources.ConsoleFormatterSummaryMessage,
                this.exampleResults.AllSuccess.Count() + errorCount,
                errorCount);
            Console.WriteLine(summaryMessage);
        }

        public virtual void WriteSummaryToStream(Stream stream, long elapsedMilliseconds)
        {
            var testsuite = new testsuiteType();
            var resultType = new resultType {
                environment = new environmentType {
                    nunitversion = typeof(TestAttribute).Assembly.GetName().Version.ToString(),
                    clrversion = Environment.Version.ToString(),
                    osversion = Environment.OSVersion.VersionString,
                    machinename = Environment.MachineName,
                    platform = Enum.GetName(typeof(PlatformID), Environment.OSVersion.Platform),
                    user = Environment.UserName,
                    userdomain = Environment.UserDomainName
                },

                cultureinfo = new cultureinfoType {
                    currentculture = CultureInfo.CurrentCulture.ToString(),
                    currentuiculture = CultureInfo.CurrentUICulture.ToString()
                },

                testsuite = testsuite
            };

            testsuite.executed = bool.TrueString;
            testsuite.result = this.HasErrors ? "Failure" : "Success";
            testsuite.time = (elapsedMilliseconds / 1000D).ToString();

            var serializer = new XmlSerializer(typeof(resultType));
            serializer.Serialize(stream, resultType);
        }
    }
}