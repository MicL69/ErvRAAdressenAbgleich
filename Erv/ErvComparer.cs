using Erv.Utils;
using Erv.Validation;
using ramicro7.stm;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Erv
{
    internal class ErvComparer
    {
        public static void CompareAddressErvNumbersToXJusticeStandard() {
            Console.WriteLine($"Befehlszeile: {Environment.CommandLine}");

            if (CommandLineParser.CommandLineArgs.ContainsKey("?")
                || CommandLineParser.CommandLineArgs.ContainsKey("h")
                || CommandLineParser.CommandLineArgs.ContainsKey("help")) {
                PrintHelp();
                return;
            }

            Console.WriteLine($"{Environment.NewLine}Die Stammdaten-Adressen werden eingelesen ...");
            var adrFunc = new AdressenClass();
            var addresses = adrFunc.GetAdressen(AdressTyp.Gerichtsort).Where(n => n.Notiz.IndexOf("ERV-Gerichtskennzahl", StringComparison.OrdinalIgnoreCase) > -1).Select(a => a).ToList();
            Console.WriteLine($"{Environment.NewLine}Adressen mit ERV-Gerichtskennzahl: {addresses.Count}{Environment.NewLine}");

            var validator = XJusticeValidatorFactory.Build();
            addresses.ToList().ForEach(adr => {
                var check = GetErvCourtIndicator(adr);
                if (validator.IndicatorExists(check.Indicator)) return;

                var foregroundColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"ERV-Gerichtskennzahl '{check.Indicator}' (Adr.nr. {adr.Nr}, {adr.Nam}) existiert im XJustiz-Standard {Globals.CurrentXJusticeVersion} nicht.");
                Console.ForegroundColor = foregroundColor;
            });
        }

        private static void PrintHelp() {
            Console.WriteLine($"{Environment.NewLine}Das Programm kann mit den folgenden Parametern gestartet werden:");
            Console.WriteLine(
                "/?, /h oder /help - Anzeige dieser Hilfe");
            Console.WriteLine(
                "/f:<Pfad zu einer aktuellen GDS.Gerichte_x.x.json> - Es wird die übergebene JSON-Datei zum Abgleich verwendet.");
            Console.WriteLine(
                "/v:3.4.1, /v:341, /v:3.3.1 oder /v:331 - Es werden die Gerichtslisten aus der ramicro.common.xjustiz.xxx.dll verwendet.");
            Console.WriteLine(
                $"Wird keiner dieser Parameter übergeben, so wird die interne Liste GDS.Gerichte_3.6.json verwendet.{Environment.NewLine}");
        }

        private static (string Indicator, string Prefix, string Suffix) GetErvCourtIndicator(Adresse adr) {
            var indicator = string.Empty;
            var ervPattern = @"^ERV-Gerichtskennzahl:(?<prefix>\s*?)(?<erv>\d*[A-Z]\d\d\d\d[A-Z]?)(?<suffix>.*$)";
            var regexAktennr = new Regex(ervPattern, RegexOptions.Multiline);
            var match = regexAktennr.Match(adr.Notiz);
            var prefix = match.Groups["prefix"].Success ? match.Groups["prefix"].Value : string.Empty;
            var suffix = match.Groups["suffix"].Success ? match.Groups["suffix"].Value : string.Empty;
            switch (match.Success) {
                case false:
                    var foregroundColor = Console.ForegroundColor;
                    Console.WriteLine($"ERV-Gerichtskennzahl zur Adressnr. '{adr.Nr}' ist aus der Stammdaten-Notiz nicht extrahierbar:");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"'{adr.Notiz}'");
                    Console.ForegroundColor = foregroundColor;
                    Console.WriteLine();
                    break;
                default:
                    indicator = match.Groups["erv"].Success ? match.Groups["erv"].Value : string.Empty;
                    break;
            }
            return (indicator, prefix, suffix);
        }
    }
}
