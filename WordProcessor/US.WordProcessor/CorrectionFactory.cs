using US.WordProcessor.Internal;
using US.WordProcessor.Internal.Rules;

namespace US.WordProcessor
{
    public static class CorrectionFactory
    {
        public static ICorrectionFinder CreateCorrectionFinder()
        {
            var correctionDefinitions = new ICorrectionDefinition[]
            {
                new ProperNounNeedsApostropheCorrectionDefinition(),
                new ContractionNeedsApostropheCorrectionDefinition(),
                new NormalNounHasApostropheCorrectionDefinition(),
            };
            var wordCorrectionFinder = new WordCorrectionFinder(correctionDefinitions);

            return new CorrectionFinder(wordCorrectionFinder);
        }
    }
}
