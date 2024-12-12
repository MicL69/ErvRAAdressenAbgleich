namespace Erv.Validation
{
    internal class XJusticeValidator331 : BaseXJusticeValidator
    {
        public override string Description => "Prüfung des Datenbestands gegen die Ramicro.Common.XJustiz.3.3.1.dll";

        public override bool IndicatorExists(string indicator) {
            return !string.IsNullOrEmpty(indicator) && Ramicro.Common.XJustiz.Version_3_3_1.Listen.GDS_Gerichte.ContainsKey(indicator);
        }
    }
}