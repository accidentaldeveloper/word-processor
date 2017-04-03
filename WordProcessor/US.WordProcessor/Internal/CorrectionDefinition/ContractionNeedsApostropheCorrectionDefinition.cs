using System;

namespace US.WordProcessor.Internal.CorrectionDefinition
{
    class ContractionNeedsApostropheCorrectionDefinition : ICorrectionDefinition
    {
        public CorrectionType CorrectionType => CorrectionType.MissingContractionApostrophe;

        public bool WordRequiresCorrection(IDefinitionState definitionState)
        {
            var current = definitionState.CurrentDefinition;
            var isContractionWithoutApostrophe = current.Type == WordType.Contraction &&
                                                 !current.Word.Equals(definitionState.CurrentWord, StringComparison.CurrentCultureIgnoreCase);
            return isContractionWithoutApostrophe;
        }
    }
}