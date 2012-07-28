namespace NSpec
{
    using System;

    public class ExampleResult : IEquatable<ExampleResult>
    {
        public ExampleResultStatus Status { get; set; }

        public Exception Exception { get; set; }

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
            return this.Status == other.Status;
        }

        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(null, obj))
            {
                return false;
            }

            if (object.ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != typeof(ExampleResult))
            {
                return false;
            }
            return this.Equals((ExampleResult)obj);
        }

        public override int GetHashCode()
        {
            return this.Status.GetHashCode();
        }
    }
}