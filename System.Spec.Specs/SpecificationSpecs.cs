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
    using Examples.Specs;
    using FluentAssertions;
    using Linq;
    using NUnit.Framework;

    [TestFixture]
    public class SpecificationSpecs
    {
        [Test]
        public void ShouldValidateSingleDescribe()
        {
            var specification = new TestSpecificationWithJustDescribe();
            var result = specification.BuildExpression();

            result.Examples.Should().HaveCount(1);
        }

        [Test]
        public void ShouldValidateNestedDescribe()
        {
            var specification = new TestSpecificationWithNestedDescribe();
            var result = specification.BuildExpression();

            result.Examples.Should().HaveCount(2);
        }

        [Test]
        public void ShouldValidateSingleDescribeWithSingleIt()
        {
            var specification = new TestSpecificationWithSingleIt();
            var result = specification.BuildExpression();

            result.Examples.Should().HaveCount(1);
            var example = result.Examples.First();
            example.Examples.Should().HaveCount(1);
        }

        [Test]
        public void ShouldValidateSingleDescribeWithSingleItAndBeforeEach()
        {
            var specification = new TestSpecificationWithSingleItWithBeforeEach();
            var result = specification.BuildExpression();

            result.Examples.Should().HaveCount(1);
            var example = result.Examples.First();
            example.Examples.Should().HaveCount(1);
            example.BeforeEach.Should().NotBeNull();
        }

        [Test]
        public void ShouldValidateSingleDescribeWithSingleItAndBeforeEachAndAfterEach()
        {
            var specification = new TestSpecificationWithSingleItWithBeforeEachAndAfterEach();
            var result = specification.BuildExpression();
           
            result.Examples.Should().HaveCount(1);
            var example = result.Examples.First();
            example.Examples.Should().HaveCount(1);
            example.BeforeEach.Should().NotBeNull();
            example.AfterEach.Should().NotBeNull();
        }

        [Test]
        public void ShouldValidateSpecificationWithBeforeAll()
        {
            var specification = new TestSpecificationWithBeforeAll();
            var result = specification.BuildExpression();

            result.Examples.Should().HaveCount(1);
            var example = result.Examples.First();
            example.Examples.Should().HaveCount(1);
            example.BeforeEach.Should().NotBeNull();
            example.AfterEach.Should().NotBeNull();
            example.BeforeAll.Should().NotBeNull();
        }

        [Test]
        public void ShouldValidateSpecificationWithBeforeAllAndAfterAll()
        {
            var specification = new TestSpecificationWithBeforeAllAndAfterAll();
            var result = specification.BuildExpression();

            result.Examples.Should().HaveCount(1);
            var example = result.Examples.First();
            example.Examples.Should().HaveCount(1);
            example.BeforeEach.Should().NotBeNull();
            example.AfterEach.Should().NotBeNull();
            example.BeforeAll.Should().NotBeNull();
            example.AfterAll.Should().NotBeNull();
        }

        [Test]
        public void ShouldValidateWithFluentAssertions()
        {
            var specification = new TestSpecificationWithFluentAssertions();
            var result = specification.BuildExpression();

            result.Examples.Should().HaveCount(1);
            var example = result.Examples.First();
            example.Examples.Should().HaveCount(1);
        }

        [Test]
        public void ShouldValidateWithNSubstitute()
        {
            var specification = new TestSpecificationWithNSubstitute();
            var result = specification.BuildExpression();

            result.Examples.Should().HaveCount(1);
            var example = result.Examples.First();
            example.Examples.Should().HaveCount(1);
        }
    }
}