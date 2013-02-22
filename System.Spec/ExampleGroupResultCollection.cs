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
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    public class ExampleGroupResultCollection : Collection<ExampleGroupResult>
    {
        public bool HasErrors {
            get {
                foreach (var result in this.Items) {
                    if (result.ErrorResults.Any()) {
                        return true;
                    }
                }

                return false;
            }
        }

        public bool HasSuccess {
            get {
                foreach (var result in this.Items) {
                    if (result.SuccessResults.Any()) {
                        return true;
                    }
                }

                return false;
            }
        }

        public IEnumerable<ExampleResult> AllSuccess {
            get {
                return from result in this.Items
                        from success in result.SuccessResults
                        select success;
            }
        }

        public IEnumerable<ExampleResult> AllErrors {
            get {
                return from result in this.Items
                        from error in result.ErrorResults
                        select error;
            }
        }
    }
}