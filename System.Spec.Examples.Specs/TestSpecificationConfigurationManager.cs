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

namespace System.Spec.Examples.Specs
{
    using System.IO;
    using System.Spec;
    using System.Configuration;
    using System.Reflection;

    using FluentAssertions;

    public class TestSpecificationConfigurationManager : Specification
    {
        protected override void Define()
        {
            Describe("Configuration Manager", () => {
                It("should find Test1 in ConfigurationManager", () => {
                    ConfigurationManager.AppSettings ["Test1"].Should().Be("Test1");
                });

                It("should find Test2 in ConfigurationManager", () => {
                    ConfigurationManager.AppSettings ["Test2"].Should().Be("Test2");
                });
            });
        }
    }
}