namespace US.WordProcessor.Internal
{
    internal abstract class CorrectionRule : ICorrectionRule
    {
        protected abstract CorrectionType CorrectionType { get; }

        protected Correction CreateCorrection(IDefinitionState definitionState) => new Correction(CorrectionType, definitionState.SourceString, definitionState.CurrentWord);

        public abstract Correction CheckForCorrection(IDefinitionState definitionState);
    }
}