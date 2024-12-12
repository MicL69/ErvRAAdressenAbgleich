using System;
using Erv.Utils;

namespace Erv.Validation
{
    internal static class XJusticeValidatorFactory
    {
        public static BaseXJusticeValidator Build() {
            BaseXJusticeValidator validator;

            if (CommandLineParser.CommandLineArgs.ContainsKey("f")) {
                validator = new JsonValidator36();

            } else if (CommandLineParser.CommandLineArgs.TryGetValue("v", out var version)) {
                validator = version switch {
                    "3.4.1" => new XJusticeValidator341(),
                    "341" => new XJusticeValidator341(),
                    "3.3.1" => new XJusticeValidator331(),
                    "331" => new XJusticeValidator331(),
                    _ => new JsonValidator36()
                };

            } else {
                validator = new JsonValidator36();
            }   

            Console.WriteLine($"{validator.Description} ...{Environment.NewLine}");
            return validator;
        }
    }
}
