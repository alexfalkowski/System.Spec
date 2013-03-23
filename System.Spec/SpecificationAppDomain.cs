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

namespace System.Spec
{
    using Collections.Generic;
    using Globalization;
    using Reflection;
    using Runners;
    using System.IO;

    public class SpecificationAppDomain
    {
        private readonly ISpecificationRunner runner;

        public SpecificationAppDomain(ISpecificationRunner runner)
        {
            this.runner = runner;
        }

        public IEnumerable<ExpressionResult> ExecuteSpecifications(string path, string example = null)
        {
            var name = Guid.NewGuid().ToString();
            var domainSetup = new AppDomainSetup {
                ApplicationName = name,
                ApplicationBase = Path.GetDirectoryName(path),
                ConfigurationFile = string.Concat(Path.GetFileName(path), ".config")
            };

            var domain = AppDomain.CreateDomain(name, null, domainSetup);

            try {
                var assembly = (SpecificationAssembly)domain.CreateInstanceAndUnwrap(
                    typeof(SpecificationAssembly).Assembly.FullName,
                    typeof(SpecificationAssembly).FullName,
                    false,
                    BindingFlags.Default,
                    null,
                    new object[] { runner },
                    CultureInfo.CurrentCulture,
                    null);

                return assembly.ExecuteSpecifications(path, example);
            } finally {
                AppDomain.Unload(domain);
            }
        }
    }
}