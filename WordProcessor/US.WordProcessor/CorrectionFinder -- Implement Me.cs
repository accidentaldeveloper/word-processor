using System;
using System.Collections.Generic;
using System.Linq;
using US.WordProcessor.Internal;
using US.WordProcessor.Internal.CorrectionDefinition;

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
            }
        }

        private static DefinitionReader CreateDefinitionReader(Sentence sentence)
        {
            return new DefinitionReader(Dictionary, new SentenceReader(sentence));
        }
    }
}
