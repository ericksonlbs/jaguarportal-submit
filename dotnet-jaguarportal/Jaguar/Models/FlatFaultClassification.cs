using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet_jaguarportal.Jaguar.Models
{
    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public class FlatFaultClassification
    {

        private FlatFaultClassificationRequirements[] requirementsField;

        private string heuristicField;

        private string projectField;

        private string requirementTypeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("requirements")]
        public FlatFaultClassificationRequirements[] requirements
        {
            get
            {
                return this.requirementsField;
            }
            set
            {
                this.requirementsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string heuristic
        {
            get
            {
                return this.heuristicField;
            }
            set
            {
                this.heuristicField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string project
        {
            get
            {
                return this.projectField;
            }
            set
            {
                this.projectField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string requirementType
        {
            get
            {
                return this.requirementTypeField;
            }
            set
            {
                this.requirementTypeField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class FlatFaultClassificationRequirements
    {

        private byte locationField;

        private byte location1Field;

        private string typeField;

        private byte cefField;

        private byte cepField;

        private byte cnfField;

        private byte cnpField;

        private string nameField;

        private int numberField;

        private decimal suspiciousvalueField;

        /// <remarks/>
        public byte location
        {
            get
            {
                return this.locationField;
            }
            set
            {
                this.locationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("location")]
        public byte location1
        {
            get
            {
                return this.location1Field;
            }
            set
            {
                this.location1Field = value;
            }
        }


        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte cef
        {
            get
            {
                return this.cefField;
            }
            set
            {
                this.cefField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte cep
        {
            get
            {
                return this.cepField;
            }
            set
            {
                this.cepField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte cnf
        {
            get
            {
                return this.cnfField;
            }
            set
            {
                this.cnfField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte cnp
        {
            get
            {
                return this.cnpField;
            }
            set
            {
                this.cnpField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int number
        {
            get
            {
                return this.numberField;
            }
            set
            {
                this.numberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("suspicious-value")]
        public decimal suspiciousvalue
        {
            get
            {
                return this.suspiciousvalueField;
            }
            set
            {
                this.suspiciousvalueField = value;
            }
        }
    }


}
