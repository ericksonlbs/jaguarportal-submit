using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet_jaguarportal.Jaguar2.Models
{

    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false, ElementName ="sbfl")]
    public partial class Jaguar2Model
    {

        private sbflClass[] classField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("class")]
        public sbflClass[] Classes
        {
            get
            {
                return this.classField;
            }
            set
            {
                this.classField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class sbflClass
    {

        private sbflClassLine[] lineField;

        private string nameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("line")]
        public sbflClassLine[] Lines
        {
            get
            {
                return this.lineField;
            }
            set
            {
                this.lineField = value;
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
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class sbflClassLine
    {

        private int nrField;

        private int cefField;

        private int cnfField;

        private int cepField;

        private int cnpField;

        private decimal suspField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int nr
        {
            get
            {
                return this.nrField;
            }
            set
            {
                this.nrField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int cef
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
        public int cnf
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
        public int cep
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
        public int cnp
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
        [System.Xml.Serialization.XmlAttributeAttribute("susp")]
        public decimal SuspiciousnessValue
        {
            get
            {
                return this.suspField;
            }
            set
            {
                this.suspField = value;
            }
        }
    }


}
