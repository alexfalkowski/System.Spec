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
    
    using System.Spec.Formatter;
    
    using PowerArgs;
    
    public class SpecCommand
    {
        private string[] args;
        private IConsoleFormatterFactory formatterFactory;
        private IFileSystem fileSystem;
        private IActionStrategy exampleGroupStrategy;
        
        public SpecCommand(string[] args, 
            IConsoleFormatterFactory formatterFactory, 
            IFileSystem fileSystem, 
            IActionStrategy exampleGroupStrategy)
        {
            this.args = args;
            this.formatterFactory = formatterFactory;
            this.fileSystem = fileSystem;
            this.exampleGroupStrategy = exampleGroupStrategy;
        }
        
        public int Perform()
        {
            try {
                var arguments = Args.Parse<Arguments>(args);
                if (arguments.Help) {
                    Console.WriteLine(ArgUsage.GetUsage<Arguments>());
                    return 0;
                }
                
                IConsoleFormatter consoleFormatter = this.formatterFactory.CreateConsoleFormatter(arguments.Format);
                ISpecificationVisitor specificationVisitor = new DefaultSpecificationVisitor(consoleFormatter);
                IActionStrategy exampleStratergy = arguments.DryRun
                    ? (IActionStrategy)new NullActionStrategy()
                        : new DefaultActionStrategy();
                IRunner command = new DefaultRunner(
                    specificationVisitor, exampleGroupStrategy, exampleStratergy, fileSystem);
                
                var elapsedTime = StopwatchHelper.ExecuteTimedAction(() => {
                    command.ExecuteSpecificationsInPath(arguments.Example, arguments.Search);
                });

                consoleFormatter.WriteSummary(elapsedTime);
                consoleFormatter.WriteSummaryToStream(this.fileSystem.OpenWrite(arguments.Output), elapsedTime);

                return consoleFormatter.HasErrors ? 1 : 0;
            } catch (ArgException) {
                Console.WriteLine(ArgUsage.GetUsage<Arguments>());
                return 1;
            } catch (Exception e) {
                Console.WriteLine(string.Format(CultureInfo.CurrentCulture, "Could not run specs: {0}", e));
                return 1;
            }
        }
    }
}