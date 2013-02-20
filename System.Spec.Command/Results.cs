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

namespace System.Spec.Console {
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlRootAttribute("test-results", Namespace="", IsNullable=false)]
    public partial class resultType {
        
        private string nameField;
        
        private decimal totalField;
        
        private decimal failuresField;
        
        private decimal notrunField;
        
        private string dateField;
        
        private string timeField;
        
        private testsuiteType testsuiteField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal total {
            get {
                return this.totalField;
            }
            set {
                this.totalField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal failures {
            get {
                return this.failuresField;
            }
            set {
                this.failuresField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("not-run")]
        public decimal notrun {
            get {
                return this.notrunField;
            }
            set {
                this.notrunField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string date {
            get {
                return this.dateField;
            }
            set {
                this.dateField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string time {
            get {
                return this.timeField;
            }
            set {
                this.timeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("test-suite")]
        public testsuiteType testsuite {
            get {
                return this.testsuiteField;
            }
            set {
                this.testsuiteField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute("test-suiteType")]
    public partial class testsuiteType {
        
        private string nameField1;
        
        private string descriptionField;
        
        private string successField;
        
        private string timeField1;
        
        private string assertsField;
        
        private categoryType[] categoriesField;
        
        private object[] resultsField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name {
            get {
                return this.nameField1;
            }
            set {
                this.nameField1 = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string description {
            get {
                return this.descriptionField;
            }
            set {
                this.descriptionField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string success {
            get {
                return this.successField;
            }
            set {
                this.successField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string time {
            get {
                return this.timeField1;
            }
            set {
                this.timeField1 = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string asserts {
            get {
                return this.assertsField;
            }
            set {
                this.assertsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItem(ElementName="category", IsNullable=false)]
        public categoryType[] categories {
            get {
                return this.categoriesField;
            }
            set {
                this.categoriesField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItem(ElementName="test-suite", Type=typeof(testsuiteType), IsNullable=false)]
        [System.Xml.Serialization.XmlArrayItem(ElementName="test-case", Type=typeof(testcaseType), IsNullable=false)]
        public object[] results {
            get {
                return this.resultsField;
            }
            set {
                this.resultsField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class categoryType {
        
        private string nameField2;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name {
            get {
                return this.nameField2;
            }
            set {
                this.nameField2 = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute("test-caseType")]
    public partial class testcaseType {
        
        private string nameField3;
        
        private string descriptionField1;
        
        private string successField1;
        
        private string timeField2;
        
        private string executedField;
        
        private string assertsField1;
        
        private categoryType[] categoriesField1;
        
        private object itemField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name {
            get {
                return this.nameField3;
            }
            set {
                this.nameField3 = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string description {
            get {
                return this.descriptionField1;
            }
            set {
                this.descriptionField1 = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string success {
            get {
                return this.successField1;
            }
            set {
                this.successField1 = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string time {
            get {
                return this.timeField2;
            }
            set {
                this.timeField2 = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string executed {
            get {
                return this.executedField;
            }
            set {
                this.executedField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string asserts {
            get {
                return this.assertsField1;
            }
            set {
                this.assertsField1 = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItem(ElementName="category", IsNullable=false)]
        public categoryType[] categories {
            get {
                return this.categoriesField1;
            }
            set {
                this.categoriesField1 = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("failure", Type=typeof(failureType))]
        [System.Xml.Serialization.XmlElementAttribute("reason", Type=typeof(reasonType))]
        public object Item {
            get {
                return this.itemField;
            }
            set {
                this.itemField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class failureType {
        
        private string messageField;
        
        private string stacktraceField;
        
        /// <remarks/>
        public string message {
            get {
                return this.messageField;
            }
            set {
                this.messageField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("stack-trace")]
        public string stacktrace {
            get {
                return this.stacktraceField;
            }
            set {
                this.stacktraceField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class reasonType {
        
        private string messageField1;
        
        /// <remarks/>
        public string message {
            get {
                return this.messageField1;
            }
            set {
                this.messageField1 = value;
            }
        }
    }
}
