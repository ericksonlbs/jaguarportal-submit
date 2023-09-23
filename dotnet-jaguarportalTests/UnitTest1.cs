using CommandLine;
using dotnet_jaguarportal;
using dotnet_jaguarportal.Interfaces;
using dotnet_jaguarportal.Jaguar2.Models;
using dotnet_jaguarportal.Jaguar2.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace dotnet_jaguarportalTests
{
    [TestClass]
    public class Jaguar2ConverterTest
    {
        private IJaguarPortalConverter<Jaguar2Model>? _jaguar2Converter;

        public Jaguar2ConverterTest()
        {
            var services = new ServiceCollection();
            services.AddTransient<IJaguarPortalConverter<Jaguar2Model>, Jaguar2Converter>();
            services.AddTransient<CommandLineParameters>(x =>
            {
                return new CommandLineParameters()
                {

                };
            });
            // configure logging
            services.AddLogging(builder =>
            {
                builder.AddConsole();
            });

            var provider = services.BuildServiceProvider();
            _jaguar2Converter = provider?.GetService<IJaguarPortalConverter<Jaguar2Model>>();
        }

        [TestInitialize]
        public void Initialize()
        {

        }

        [TestMethod]
        public void Test_Convert()
        {
            Tuple<AnalysisControlFlowModel, IEnumerable<ClassAnalysisModel>> expected =
                new(
                    new AnalysisControlFlowModel()
                    {
                        ProjectKey = "123456",
                    },
                    new List<ClassAnalysisModel>()
                    {
                        new ClassAnalysisModel()
                        {
                            FullName = "Test",
                            Lines = new [] {}
                        }
                    }
                );

            var actual = _jaguar2Converter?.Convert(new Jaguar2Model()
            {
                package = new List<reportPackage>() {
                new reportPackage()
                {
                    name = "Test",
                    sourcefile = new List<reportPackageSourcefile>()
                    {
                        new reportPackageSourcefile()
                        {
                            name = "myclass",
                            line = new List<reportPackageSourcefileLine>()
                            {
                                new reportPackageSourcefileLine()
                                {
                                    cef = 1,
                                    cep = 1,
                                    cnf = 1,
                                    cnp = 1,
                                    nr = 1,
                                    susp = 1
                                },
                                new reportPackageSourcefileLine()
                                {
                                    cef = 1,
                                    cep = 1,
                                    cnf = 1,
                                    cnp = 1,
                                    nr = 2,
                                    susp = new decimal(0.5)
                                }
                            }.ToArray(),
                        }
                    }.ToArray()
                }
            }.ToArray()
            }, "123456");

            Assert.IsNotNull(actual);

            string jsonExpected = JsonConvert.SerializeObject(expected);
            string jsonActual = JsonConvert.SerializeObject(actual);
            Assert.AreEqual(jsonExpected, jsonActual);
        }
    }
}