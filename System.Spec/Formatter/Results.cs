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

namespace System.Spec.Formatter
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlRootAttribute("test-results", Namespace="", IsNullable=false)]
    public partial class resultType
    {
        private string nameField;
        private decimal totalField;
        private decimal errorsField;
        private decimal failuresField;
        private decimal inconclusiveField;
        private decimal notrunField;
        private decimal ignoredField;
        private decimal skippedField;
        private decimal invalidField;
        private string dateField;
        private string timeField;
        private environmentType environmentField;
        private cultureinfoType cultureinfoField;
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
        public decimal errors {
            get {
                return this.errorsField;
            }
            set {
                this.errorsField = value;
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
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal inconclusive {
            get {
                return this.inconclusiveField;
            }
            set {
                this.inconclusiveField = value;
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
        public decimal ignored {
            get {
                return this.ignoredField;
            }
            set {
                this.ignoredField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal skipped {
            get {
                return this.skippedField;
            }
            set {
                this.skippedField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal invalid {
            get {
                return this.invalidField;
            }
            set {
                this.invalidField = value;
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
        public environmentType environment {
            get {
                return this.environmentField;
            }
            set {
                this.environmentField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("culture-info")]
        public cultureinfoType cultureinfo {
            get {
                return this.cultureinfoField;
            }
            set {
                this.cultureinfoField = value;
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
    public partial class environmentType
    {
        
        private string nunitversionField;
        private string clrversionField;
        private string osversionField;
        private string platformField;
        private string cwdField;
        private string machinenameField;
        private string userField;
        private string userdomainField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("nunit-version")]
        public string nunitversion {
            get {
                return this.nunitversionField;
            }
            set {
                this.nunitversionField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("clr-version")]
        public string clrversion {
            get {
                return this.clrversionField;
            }
            set {
                this.clrversionField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("os-version")]
        public string osversion {
            get {
                return this.osversionField;
            }
            set {
                this.osversionField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string platform {
            get {
                return this.platformField;
            }
            set {
                this.platformField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string cwd {
            get {
                return this.cwdField;
            }
            set {
                this.cwdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("machine-name")]
        public string machinename {
            get {
                return this.machinenameField;
            }
            set {
                this.machinenameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string user {
            get {
                return this.userField;
            }
            set {
                this.userField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("user-domain")]
        public string userdomain {
            get {
                return this.userdomainField;
            }
            set {
                this.userdomainField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute("culture-infoType")]
    public partial class cultureinfoType
    {
        
        private string currentcultureField;
        private string currentuicultureField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("current-culture")]
        public string currentculture {
            get {
                return this.currentcultureField;
            }
            set {
                this.currentcultureField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("current-uiculture")]
        public string currentuiculture {
            get {
                return this.currentuicultureField;
            }
            set {
                this.currentuicultureField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute("test-suiteType")]
    public partial class testsuiteType
    {
        private string typeField;
        private string nameField1;
        private string descriptionField;
        private string successField;
        private string timeField1;
        private string executedField;
        private string assertsField;
        private string resultField;
        private categoryType[] categoriesField;
        private propertyType[] propertiesField;
        private object itemField;
        private object[] resultsField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string type {
            get {
                return this.typeField;
            }
            set {
                this.typeField = value;
            }
        }
        
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
                return this.assertsField;
            }
            set {
                this.assertsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string result {
            get {
                return this.resultField;
            }
            set {
                this.resultField = value;
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
        [System.Xml.Serialization.XmlArrayItem(ElementName="property", IsNullable=false)]
        public propertyType[] properties {
            get {
                return this.propertiesField;
            }
            set {
                this.propertiesField = value;
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
    public partial class categoryType
    {
        
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
    public partial class propertyType
    {
        
        private string nameField3;
        private string valueField;
        
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
        public string value {
            get {
                return this.valueField;
            }
            set {
                this.valueField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class failureType
    {
        private string messageField;
        private string stacktraceField;
        
        /// <remarks/>
        public System.Xml.XmlCDataSection message {
            get {
                var document = new System.Xml.XmlDocument();
                return document.CreateCDataSection(this.messageField);
            }
            set {
                this.messageField = value.Value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("stack-trace")]
        public System.Xml.XmlCDataSection stacktrace {
            get {
                var document = new System.Xml.XmlDocument();
                return document.CreateCDataSection(this.stacktraceField);
            }
            set {
                this.stacktraceField = value.Value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class reasonType
    {
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute("test-caseType")]
    public partial class testcaseType
    {
        
        private string nameField4;
        private string descriptionField1;
        private string successField1;
        private string timeField2;
        private string executedField1;
        private string assertsField1;
        private string resultField1;
        private categoryType[] categoriesField1;
        private propertyType[] propertiesField1;
        private object itemField1;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name {
            get {
                return this.nameField4;
            }
            set {
                this.nameField4 = value;
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
                return this.executedField1;
            }
            set {
                this.executedField1 = value;
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
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string result {
            get {
                return this.resultField1;
            }
            set {
                this.resultField1 = value;
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
        [System.Xml.Serialization.XmlArrayItem(ElementName="property", IsNullable=false)]
        public propertyType[] properties {
            get {
                return this.propertiesField1;
            }
            set {
                this.propertiesField1 = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("failure", Type=typeof(failureType))]
        [System.Xml.Serialization.XmlElementAttribute("reason", Type=typeof(reasonType))]
        public object Item {
            get {
                return this.itemField1;
            }
            set {
                this.itemField1 = value;
            }
        }
    }
}
