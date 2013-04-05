//  Author:
//       alexfalkowski <alexrfalkowski@gmail.com>
//
//  Copyright (c) 2013 alexfalkowski
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

namespace System.Spec.Reports
{
    using CodeDom.Compiler;
    using ComponentModel;
    using Diagnostics;
    using Xml;
    using Xml.Serialization;

    /// <remarks />
    [GeneratedCode("System.Xml", "4.0.30319.1")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlRoot("test-results", Namespace = "", IsNullable = false)]
    public class resultType
    {
        private cultureinfoType cultureinfoField;
        private string dateField;
        private environmentType environmentField;
        private decimal errorsField;
        private decimal failuresField;
        private decimal ignoredField;
        private decimal inconclusiveField;
        private decimal invalidField;
        private string nameField;
        private decimal notrunField;
        private decimal skippedField;
        private testsuiteType testsuiteField;
        private string timeField;
        private decimal totalField;

        /// <remarks />
        [XmlAttribute]
        public string name
        {
            get { return nameField; }
            set { nameField = value; }
        }

        /// <remarks />
        [XmlAttribute]
        public decimal total
        {
            get { return totalField; }
            set { totalField = value; }
        }

        /// <remarks />
        [XmlAttribute]
        public decimal errors
        {
            get { return errorsField; }
            set { errorsField = value; }
        }

        /// <remarks />
        [XmlAttribute]
        public decimal failures
        {
            get { return failuresField; }
            set { failuresField = value; }
        }

        /// <remarks />
        [XmlAttribute]
        public decimal inconclusive
        {
            get { return inconclusiveField; }
            set { inconclusiveField = value; }
        }

        /// <remarks />
        [XmlAttribute("not-run")]
        public decimal notrun
        {
            get { return notrunField; }
            set { notrunField = value; }
        }

        /// <remarks />
        [XmlAttribute]
        public decimal ignored
        {
            get { return ignoredField; }
            set { ignoredField = value; }
        }

        /// <remarks />
        [XmlAttribute]
        public decimal skipped
        {
            get { return skippedField; }
            set { skippedField = value; }
        }

        /// <remarks />
        [XmlAttribute]
        public decimal invalid
        {
            get { return invalidField; }
            set { invalidField = value; }
        }

        /// <remarks />
        [XmlAttribute]
        public string date
        {
            get { return dateField; }
            set { dateField = value; }
        }

        /// <remarks />
        [XmlAttribute]
        public string time
        {
            get { return timeField; }
            set { timeField = value; }
        }

        /// <remarks />
        public environmentType environment
        {
            get { return environmentField; }
            set { environmentField = value; }
        }

        /// <remarks />
        [XmlElement("culture-info")]
        public cultureinfoType cultureinfo
        {
            get { return cultureinfoField; }
            set { cultureinfoField = value; }
        }

        /// <remarks />
        [XmlElement("test-suite")]
        public testsuiteType testsuite
        {
            get { return testsuiteField; }
            set { testsuiteField = value; }
        }
    }

    /// <remarks />
    [GeneratedCode("System.Xml", "4.0.30319.1")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    public class environmentType
    {
        private string clrversionField;
        private string cwdField;
        private string machinenameField;
        private string nunitversionField;
        private string osversionField;
        private string platformField;
        private string userField;
        private string userdomainField;

        /// <remarks />
        [XmlAttribute("nunit-version")]
        public string nunitversion
        {
            get { return nunitversionField; }
            set { nunitversionField = value; }
        }

        /// <remarks />
        [XmlAttribute("clr-version")]
        public string clrversion
        {
            get { return clrversionField; }
            set { clrversionField = value; }
        }

        /// <remarks />
        [XmlAttribute("os-version")]
        public string osversion
        {
            get { return osversionField; }
            set { osversionField = value; }
        }

        /// <remarks />
        [XmlAttribute]
        public string platform
        {
            get { return platformField; }
            set { platformField = value; }
        }

        /// <remarks />
        [XmlAttribute]
        public string cwd
        {
            get { return cwdField; }
            set { cwdField = value; }
        }

        /// <remarks />
        [XmlAttribute("machine-name")]
        public string machinename
        {
            get { return machinenameField; }
            set { machinenameField = value; }
        }

        /// <remarks />
        [XmlAttribute]
        public string user
        {
            get { return userField; }
            set { userField = value; }
        }

        /// <remarks />
        [XmlAttribute("user-domain")]
        public string userdomain
        {
            get { return userdomainField; }
            set { userdomainField = value; }
        }
    }

    /// <remarks />
    [GeneratedCode("System.Xml", "4.0.30319.1")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType("culture-infoType")]
    public class cultureinfoType
    {
        private string currentcultureField;
        private string currentuicultureField;

        /// <remarks />
        [XmlAttribute("current-culture")]
        public string currentculture
        {
            get { return currentcultureField; }
            set { currentcultureField = value; }
        }

        /// <remarks />
        [XmlAttribute("current-uiculture")]
        public string currentuiculture
        {
            get { return currentuicultureField; }
            set { currentuicultureField = value; }
        }
    }

    /// <remarks />
    [GeneratedCode("System.Xml", "4.0.30319.1")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType("test-suiteType")]
    public class testsuiteType
    {
        private string assertsField;
        private categoryType[] categoriesField;
        private string descriptionField;
        private string executedField;
        private object itemField;
        private string nameField1;
        private propertyType[] propertiesField;
        private string resultField;
        private object[] resultsField;
        private string successField;
        private string timeField1;
        private string typeField;

        /// <remarks />
        [XmlAttribute]
        public string type
        {
            get { return typeField; }
            set { typeField = value; }
        }

        /// <remarks />
        [XmlAttribute]
        public string name
        {
            get { return nameField1; }
            set { nameField1 = value; }
        }

        /// <remarks />
        [XmlAttribute]
        public string description
        {
            get { return descriptionField; }
            set { descriptionField = value; }
        }

        /// <remarks />
        [XmlAttribute]
        public string success
        {
            get { return successField; }
            set { successField = value; }
        }

        /// <remarks />
        [XmlAttribute]
        public string time
        {
            get { return timeField1; }
            set { timeField1 = value; }
        }

        /// <remarks />
        [XmlAttribute]
        public string executed
        {
            get { return executedField; }
            set { executedField = value; }
        }

        /// <remarks />
        [XmlAttribute]
        public string asserts
        {
            get { return assertsField; }
            set { assertsField = value; }
        }

        /// <remarks />
        [XmlAttribute]
        public string result
        {
            get { return resultField; }
            set { resultField = value; }
        }

        /// <remarks />
        [XmlArrayItem(ElementName = "category", IsNullable = false)]
        public categoryType[] categories
        {
            get { return categoriesField; }
            set { categoriesField = value; }
        }

        /// <remarks />
        [XmlArrayItem(ElementName = "property", IsNullable = false)]
        public propertyType[] properties
        {
            get { return propertiesField; }
            set { propertiesField = value; }
        }

        /// <remarks />
        [XmlElement("failure", Type = typeof (failureType))]
        [XmlElement("reason", Type = typeof (reasonType))]
        public object Item
        {
            get { return itemField; }
            set { itemField = value; }
        }

        /// <remarks />
        [XmlArrayItem(ElementName = "test-suite", Type = typeof (testsuiteType), IsNullable = false)]
        [XmlArrayItem(ElementName = "test-case", Type = typeof (testcaseType), IsNullable = false)]
        public object[] results
        {
            get { return resultsField; }
            set { resultsField = value; }
        }
    }

    /// <remarks />
    [GeneratedCode("System.Xml", "4.0.30319.1")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    public class categoryType
    {
        private string nameField2;

        /// <remarks />
        [XmlAttribute]
        public string name
        {
            get { return nameField2; }
            set { nameField2 = value; }
        }
    }

    /// <remarks />
    [GeneratedCode("System.Xml", "4.0.30319.1")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    public class propertyType
    {
        private string nameField3;
        private string valueField;

        /// <remarks />
        [XmlAttribute]
        public string name
        {
            get { return nameField3; }
            set { nameField3 = value; }
        }

        /// <remarks />
        [XmlAttribute]
        public string value
        {
            get { return valueField; }
            set { valueField = value; }
        }
    }

    /// <remarks />
    [GeneratedCode("System.Xml", "4.0.30319.1")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    public class failureType
    {
        private string messageField;
        private string stacktraceField;

        /// <remarks />
        public XmlCDataSection message
        {
            get
            {
                var document = new XmlDocument();
                return document.CreateCDataSection(messageField);
            }
            set { messageField = value.Value; }
        }

        /// <remarks />
        [XmlElement("stack-trace")]
        public XmlCDataSection stacktrace
        {
            get
            {
                var document = new XmlDocument();
                return document.CreateCDataSection(stacktraceField);
            }
            set { stacktraceField = value.Value; }
        }
    }

    /// <remarks />
    [GeneratedCode("System.Xml", "4.0.30319.1")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    public class reasonType
    {
        private string messageField1;

        /// <remarks />
        public string message
        {
            get { return messageField1; }
            set { messageField1 = value; }
        }
    }

    /// <remarks />
    [GeneratedCode("System.Xml", "4.0.30319.1")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType("test-caseType")]
    public class testcaseType
    {
        private string assertsField1;
        private categoryType[] categoriesField1;
        private string descriptionField1;
        private string executedField1;
        private object itemField1;
        private string nameField4;
        private propertyType[] propertiesField1;
        private string resultField1;
        private string successField1;
        private string timeField2;

        /// <remarks />
        [XmlAttribute]
        public string name
        {
            get { return nameField4; }
            set { nameField4 = value; }
        }

        /// <remarks />
        [XmlAttribute]
        public string description
        {
            get { return descriptionField1; }
            set { descriptionField1 = value; }
        }

        /// <remarks />
        [XmlAttribute]
        public string success
        {
            get { return successField1; }
            set { successField1 = value; }
        }

        /// <remarks />
        [XmlAttribute]
        public string time
        {
            get { return timeField2; }
            set { timeField2 = value; }
        }

        /// <remarks />
        [XmlAttribute]
        public string executed
        {
            get { return executedField1; }
            set { executedField1 = value; }
        }

        /// <remarks />
        [XmlAttribute]
        public string asserts
        {
            get { return assertsField1; }
            set { assertsField1 = value; }
        }

        /// <remarks />
        [XmlAttribute]
        public string result
        {
            get { return resultField1; }
            set { resultField1 = value; }
        }

        /// <remarks />
        [XmlArrayItem(ElementName = "category", IsNullable = false)]
        public categoryType[] categories
        {
            get { return categoriesField1; }
            set { categoriesField1 = value; }
        }

        /// <remarks />
        [XmlArrayItem(ElementName = "property", IsNullable = false)]
        public propertyType[] properties
        {
            get { return propertiesField1; }
            set { propertiesField1 = value; }
        }

        /// <remarks />
        [XmlElement("failure", Type = typeof (failureType))]
        [XmlElement("reason", Type = typeof (reasonType))]
        public object Item
        {
            get { return itemField1; }
            set { itemField1 = value; }
        }
    }
}