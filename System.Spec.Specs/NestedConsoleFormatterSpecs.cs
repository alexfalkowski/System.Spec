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
    public class NestedConsoleFormatterSpecs
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

            this.consoleFormatter = new NestedConsoleFormatter();
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
            var result = new ExampleResult { Reason = Resources.TestReason, Status = ResultStatus.Success };
            this.consoleFormatter.WriteSuccess(result);
            this.stringWriter.Flush();
            this.stringWriter.ToString().Should().Be(Resources.TestReason + Environment.NewLine + StringHelper.Tab + Resources.TestReason + Environment.NewLine);
        }

        [Test]
        public void ShouldWriteError()
        {
            this.consoleFormatter.WriteError(new ExampleResult {
                        Reason = Resources.TestReason,
                        Status = ResultStatus.Error,
                        Exception = new InvalidOperationException("Test Exception")
                    });
            this.stringWriter.Flush();
            this.stringWriter.ToString().Should().Be(
                Resources.TestReason + Environment.NewLine + StringHelper.Tab + Resources.TestReason + " - System.InvalidOperationException: Test Exception" + Environment.NewLine);
        }

        [Test]
        public void ShouldWriteSummary()
        {
            var results = new Collection<ExpressionResult>();
            
            this.consoleFormatter.WriteSummary(results);
            this.stringWriter.Flush();
            this.stringWriter.ToString().Should().Be(Resources.TestReason + 
                Environment.NewLine + 
                "Finished in 0 seconds" + Environment.NewLine + 
                "0 examples, 0 failures" + Environment.NewLine);
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

            this.consoleFormatter.WriteSummary(results);
            this.stringWriter.Flush();
            this.stringWriter.ToString().Should().Be(Resources.TestReason + 
                Environment.NewLine + Environment.NewLine + 
                "Finished in 1 seconds" + Environment.NewLine + 
                "1 examples, 0 failures" + Environment.NewLine);
        }
    }
}