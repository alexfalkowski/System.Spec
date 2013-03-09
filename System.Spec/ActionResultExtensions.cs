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
    public static class ActionResultExtensions
    {
        public static ExampleResult ToExampleResult(this ActionResult result, Example example)
        {
            return new ExampleResult {
                Reason = example.Reason,
                Status = result.Status,
                Exception = result.Exception,
                ElapsedTime = result.ElapsedTime,
                FileName = example.FileName,
                LineNumber = example.LineNumber
            };
        }

        public static ExampleGroupResult ToExampleGroupResult(this ActionResult result, string reason)
        {
            return new ExampleGroupResult { Reason = reason };
        }
    }
}