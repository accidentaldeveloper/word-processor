using System;
using System.Collections.Generic;
using System.Linq;
using US.WordProcessor.Internal;
using US.WordProcessor.Internal.Rules;

namespace US.WordProcessor
{
    internal class CorrectionFinder
       : ICorrectionFinder
    {
        private readonly WordCorrectionFinder _wordCorrectionFinder;
        private static readonly Dictionary Dictionary = new Dictionary();

        public CorrectionFinder(WordCorrectionFinder wordCorrectionFinder)
        {
            if (wordCorrectionFinder == null) throw new ArgumentNullException(nameof(wordCorrectionFinder));
            _wordCorrectionFinder = wordCorrectionFinder;
        }

        public IEnumerable<Correction> Find(Paragraph paragraph)
        {
            var corrections = paragraph.SelectMany(CheckSentenceForCorrections);
            return corrections.SelectMany(enumerable => enumerable);
        }

        private IEnumerable<IEnumerable<Correction>> CheckSentenceForCorrections(Sentence sentence)
        {
            var definitionReader = CreateDefinitionReader(sentence);
            while (definitionReader.MoveNext())
            {
                yield return _wordCorrectionFinder.GetWordCorrections(definitionReader);
                //var correction = CheckIfProperNounNeedsApostrophe(definitionReader);
                //yield return correction;

                //var contractionCorrection = CheckIfContractionNeedsApostrophe(definitionReader);
                //yield return contractionCorrection;

                //var normalNounCorrection = CheckIfNormalNounHasApostrophe(definitionReader);
                //yield return normalNounCorrection;
            }
        }

        private static Correction CheckIfNormalNounHasApostrophe(IDefinitionState definitionState)
        {
            var current = definitionState.CurrentDefinition;
            var isRegularNounWithoutApostrophe = current.Type == WordType.Noun &&
                                     !(current.Word + current.Suffix).Equals(definitionState.CurrentWord, StringComparison.CurrentCultureIgnoreCase);
            if (isRegularNounWithoutApostrophe)
            {
                return new Correction(CorrectionType.IncorrectNounApostrophe, definitionState.SourceString, definitionState.CurrentWord);
            }

            return null;
        }

        private static Correction CheckIfContractionNeedsApostrophe(IDefinitionState definitionState)
        {
            var current = definitionState.CurrentDefinition;
            var isContractionWithoutApostrophe = current.Type == WordType.Contraction &&
                                                 !current.Word.Equals(definitionState.CurrentWord, StringComparison.CurrentCultureIgnoreCase);
            if (isContractionWithoutApostrophe)
            {
                return new Correction(CorrectionType.MissingContractionApostrophe, definitionState.SourceString, definitionState.CurrentWord);
            }

            return null;
        }


        private static Correction CheckIfProperNounNeedsApostrophe(IDefinitionState definitionState)
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
                        return new Correction(CorrectionType.OwnershipByAProperNoun, definitionState.SourceString, currentWord);
                    }

                }
            }

            return null;
        }

        private static DefinitionReader CreateDefinitionReader(Sentence sentence)
        {
            return new DefinitionReader(Dictionary, new SentenceReader(sentence));
        }
    }
}
