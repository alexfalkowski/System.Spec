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

    public class DefaultTestDiscoverer : ITestDiscoverer
    {
        private ISpecificationRunner runner;

        public DefaultTestDiscoverer() : this(CreateSpecificationRunner())
        {
        }

        public DefaultTestDiscoverer(ISpecificationRunner runner)
        {
            this.runner = runner;
        }

        public void DiscoverTests(IEnumerable<string> sources, 
                                  IDiscoveryContext discoveryContext, 
                                  IMessageLogger logger, 
                                  ITestCaseDiscoverySink discoverySink)
        {
            foreach (var source in sources) {
                var results = runner.ExecuteSpecificationsInPath(source);

                var query = from result in results
                            from @group in result.Examples
                            from example in @group.Examples
                            select example;

                foreach (var example in query) {
                    discoverySink.SendTestCase(new TestCase(example.Reason, DefaultTestExecutor.ExecutorUri, source) {
                        CodeFilePath = source
                    });
                }
            }
        }

        private static ISpecificationRunner CreateSpecificationRunner()
        {
            return new DefaultSpecificationRunner(
                new DefaultExpressionRunner(new NoneActionStratergy()), 
                new DefaultSpecificationFinder(new DefaultFileSystem()), 
                new SilentConsoleFormatter(new DefaultConsoleWritter()));
        }
    }
}