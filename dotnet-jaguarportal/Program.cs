using CommandLine;
using dotnet_jaguarportal;
using System.Text.Json;

namespace dotnetJaguarPortal
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var parserResult = Parser.Default.ParseArguments<Options>(args)
                  .WithParsed(o =>
                  {
                      Console.WriteLine($"Start project: '{o.ProjectName}'");

                      Console.WriteLine($"Parameters: {JsonSerializer.Serialize(o)}");

                      if (o.SBFLPathResult != null)
                      {
                          string file = Path.Combine(o.SBFLPathResult, "control-flow.xml");
                          if (File.Exists(file))
                              Console.WriteLine(File.ReadAllText(file));
                          else
                              Console.WriteLine($"File not found: {file}");
                      }

                      Console.WriteLine($"End project: '{o.ProjectName}'");
                  });
        }
    }
}
