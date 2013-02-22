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
    using System.Linq;
    using System.IO;
    using System.Xml.Serialization;

    using NUnit.Framework;

    using System.Spec.Properties;

    public abstract class ConsoleFormatterBase : IConsoleFormatter
    {
        private readonly IList<ExampleResult> errorResults = new Collection<ExampleResult>();
        private readonly IList<ExampleResult> successResults = new Collection<ExampleResult>();

        protected IList<ExampleResult> ErrorResults {
            get {
                return this.errorResults;
            }
        }

        protected IList<ExampleResult> SuccessResults {
            get {
                return this.successResults;
            }
        }

        public bool HasErrors {
            get {
                return this.errorResults.Any();
            }
        }

        public abstract void WriteInformation(string message);

        public virtual void WriteSuccess(string reason, ExampleResult example)
        {
            this.SuccessResults.Add(example);
        }

        public virtual void WriteError(string reason, ExampleResult example)
        {
            this.ErrorResults.Add(example);
        }

        public virtual void WriteSummary(long elapsedMilliseconds)
        {
            var elapsdeTimeMessage = string.Format(
                CultureInfo.CurrentCulture, Resources.ConsoleFormatterElapsedTimeMessage, elapsedMilliseconds / 1000D);
            Console.WriteLine(elapsdeTimeMessage);

            var errorCount = this.ErrorResults.Count;
            var summaryMessage = string.Format(
                CultureInfo.CurrentCulture,
                Resources.ConsoleFormatterSummaryMessage,
                this.SuccessResults.Count + errorCount,
                errorCount);
            Console.WriteLine(summaryMessage);
        }

        public void WriteSummaryToStream(Stream stream, long elapsedMilliseconds)
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