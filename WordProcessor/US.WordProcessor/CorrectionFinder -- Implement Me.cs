using System;
using System.Collections.Generic;
using System.Linq;
using US.WordProcessor.Internal;

namespace US.WordProcessor
{
    internal class CorrectionFinder
       : ICorrectionFinder
    {
        private static readonly Dictionary Dictionary = new Dictionary();

        public IEnumerable<Correction> Find(Paragraph paragraph)
        {
            /* NOTE: Please feel feel to add / remove / modify any classes in 
             *       this solution in order to implement the requested functionality
             *       the best way that you see fit.  Remember, design is a factor in
             *       our evaluation.
             */
            var corrections = paragraph.SelectMany(CheckSentenceForCorrections);
            return corrections.Where(correction => correction != null);
        }

        private static IEnumerable<Correction> CheckSentenceForCorrections(Sentence sentence)
        {
            var definitionReader = CreateDefinitionReader(sentence);
            while (definitionReader.MoveNext())
            {
                var correction = CheckIfProperNounNeedsApostrophe(sentence, definitionReader);
                yield return correction;

                var contractionCorrection = CheckIfContractionNeedsApostrophe(sentence, definitionReader);
                yield return contractionCorrection;

                var normalNounCorrection = CheckIfNormalNounHasApostrophe(sentence, definitionReader);
                yield return normalNounCorrection;
            }
        }

        private static Correction CheckIfNormalNounHasApostrophe(Sentence sentence, DefinitionReader definitionReader)
        {
            var current = definitionReader.CurrentDefinition;
            var isRegularNounWithoutApostrophe = current.Type == WordType.Noun &&
                                     !(current.Word + current.Suffix).Equals(definitionReader.CurrentWord, StringComparison.CurrentCultureIgnoreCase);
            if (isRegularNounWithoutApostrophe)
            {
                return new Correction(CorrectionType.IncorrectNounApostrophe, sentence.Source, definitionReader.CurrentWord);
            }

            return null;
        }

        private static Correction CheckIfContractionNeedsApostrophe(Sentence sentence, DefinitionReader definitionReader)
        {
            var current = definitionReader.CurrentDefinition;
            var isContractionWithoutApostrophe = current.Type == WordType.Contraction &&
                                                 !current.Word.Equals(definitionReader.CurrentWord, StringComparison.CurrentCultureIgnoreCase);
            if (isContractionWithoutApostrophe)
            {
                return new Correction(CorrectionType.MissingContractionApostrophe, sentence.Source, definitionReader.CurrentWord);
            }

            return null;
        }


        private static Correction CheckIfProperNounNeedsApostrophe(Sentence sentence, DefinitionReader definitionReader)
        {
            var current = definitionReader.CurrentDefinition;
            var isProperNounSuffixedByS = current.Type == WordType.ProperNoun &&
                                             current.Suffix.Equals("s", StringComparison.CurrentCultureIgnoreCase);
            if (isProperNounSuffixedByS)
            {
                var nextWordIsNoun = definitionReader.NextDefinition.Type == WordType.Noun;
                var previousWordIsIs = definitionReader.PreviousWord.Equals("is",
                    StringComparison.CurrentCultureIgnoreCase);
                if (nextWordIsNoun || previousWordIsIs)
                {
                    var currentWord = definitionReader.CurrentWord;
                    var nextToLastCharacter = currentWord.Substring(currentWord.Length - 2, 1);
                    if (nextToLastCharacter != "'")
                    {
                        return new Correction(CorrectionType.OwnershipByAProperNoun, sentence.Source, currentWord);
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
