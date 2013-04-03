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

namespace System.Spec.Reports
{
    using Collections.Generic;
    using Collections.ObjectModel;
    using Globalization;
    using Linq;
    using NUnit.Framework;
    using System;
    using System.IO;
    using Xml;
    using Xml.Serialization;

    public class NUnitSpecificationReporter : ISpecificationReporter
    {
        public void Write(Stream stream, IEnumerable<ExpressionResult> expressions)
        {
            var expressionResults = expressions as ExpressionResult[] ?? expressions.ToArray();
            var testsuite = new testsuiteType
                {
                    name = "Specifications",
                    type = "Assembly",
                    executed = bool.TrueString,
                    result = expressionResults.HasErrors() ? "Failure" : "Success",
                    asserts = "0",
                    time = ConvertToSeconds(expressionResults.ElapsedTime()).ToString(CultureInfo.InvariantCulture),
                };
            
            var errorCount = expressionResults.AllErrors().Count();
            var resultType = CreateResultType(expressionResults, testsuite, errorCount);
            var resultTypes = new Collection<testsuiteType>();

            foreach (var expression in expressionResults) {
                var hasResultErrors = expression.Examples.HasErrors();
                var expressionType = new testsuiteType
                    {
                        name = expression.Name,
                        type = "Namespace",
                        result = hasResultErrors ? "Failure" : "Success",
                        executed = bool.TrueString,
                        time =
                            ConvertToSeconds(expression.Examples.ElapsedTime()).ToString(CultureInfo.InvariantCulture),
                        asserts = "0",
                        success = hasResultErrors ? bool.FalseString : bool.TrueString
                    };

                var groupTypes = new Collection<testsuiteType>();

                CreateExamples(expression, groupTypes);

                expressionType.results = groupTypes.ToArray();
                resultTypes.Add(expressionType);
            }
            
            testsuite.results = resultTypes.ToArray();
            
            var serializer = new XmlSerializer(typeof(resultType));
            serializer.Serialize(stream, resultType);
        }

        private static void CreateExamples(ExpressionResult expression, ICollection<testsuiteType> groupTypes)
        {
            foreach (var group in expression.Examples) {
                var hasGroupErrors = group.Examples.HasErrors();
                var groupType = new testsuiteType
                    {
                        name = group.Reason,
                        type = "TestFixture",
                        result = hasGroupErrors ? "Failure" : "Success",
                        executed = bool.TrueString,
                        time = ConvertToSeconds(group.Examples.ElapsedTime()).ToString(CultureInfo.InvariantCulture),
                        asserts = "0",
                        success = hasGroupErrors ? bool.FalseString : bool.TrueString
                    };
                var cases = new Collection<testcaseType>();
                CreateErrors(group, cases);
                CreateSuccesses(group, cases);
                groupType.results = cases.ToArray();
                groupTypes.Add(groupType);
            }
        }

        private static resultType CreateResultType(IEnumerable<ExpressionResult> expressions, testsuiteType testsuite,
                                                   int errorCount)
        {
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
                testsuite = testsuite,
                errors = errorCount,
                total = expressions.AllSuccesses().Count() + errorCount,
                name = "Results",
                date = DateTime.Now.ToString("yyyy-MM-dd"),
                time = DateTime.Now.ToString("H:mm:ss")
            };
            
            return resultType;
        }

        private static void CreateErrors(ExampleGroupResult group, Collection<testcaseType> cases)
        {
            var testcaseTypes = from error in @group.Examples.AllErrors()
                                let failure = new failureType
                                    {
                                        message = CreateCDataSection(error.Message),
                                        stacktrace = CreateCDataSection(error.StackTrace)
                                    }
                                select new testcaseType
                                    {
                                        name = error.Reason,
                                        executed = bool.TrueString,
                                        success = bool.FalseString,
                                        result = "Failure",
                                        asserts = "0",
                                        time = ConvertToSeconds(error.ElapsedTime).ToString(CultureInfo.InvariantCulture),
                                        Item = failure
                                    };
            foreach (var @case in testcaseTypes)
            {
                cases.Add(@case);
            }
        }

        private static void CreateSuccesses(ExampleGroupResult group, ICollection<testcaseType> cases)
        {
            var testcaseTypes = @group.Examples.AllSuccesses().Select(success => new testcaseType
                {
                    name = success.Reason,
                    executed = bool.TrueString,
                    success = bool.TrueString,
                    result = "Success",
                    asserts = "0",
                    time = ConvertToSeconds(success.ElapsedTime).ToString(CultureInfo.InvariantCulture)
                });
            foreach (var @case in testcaseTypes)
            {
                cases.Add(@case);
            }
        }

        private static double ConvertToSeconds(double elapsedTime)
        {
            return elapsedTime / 1000D;
        }
        
        private static XmlCDataSection CreateCDataSection(string value)
        {
            var document = new XmlDocument();
            return document.CreateCDataSection(value);
        }
    }
}