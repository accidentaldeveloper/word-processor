using System;
using System.Collections.Generic;
using System.Linq;

namespace US.WordProcessor.Internal.CorrectionDefinition
{
    internal class WordCorrectionFinder
    {
        private ICorrectionDefinition[] _correctionDefinitions;

        public WordCorrectionFinder(ICorrectionDefinition[] correctionDefinitions)
        {
            if (correctionDefinitions == null) throw new ArgumentNullException(nameof(correctionDefinitions));
            _correctionDefinitions = correctionDefinitions;
        }

        public IEnumerable<Correction> GetWordCorrections(IDefinitionState definitionState)
        {
            if (definitionState == null) throw new ArgumentNullException(nameof(definitionState));
            var needCorrection =
                _correctionDefinitions.Where(definition => definition.WordRequiresCorrection(definitionState));
            return needCorrection.Select(definition => CreateCorrection(definitionState, definition.CorrectionType));
        }

        private Correction CreateCorrection(IDefinitionState definitionState, CorrectionType correctionType) => new Correction(correctionType, definitionState.SourceString, definitionState.CurrentWord);
    }
}