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

namespace System.Spec.VisualStudio
{
    using Microsoft.VisualStudio.TestPlatform.ObjectModel;
    using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;
    using Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging;
    using System.Collections.Generic;
    using System.Linq;
    using System.Spec.Formatter;
    using System.Spec.IO;
    using System.Spec.Runners;

    [DefaultExecutorUri(DefaultTestExecutor.ExecutorUriString)]
    [ExtensionUri(DefaultTestExecutor.ExecutorUriString)]
    public class DefaultTestExecutor : ITestExecutor
    {
        public const string ExecutorUriString = "executor://System.Spec.VisualStudio.DefaultTestExecutor/v1";
        public static readonly Uri ExecutorUri = new Uri(ExecutorUriString);
        private SpecificationAppDomain appDomainRunner;

        public DefaultTestExecutor()
            : this(CreateSpecificationRunner())
        {
        }

        public DefaultTestExecutor(ISpecificationRunner runner)
        {
            appDomainRunner = new SpecificationAppDomain(runner);
        }

        public void Cancel()
        {
            throw new NotSupportedException();
        }

        public void RunTests(IEnumerable<string> sources, IRunContext runContext, IFrameworkHandle frameworkHandle)
        {
            var validSources = from source in sources
                               where source.EndsWith(StringHelper.GetSearchExpression(), StringComparison.CurrentCultureIgnoreCase)
                               select source;

            foreach (var source in validSources) {
                RunTest(frameworkHandle, source);
            }
        }

        public void RunTests(IEnumerable<TestCase> tests, IRunContext runContext, IFrameworkHandle frameworkHandle)
        {
            foreach (var test in tests) {
                RunTest(frameworkHandle, test.Source, test.DisplayName);
            }
        }

        private void RunTest(IFrameworkHandle frameworkHandle, string source, string spec = null)
        {
            var results = appDomainRunner.ExecuteSpecifications(source, spec);
            var query = from result in results
                        from @group in result.Examples
                        from example in @group.Examples
                        select example;

            foreach (var example in query) {
                var testCase = new TestCase(example.Reason, DefaultTestExecutor.ExecutorUri, source) {
                    CodeFilePath = source
                };

                frameworkHandle.RecordStart(testCase);
                var testResult = new TestResult(testCase) {
                    DisplayName = example.Reason,
                    Duration = new TimeSpan(example.ElapsedTime),
                };

                if (example.Status == ResultStatus.Error) {
                    testResult.Outcome = TestOutcome.Failed;
                    testResult.ErrorMessage = example.Exception.Message;
                    testResult.ErrorStackTrace = example.Exception.StackTrace;
                }

                if (example.Status == ResultStatus.Success) {
                    testResult.Outcome = TestOutcome.Passed;
                }

                frameworkHandle.RecordEnd(testCase, testResult.Outcome);
                frameworkHandle.RecordResult(testResult);
            }
        }

        private static ISpecificationRunner CreateSpecificationRunner()
        {
            return new DefaultSpecificationRunner(
                new DefaultExpressionRunnerFactory().CreateExpressionRunner(false),
                new DefaultSpecificationFinder(new DefaultFileSystem()),
                new DefaultConsoleFormatterFactory().CreateConsoleFormatter(ConsoleFormatterType.Silent, new DefaultConsoleWritter()));
        }
    }
}
