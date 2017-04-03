using System;
using US.WordProcessor.Internal.Rules;

namespace US.WordProcessor.Internal
{
    class NormalNounHasApostropheCorrectionDefinition : ICorrectionDefinition
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