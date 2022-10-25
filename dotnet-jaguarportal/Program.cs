﻿using CommandLine;
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
                          if (File.Exists(o.SBFLPathResult))
                              Console.WriteLine(File.ReadAllText(o.SBFLPathResult));
                          else
                              Console.WriteLine($"File not found: {o.SBFLPathResult}");
                      }

                      Console.WriteLine($"End project: '{o.ProjectName}'");
                  });
        }
    }
}
