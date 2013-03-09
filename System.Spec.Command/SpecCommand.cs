//  Author:
//       alexfalkowski <alexrfalkowski@gmail.com>
//
//  Copyright (c) 2013 alexfalkowski
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

namespace System.Spec.Command
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.Reflection;

    using System.Spec.Formatter;
    using System.Spec.Reports;
    using System.Spec.IO;
    using System.Spec.Runners;
    
    using PowerArgs;
    
    public class SpecCommand
    {
        private string[] args;
        private IConsoleFormatterFactory formatterFactory;
        private ISpecificationReporter reporter;
        private IFileSystem fileSystem;
        private IConsoleWritterFactory consoleFactory;
        private IExpressionRunnerFactory expressionFactory;
        private ISpecificationFinder finder;
        private ISpecificationRunnerFactory runnerFactory;
        
        public SpecCommand(string[] args, 
                           IConsoleFormatterFactory formatterFactory,
                           ISpecificationReporter reporter,
                           IFileSystem fileSystem,
                           IConsoleWritterFactory consoleFactory,
                           IExpressionRunnerFactory expressionFactory,
                           ISpecificationFinder finder,
                           ISpecificationRunnerFactory runnerFactory)
        {
            this.args = args;
            this.formatterFactory = formatterFactory;
            this.reporter = reporter;
            this.fileSystem = fileSystem;
            this.consoleFactory = consoleFactory;
            this.expressionFactory = expressionFactory;
            this.finder = finder;
            this.runnerFactory = runnerFactory;
        }

        public int Run()
        {
            try {
                var arguments = Args.Parse<Arguments>(args);
                var writter = this.consoleFactory.CreateConsoleWritter(arguments.Colour);

                if (arguments.Help) {
                    writter.WriteInformationLine(ArgUsage.GetUsage<Arguments>());
                    return 0;
                }

                if (arguments.Version) {
                    writter.WriteInformationLine(Assembly.GetEntryAssembly().GetName().Version.ToString());
                    return 0;
                }

                var consoleFormatter = this.formatterFactory.CreateConsoleFormatter(arguments.Format, writter);
                var expressionRunner = this.expressionFactory.CreateExpressionRunner(arguments.DryRun);
                var specRunner = this.runnerFactory.CreateSpecificationRunner(arguments.Parrallel, expressionRunner, finder, consoleFormatter);
                var files = this.finder.GetSpecificationFiles(arguments.Path, arguments.Pattern);
                var appDomain = new SpecificationAppDomain(specRunner);
                var results = new List<ExpressionResult>();

                foreach (var file in files) {
                    results.AddRange(appDomain.ExecuteSpecifications(file, arguments.Example));
                }

                consoleFormatter.WriteSummary(results);
                this.reporter.Write(this.fileSystem.OpenWrite(arguments.Output), results);

                return results.HasErrors() ? 1 : 0;
            } catch (ArgException) {
                var consoleFormatter = this.consoleFactory.CreateConsoleWritter(false);
                consoleFormatter.WriteInformationLine(ArgUsage.GetUsage<Arguments>());
                return 1;
            } catch (Exception e) {
                var consoleFormatter = this.consoleFactory.CreateConsoleWritter(false);
                consoleFormatter.WriteInformationLine(e.ToString().Trim());
                return 1;
            }
        }
    }
}