using Erv.Utils;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Erv.Validation
{
    internal class JsonValidator36 : BaseXJusticeValidator
    {
        private readonly XJusticeJson _xJustice;
        private const string JsonFileName = "GDS.Gerichte_3.6.json";

        public override string Description => $"Prüfung des Datenbestands gegen die interne {JsonFileName}";

        public JsonValidator36() {
            var jsonPath = CommandLineParser.CommandLineArgs.ContainsKey("f") ? CommandLineParser.CommandLineArgs["f"] : string.Empty;
            var tempPath = Path.Combine(Path.GetTempPath(), JsonFileName);

            if (string.IsNullOrEmpty(jsonPath)) {
                Console.WriteLine("Es wurde kein Dateiname angegeben. Die interne Ressource wird verwendet.");
            } else if (!File.Exists(jsonPath)) {
                Console.WriteLine($"Die Datei '{jsonPath}' existiert nicht. Die interne Ressource wird verwendet.");
            } else {
                File.Copy(jsonPath, tempPath, true);
                System.Threading.Thread.Sleep(500);
            }

            if (!File.Exists(tempPath)) {
                var resourceHelper = new ResourceHelper();
                resourceHelper.EnsureFileExists(tempPath);
            }

            // Deserialization
            var jsonString = File.ReadAllText(tempPath, Encoding.UTF8);
            _xJustice = JsonConvert.DeserializeObject<XJusticeJson>(jsonString);
        }

        public override bool IndicatorExists(string indicator) {
            return _xJustice.Data.Any(d => d.Key == indicator);
        }
    }
}
