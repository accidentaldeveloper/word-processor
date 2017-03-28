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
            var corrections = paragraph.SelectMany(CheckSentenceForProperNounFollowedByNoun);
            return corrections;
        }

        private static IEnumerable<Correction> CheckSentenceForProperNounFollowedByNoun(Sentence sentence)
        {
            var definitionReader = CreateDefinitionReader(sentence);
            while (definitionReader.MoveNext())
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
                            yield return new Correction(CorrectionType.OwnershipByAProperNoun, sentence.Source, currentWord);
                        }

                    }
                }
            }

        }

        private static DefinitionReader CreateDefinitionReader(Sentence sentence)
        {
            return new DefinitionReader(Dictionary, new SentenceReader(sentence));
        }
    }
}
