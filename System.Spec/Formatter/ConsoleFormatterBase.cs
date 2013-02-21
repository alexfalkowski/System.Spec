namespace System.Spec.Formatter
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.IO;
    using System.Xml.Serialization;

    using NUnit.Framework;

    using System.Spec.Properties;

    public abstract class ConsoleFormatterBase : IConsoleFormatter
    {
        private readonly IList<ExampleResult> errorResults = new Collection<ExampleResult>();
        private readonly IList<ExampleResult> successResults = new Collection<ExampleResult>();

        protected IList<ExampleResult> ErrorResults {
            get {
                return this.errorResults;
            }
        }

        protected IList<ExampleResult> SuccessResults {
            get {
                return this.successResults;
            }
        }

        public abstract void WriteInformation(string message);

        public virtual void WriteSuccess(string reason, ExampleResult example)
        {
            this.SuccessResults.Add(example);
        }

        public virtual void WriteError(string reason, ExampleResult example)
        {
            this.ErrorResults.Add(example);
        }

        public virtual int WriteSummary(long elapsedMilliseconds)
        {
            var elapsdeTimeMessage = string.Format(
                CultureInfo.CurrentCulture, Resources.ConsoleFormatterElapsedTimeMessage, elapsedMilliseconds / 1000D);
            Console.WriteLine(elapsdeTimeMessage);

            var errorCount = this.ErrorResults.Count;
            var summaryMessage = string.Format(
                CultureInfo.CurrentCulture,
                Resources.ConsoleFormatterSummaryMessage,
                this.SuccessResults.Count + errorCount,
                errorCount);
            Console.WriteLine(summaryMessage);

            return errorCount;
        }

        public void WriteSummaryToStream(Stream stream)
        {
            var resultType = new resultType {
                environment = new environmentType {
                    nunitversion = typeof(TestAttribute).Assembly.GetName().Version.ToString(),
                    clrversion = Environment.Version.ToString(),
                    osversion = Environment.OSVersion.VersionString,
                    machinename = Environment.MachineName,
                    platform = Enum.GetName(typeof(PlatformID), Environment.OSVersion.Platform),
                    user = Environment.UserName,
                    userdomain = Environment.UserDomainName
                } 
            };

            var serializer = new XmlSerializer(typeof(resultType));
            serializer.Serialize(stream, resultType);
        }
    }
}