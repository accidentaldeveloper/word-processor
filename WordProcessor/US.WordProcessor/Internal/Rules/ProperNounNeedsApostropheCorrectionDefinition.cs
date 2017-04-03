using System;

namespace US.WordProcessor.Internal.Rules
{
    class ProperNounNeedsApostropheCorrectionDefinition : ICorrectionDefinition
    {
        public CorrectionType CorrectionType => CorrectionType.OwnershipByAProperNoun;
        public bool WordRequiresCorrection(IDefinitionState definitionState)
        {
            var current = definitionState.CurrentDefinition;
            var isProperNounSuffixedByS = current.Type == WordType.ProperNoun &&
                                          current.Suffix.Equals("s", StringComparison.CurrentCultureIgnoreCase);
            if (isProperNounSuffixedByS)
            {
                var nextWordIsNoun = definitionState.NextDefinition.Type == WordType.Noun;
                var previousWordIsIs = definitionState.PreviousWord.Equals("is",
                    StringComparison.CurrentCultureIgnoreCase);
                if (nextWordIsNoun || previousWordIsIs)
                {
                    var currentWord = definitionState.CurrentWord;
                    var nextToLastCharacter = currentWord.Substring(currentWord.Length - 2, 1);
                    if (nextToLastCharacter != "'")
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}