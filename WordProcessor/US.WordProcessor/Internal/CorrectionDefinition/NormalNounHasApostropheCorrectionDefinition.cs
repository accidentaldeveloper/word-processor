using System;

namespace US.WordProcessor.Internal.CorrectionDefinition
{
    internal class NormalNounHasApostropheCorrectionDefinition : ICorrectionDefinition
    {
        public CorrectionType CorrectionType => CorrectionType.IncorrectNounApostrophe;

        public bool WordRequiresCorrection(IDefinitionState definitionState)
        {
            var current = definitionState.CurrentDefinition;
            var isRegularNounWithoutApostrophe = current.Type == WordType.Noun &&
                                     !(current.Word + current.Suffix).Equals(definitionState.CurrentWord, StringComparison.CurrentCultureIgnoreCase);
            return isRegularNounWithoutApostrophe;
        }
    }
}