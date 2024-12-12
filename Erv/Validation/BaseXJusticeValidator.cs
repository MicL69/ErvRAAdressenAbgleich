namespace Erv.Validation
{
    internal abstract class BaseXJusticeValidator
    {
        public abstract bool IndicatorExists(string indicator);
        public abstract string Description { get; }
    }
}
