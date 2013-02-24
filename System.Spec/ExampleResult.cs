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
    using System;

	public class ExampleResult : IEquatable<ExampleResult>
	{
		public string Reason { get; set; }

		public ResultStatus Status { get; set; }

		public Exception Exception { get; set; }

        public long ElapsedTime { get; set; }

		public static bool operator ==(ExampleResult left, ExampleResult right)
		{
			return object.Equals(left, right);
		}

		public static bool operator !=(ExampleResult left, ExampleResult right)
		{
			return !object.Equals(left, right);
		}

		public bool Equals(ExampleResult other)
		{
			return other != null && this.Status == other.Status;
		}

		public override bool Equals(object value)
		{
			if (object.ReferenceEquals(null, value)) {
				return false;
			}

			if (object.ReferenceEquals(this, value)) {
				return true;
			}

			if (value.GetType() != typeof(ExampleResult)) {
				return false;
			}

			return this.Equals((ExampleResult)value);
		}

		public override int GetHashCode()
		{
			return this.Status.GetHashCode();
		}
	}
}