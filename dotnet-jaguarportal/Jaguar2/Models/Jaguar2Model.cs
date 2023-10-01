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
    [System.Xml.Serialization.XmlRootAttribute(ElementName = "report", Namespace = "", IsNullable = false)]
    public partial class Jaguar2Model
    {

        private reportPackage[]? packageField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("package")]
        public reportPackage[]? package
        {
            get
            {
                return this.packageField;
            }
            set
            {
                this.packageField = value;
            }
        }

        private reportTests? testsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("tests")]
        public reportTests? tests
        {
            get
            {
                return this.testsField;
            }
            set
            {
                this.testsField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class reportPackage
    {

        private reportPackageSourcefile[]? sourcefileField;

        private string? nameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("sourcefile")]
        public reportPackageSourcefile[]? sourcefile
        {
            get
            {
                return this.sourcefileField;
            }
            set
            {
                this.sourcefileField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string? name
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
    public partial class reportPackageSourcefile
    {

        private reportPackageSourcefileLine[]? lineField;

        private string? nameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("line")]
        public reportPackageSourcefileLine[]? line
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
        public string? name
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
    public partial class reportPackageSourcefileLine
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
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal susp
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



    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class reportTests
    {

        private int failField;

        private int passField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int fail
        {
            get
            {
                return this.failField;
            }
            set
            {
                this.failField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int pass
        {
            get
            {
                return this.passField;
            }
            set
            {
                this.passField = value;
            }
        }
    }


}
