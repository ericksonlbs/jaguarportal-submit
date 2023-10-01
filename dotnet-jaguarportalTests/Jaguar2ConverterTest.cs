using System.Xml.Serialization;
using System.Xml;
using NuGet.Frameworks;

namespace dotnet_jaguarportalTests
{
    [TestClass]
    public class Jaguar2ConverterTest
    {
        private IJaguarPortalConverter<Jaguar2Model>? converter;

        [TestInitialize]
        public void Initialize()
        {
            ServiceCollection services = new ServiceCollection();

            // Register your dependencies here
            services.AddTransient<IJaguarPortalConverter<Jaguar2Model>, Jaguar2Converter>();
            services.AddTransient<CommandLineParameters>(_ => new CommandLineParameters()
            {
                PathTarget = Path.Combine(Environment.CurrentDirectory, "FileTests", "csv1b")
            });
            services.AddLogging(builder =>
            {
                builder.AddDebug();
            });

            ServiceProvider provider = services.BuildServiceProvider();

            converter = provider?.GetService<IJaguarPortalConverter<Jaguar2Model>>();

            if (converter == null)
                throw new Exception("IJaguarPortalConverter<Jaguar2Model> not initialized");
        }

        [TestMethod("Null model")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullModel()
        {
            Assert.IsNotNull(converter);
            _ = converter.Convert(null, "123456", null, null, null, null, null);
        }

        [TestMethod("Empty project")]
        [ExpectedException(typeof(ArgumentException))]
        public void EmptyProject()
        {
            Assert.IsNotNull(converter);
            _ = converter.Convert(new Jaguar2Model(), string.Empty, null, null, null, null, null);
        }

        [TestMethod("Null project")]
        [ExpectedException(typeof(ArgumentException))]
        public void NullProject()
        {
            Assert.IsNotNull(converter);
            _ = converter.Convert(new Jaguar2Model(), null, null, null, null, null, null);
        }

        /// <summary>
        /// Validate XML Converter to API Format
        /// </summary>
        [TestMethod("Validate XML Converter to API Format")]
        public void ConvertValidate()
        {
            using XmlReader xmlReader = XmlReader.Create(Path.Combine("FileTests", "csv1b", "jaguar2.xml"));

            XmlSerializer serializer = new(typeof(Jaguar2Model));

            Jaguar2Model? jaguar2Model = serializer.Deserialize(xmlReader) as Jaguar2Model;

            Assert.IsNotNull(converter);

            Assert.IsNotNull(jaguar2Model);

            Tuple<AnalysisControlFlowModel, IEnumerable<ClassAnalysisModel>> result = 
                converter.Convert(jaguar2Model, "123456", "ericksonlbs/csv1b", "github", null, null, null);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Item1);

            Assert.AreEqual("123456", result.Item1.ProjectKey, nameof(result.Item1.ProjectKey));
            Assert.AreEqual(53, result.Item1.TestsPass, nameof(result.Item1.TestsPass));
            Assert.AreEqual(1, result.Item1.TestsFail, nameof(result.Item1.TestsFail));


            IEnumerable<ClassAnalysisModel> itens = result.Item2;

            Assert.IsNotNull(itens);

            //source files
            Assert.AreEqual(7, itens.Count(), "Count files");
            Assert.AreEqual("org/apache/commons/csv/CSVFormat.java", itens.Skip(0).First().FullName, "1º name file");
            Assert.AreEqual("org/apache/commons/csv/CSVLexer.java", itens.Skip(1).First().FullName, "2º name file");
            Assert.AreEqual("org/apache/commons/csv/CSVParser.java", itens.Skip(2).First().FullName, "3º name file");
            Assert.AreEqual("org/apache/commons/csv/CSVRecord.java", itens.Skip(3).First().FullName, "4º name file");
            Assert.AreEqual("org/apache/commons/csv/ExtendedBufferedReader.java", itens.Skip(4).First().FullName, "5º name file");
            Assert.AreEqual("org/apache/commons/csv/Lexer.java", itens.Skip(5).First().FullName, "6º name file");
            Assert.AreEqual("org/apache/commons/csv/Token.java", itens.Skip(6).First().FullName, "7º name file");

            Assert.AreEqual(27, itens.Skip(0).First().Lines.Count(), "Number lines suspiciousness in file CSVFormat.java");
            Assert.AreEqual(29, itens.Skip(1).First().Lines.Count(), "Number lines suspiciousness in file CSVLexer.java");
            Assert.AreEqual(24, itens.Skip(2).First().Lines.Count(), "Number lines suspiciousness in file CSVParser.java");
            Assert.AreEqual(4, itens.Skip(3).First().Lines.Count(), "Number lines suspiciousness in file CSVRecord.java");
            Assert.AreEqual(14, itens.Skip(4).First().Lines.Count(), "Number lines suspiciousness in file ExtendedBufferedReader.java");
            Assert.AreEqual(21, itens.Skip(5).First().Lines.Count(), "Number lines suspiciousness in file Lexer.java");
            Assert.AreEqual(7, itens.Skip(6).First().Lines.Count(), "Number lines suspiciousness in file Token.java");

            Assert.AreEqual(142, itens.Skip(0).First().Lines.First().NumberLine, "Number line on first item suspiciousness in file CSVFormat.java");
            Assert.AreEqual(1, itens.Skip(0).First().Lines.First().Cef, "CEF, CSVFormat.java, first item");
            Assert.AreEqual(20, itens.Skip(0).First().Lines.First().Cep, "CEP, CSVFormat.java, first item");
            Assert.AreEqual(result.Item1.TestsFail - itens.Skip(0).First().Lines.First().Cef, itens.Skip(0).First().Lines.First().Cnf, "CNF, CSVFormat.java, first item");
            Assert.AreEqual(result.Item1.TestsPass - itens.Skip(0).First().Lines.First().Cep, itens.Skip(0).First().Lines.First().Cnp, "CNP, CSVFormat.java, first item");
            Assert.AreEqual(0.218218, itens.Skip(0).First().Lines.First().SuspiciousValue, "SuspiciousValue in CSVFormat.java, first item");

            byte[] file = File.ReadAllBytes(Path.Combine(Environment.CurrentDirectory, "FileTests", "csv1b", "org", "apache", "commons", "csv", "CSVFormat.java"));

            Assert.AreEqual(file.LongLength, itens.Skip(0).First().Code.LongLength, "Codefile CSVFormat.java");
        }
    }
}