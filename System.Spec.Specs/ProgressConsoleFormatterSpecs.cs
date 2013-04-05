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
    using System;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.IO;

    using FluentAssertions;

    using System.Spec.Formatter;
    using System.Spec.Specs.Properties;

    using NUnit.Framework;

    [TestFixture]
    public class ProgressConsoleFormatterSpecs
    {
        private StringWriter stringWriter;
        private IConsoleFormatter consoleFormatter;
        private TextWriter originalWritter;

        [SetUp]
        public void BeforeEach()
        {
            this.originalWritter = Console.Out;
            this.stringWriter = new StringWriter(CultureInfo.CurrentCulture);
            Console.SetOut(this.stringWriter);

            this.consoleFormatter = new ProgressConsoleFormatter(new DefaultConsoleWritter());
            this.consoleFormatter.WriteInformation(Resources.TestReason);
        }

        [TearDown]
        public void AfterEach()
        {
            this.stringWriter.Dispose();
            Console.SetOut(this.originalWritter);
        }

        [Test]
        public void ShouldWriteSuccess()
        {
            this.consoleFormatter.WriteSuccess(new ExampleResult { Reason = Resources.TestReason });
            this.stringWriter.Flush();
            this.stringWriter.ToString().Should().Be(".");
        }

        [Test]
        public void ShouldWriteError()
        {
            this.consoleFormatter.WriteError(new ExampleResult { Reason = Resources.TestReason });
            this.stringWriter.Flush();
            this.stringWriter.ToString().Should().Be("F");
        }

        [Test]
        public void ShouldWriteSummary()
        {
            var example = new ExampleResult {
                Reason = Resources.TestReason,
                ElapsedTime = 1000,
                Message = "Test Exception",
                StackTrace = "System.InvalidOperationException: Test Exception",
                Status = ResultStatus.Error
            };
            var results = new Collection<ExpressionResult>();
            var expression = new ExpressionResult();
            var group = new ExampleGroupResult();
            group.Examples.Add(example);
            expression.Examples.Add(group);
            results.Add(expression);

            this.consoleFormatter.WriteError(example);
            this.consoleFormatter.WriteSummary(results);
            this.stringWriter.Flush();

            var value = "F" + Environment.NewLine + Environment.NewLine +
                "Failures:" + Environment.NewLine + Environment.NewLine +
                "1) test" + Environment.NewLine +
                "   Failure/Error: System.InvalidOperationException: Test Exception" + Environment.NewLine + Environment.NewLine + 
                "Finished in 1 seconds" + Environment.NewLine + 
                "1 examples, 1 failures" + Environment.NewLine;
            this.stringWriter.ToString().Should().Be(value);
        }
    }
}