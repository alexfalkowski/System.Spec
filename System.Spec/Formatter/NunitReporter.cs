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

    public class NunitReporter : IReporter
    {
        private bool hasErrors;
        private ExampleGroupResultCollection exampleResults;

        public NunitReporter(bool hasErrors, ExampleGroupResultCollection exampleResults)
        {
            this.hasErrors = hasErrors;
            this.exampleResults = exampleResults;
        }

        public void Write(Stream stream, long elapsedMilliseconds)
        {
            var testsuite = new testsuiteType {
                name = "Specifications",
                type = "Assembly",
                executed = bool.TrueString,
                result = hasErrors ? "Failure" : "Success",
                asserts = "0",
                time = ConvertToSeconds(elapsedMilliseconds).ToString(),
            };

            var errorCount = this.exampleResults.AllErrors.Count();

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
                total = this.exampleResults.AllSuccess.Count() + errorCount,
                name = "Results",
                date = DateTime.Now.ToString("yyyy-MM-dd"),
                time = DateTime.Now.ToString("H:mm:ss")
            };

            var types = new Collection<testsuiteType>();
        
            foreach (var result in this.exampleResults) {
                var successElapsedTimeQuery = from success in result.SuccessResults
                                              select success.ElapsedTime;
                var errorElapsedTimeQuery = from error in result.ErrorResults
                                            select error.ElapsedTime;
                var totalTime = successElapsedTimeQuery.Sum() + errorElapsedTimeQuery.Sum();
                var hasResultErrors = result.ErrorResults.Any();
                var type = new testsuiteType { 
                    name = result.Name, 
                    type = "TestFixture",
                    result = hasResultErrors ? "Failure" : "Success",
                    executed = bool.TrueString,
                    time = ConvertToSeconds(totalTime).ToString(),
                    asserts = "0",
                    success = hasResultErrors ? bool.FalseString : bool.TrueString
                };
                var cases = new Collection<testcaseType>();
            
                foreach (var error in result.ErrorResults) {
                    var failure = new failureType {
                        message = this.CreateCDataSection(error.Exception.Message),
                        stacktrace = this.CreateCDataSection(error.Exception.StackTrace)
                    };
                        
                    var @case = new testcaseType { 
                        name = error.Reason, 
                        executed = bool.TrueString, 
                        success = bool.FalseString,
                        result = "Failure",
                        asserts = "0",
                        time = ConvertToSeconds(error.ElapsedTime).ToString(),
                        Item = failure
                    };
                        
                    cases.Add(@case);
                }
            
                foreach (var success in result.SuccessResults) {
                    var @case = new testcaseType { 
                        name = success.Reason, 
                        executed = bool.TrueString, 
                        success = bool.TrueString,
                        result = "Success",
                        asserts = "0",
                        time = ConvertToSeconds(success.ElapsedTime).ToString()
                    };
                
                    cases.Add(@case);
                }
            
                type.results = cases.ToArray();
                types.Add(type);
            }
        
            testsuite.results = types.ToArray();
        
            var serializer = new XmlSerializer(typeof(resultType));
            serializer.Serialize(stream, resultType);
        }

        private double ConvertToSeconds(double elapsedTime)
        {
            return elapsedTime / 1000D;
        }
    
        private XmlCDataSection CreateCDataSection(string value)
        {
            var document = new XmlDocument();
            return document.CreateCDataSection(value);
        }
    }
}