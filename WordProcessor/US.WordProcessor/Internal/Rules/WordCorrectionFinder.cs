using System.Collections.Generic;
using System.Linq;

namespace US.WordProcessor.Internal.Rules
{
    internal class WordCorrectionFinder
    {
        private ICorrectionDefinition[] _correctionDefinitions;

        public WordCorrectionFinder(ICorrectionDefinition[] correctionDefinitions)
        {
            _correctionDefinitions = correctionDefinitions;
        }

        public IEnumerable<Correction> GetWordCorrections(IDefinitionState definitionState)
        {
            var needCorrection =
                _correctionDefinitions.Where(definition => definition.WordRequiresCorrection(definitionState));
            return needCorrection.Select(definition => CreateCorrection(definitionState, definition.CorrectionType));
        }

        private Correction CreateCorrection(IDefinitionState definitionState, CorrectionType correctionType) => new Correction(correctionType, definitionState.SourceString, definitionState.CurrentWord);
    }
}