namespace System.Spec
{
    using System;
    using System.Globalization;

	public class ExampleResult : IEquatable<ExampleResult>
	{
		public string Reason { get; set; }

		public ExampleResultStatus Status { get; set; }

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

        public override string ToString()
        {
            return string.Format(CultureInfo.CurrentCulture, 
                                 "[ExampleResult: Reason={0}, Status={1}, Exception={2}, ElapsedTime={3}]", 
                                 Reason, Status, Exception, ElapsedTime);
        }
	}
}