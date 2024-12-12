using System;

namespace Erv
{
    internal class Program
    {
        static void Main(string[] args) {
            Console.WriteLine("Prüfung der ERV-Gerichtskennzahlen des aktuellen Adress-Datenbestands");
            Console.WriteLine($"---------------------------------------------------------------------{Environment.NewLine}");
            ErvComparer.CompareAddressErvNumbersToXJusticeStandard();
            Console.WriteLine($"{Environment.NewLine}Drücken Sie eine beliebige Taste zum Beenden ...");
            Console.ReadKey();
        }
    }
}
