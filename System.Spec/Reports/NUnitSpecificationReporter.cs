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

    public class NUnitSpecificationReporter : ISpecificationReporter
    {
        public void Write(Stream stream, ExpressionResultCollection expressions)
        {
            var testsuite = new testsuiteType {
                name = "Specifications",
                type = "Assembly",
                executed = bool.TrueString,
                result = expressions.HasErrors ? "Failure" : "Success",
                asserts = "0",
                time = ConvertToSeconds(expressions.ElapsedTime).ToString(),
            };
            
            var errorCount = expressions.AllErrors.Count();
            
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
                total = expressions.AllSuccess.Count() + errorCount,
                name = "Results",
                date = DateTime.Now.ToString("yyyy-MM-dd"),
                time = DateTime.Now.ToString("H:mm:ss")
            };

            var resultTypes = new Collection<testsuiteType>();

            foreach (var expression in expressions) {
                var hasResultErrors = expression.Examples.HasErrors();
                var expressionType = new testsuiteType { 
                    name = expression.Name, 
                    type = "Namespace",
                    result =  hasResultErrors ? "Failure" : "Success",
                    executed = bool.TrueString,
                    time = ConvertToSeconds(expression.Examples.ElapsedTime()).ToString(),
                    asserts = "0",
                    success = hasResultErrors ? bool.FalseString : bool.TrueString
                };

                var groupTypes = new Collection<testsuiteType>();

                foreach (var group in expression.Examples) {
                    var hasGroupErrors = group.Examples.HasErrors();
                    var groupType = new testsuiteType { 
                        name = group.Reason, 
                        type = "TestFixture",
                        result =  hasGroupErrors ? "Failure" : "Success",
                        executed = bool.TrueString,
                        time = ConvertToSeconds(group.Examples.ElapsedTime()).ToString(),
                        asserts = "0",
                        success = hasGroupErrors ? bool.FalseString : bool.TrueString
                    };

                    var cases = new Collection<testcaseType>();
                    
                    foreach (var error in group.Examples.AllErrors()) {
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
                    
                    foreach (var success in group.Examples.AllSuccesses()) {
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
                    
                    groupType.results = cases.ToArray();
                    groupTypes.Add(groupType);
                }

                expressionType.results = groupTypes.ToArray();
                resultTypes.Add(expressionType);
            }
            
            testsuite.results = resultTypes.ToArray();
            
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