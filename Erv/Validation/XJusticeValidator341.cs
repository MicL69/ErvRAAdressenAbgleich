namespace Erv.Validation
{
    internal class XJusticeValidator341 : BaseXJusticeValidator
    {
        public override string Description => "Prüfung des Datenbestands gegen die Ramicro.Common.XJustiz.3.4.1.dll";

        public override bool IndicatorExists(string indicator) {
            return !string.IsNullOrEmpty(indicator) && Ramicro.Common.XJustiz.Version_3_4_1.Listen.GDS_Gerichte.ContainsKey(indicator);
        }
    }
}
