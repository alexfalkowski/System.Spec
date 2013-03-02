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
        
        public SpecCommand(string[] args, 
            IConsoleFormatterFactory formatterFactory,
            ISpecificationReporter reporter,
            IFileSystem fileSystem)
        {
            this.args = args;
            this.formatterFactory = formatterFactory;
            this.reporter = reporter;
            this.fileSystem = fileSystem;
        }

        public int Perform()
        {
            try {
                var arguments = Args.Parse<Arguments>(args);
                if (arguments.Help) {
                    Console.WriteLine(ArgUsage.GetUsage<Arguments>());
                    return 0;
                }

                if (arguments.Version) {
                    Console.WriteLine(Assembly.GetEntryAssembly().GetName().Version);
                    return 0;
                }
                
                IConsoleFormatter consoleFormatter = this.formatterFactory.CreateConsoleFormatter(arguments.Format);
                ISpecificationFinder finder = new DefaultSpecificationFinder(fileSystem);
                IActionStrategy actionStratergy = this.CreateActionStrategy(arguments.DryRun);
                IExpressionRunner runner = new DefaultExpressionRunner(actionStratergy);
                ISpecificationRunner command = this.CreateSpecificationRunner(arguments.Parrallel, 
                                                                              runner, 
                                                                              finder, 
                                                                              consoleFormatter);
                
                var results = command.ExecuteSpecificationsInPath(arguments.Path, arguments.Pattern, arguments.Example);

                consoleFormatter.WriteSummary(results);
                this.reporter.Write(this.fileSystem.OpenWrite(arguments.Output), results);

                return results.HasErrors() ? 1 : 0;
            } catch (ArgException) {
                Console.WriteLine(ArgUsage.GetUsage<Arguments>());
                return 1;
            } catch (Exception e) {
                Console.WriteLine(string.Format(CultureInfo.CurrentCulture, "Could not run specs: {0}", e.Message));
                return 1;
            }
        }

        private ISpecificationRunner CreateSpecificationRunner(bool parrallel, 
                                                               IExpressionRunner runner, 
                                                               ISpecificationFinder finder, 
                                                               IConsoleFormatter formatter)
        {
            if (parrallel) {
                return new ParallelSpecificationRunner(runner, finder, formatter);
            }

            return new DefaultSpecificationRunner(runner, finder, formatter);
        }

        private IActionStrategy CreateActionStrategy(bool dryRun)
        {
            if (dryRun) {
                return new NoneActionStrategy();
            }
            
            return new DefaultActionStrategy();
        }
    }
}