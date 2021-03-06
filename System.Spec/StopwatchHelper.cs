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
    using Diagnostics;
    using System;

    public static class StopwatchHelper
    {
        public static ActionResult ExecuteTimedActionWithResult(Action action)
        {
            var stopwatch = new Stopwatch();
            var result = new ActionResult();

            try {
                stopwatch.Start();
                action();
                result.Status = ResultStatus.Success;
            } catch (Exception e) {
                result.Status = ResultStatus.Error;
                result.Message = e.Message;
                result.StackTrace = e.ToString();
            } finally {
                stopwatch.Stop();
            }

            result.ElapsedTime = stopwatch.ElapsedMilliseconds;

            return result;
        }
    }
}