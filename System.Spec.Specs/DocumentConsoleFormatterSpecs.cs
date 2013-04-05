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

namespace System.Spec.Specs
{
    using Collections.ObjectModel;
    using FluentAssertions;
    using Formatter;
    using Globalization;
    using NUnit.Framework;
    using Properties;
    using System;
    using System.IO;

    [TestFixture]
    public class DocumentConsoleFormatterSpecs
    {
        private StringWriter stringWriter;
        private IConsoleFormatter consoleFormatter;
        private TextWriter originalWritter;

        [SetUp]
        public void BeforeEach()
        {
            originalWritter = Console.Out;
            stringWriter = new StringWriter(CultureInfo.CurrentCulture);
            Console.SetOut(stringWriter);

            consoleFormatter = new DocumentionConsoleFormatter(new DefaultConsoleWritter());
            consoleFormatter.WriteInformation(Resources.TestReason);
        }

        [TearDown]
        public void AfterEach()
        {
            stringWriter.Dispose();
            Console.SetOut(originalWritter);
        }

        [Test]
        public void ShouldWriteSuccess()
        {
            var result = new ExampleResult { Reason = Resources.TestReason, Status = ResultStatus.Success };
            consoleFormatter.WriteSuccess(result);
            stringWriter.Flush();
            var value = Environment.NewLine + Resources.TestReason + Environment.NewLine + 
                StringHelper.DoubleSpace + Resources.TestReason + Environment.NewLine;
            stringWriter.ToString().Should().Be(value);
        }

        [Test]
        public void ShouldWriteError()
        {
            consoleFormatter.WriteError(new ExampleResult {
                        Reason = Resources.TestReason,
                        Status = ResultStatus.Error,
                        StackTrace = new InvalidOperationException("Test Exception").ToString()
                    });
            stringWriter.Flush();
            var value = Environment.NewLine + Resources.TestReason + Environment.NewLine + 
                StringHelper.DoubleSpace + Resources.TestReason + " (FAILED)" + Environment.NewLine;
            stringWriter.ToString().Should().Be(value);
        }

        [Test]
        public void ShouldWriteSummary()
        {
            var results = new Collection<ExpressionResult>();
            
            consoleFormatter.WriteSummary(results);
            stringWriter.Flush();
            var value = Environment.NewLine + Resources.TestReason + Environment.NewLine + Environment.NewLine +
                "Finished in 0 seconds" + Environment.NewLine + 
                "0 examples, 0 failures" + Environment.NewLine;
            stringWriter.ToString().Should().Be(value);
        }

        [Test]
        public void ShouldWriteSummaryWithOneSuccess()
        {
            var results = new Collection<ExpressionResult>();
            var result = new ExpressionResult();
            var group = new ExampleGroupResult();
            var example = new ExampleResult { ElapsedTime = 1000 };
            group.Examples.Add(example);
            result.Examples.Add(group);
            results.Add(result);

            consoleFormatter.WriteSummary(results);
            stringWriter.Flush();
            var value = Environment.NewLine + Resources.TestReason + Environment.NewLine + Environment.NewLine +
                "Finished in 1 seconds" + Environment.NewLine + 
                "1 examples, 0 failures" + Environment.NewLine;
            stringWriter.ToString().Should().Be(value);
        }
    }
}