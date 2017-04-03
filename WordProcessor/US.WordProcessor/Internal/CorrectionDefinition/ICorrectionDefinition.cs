namespace US.WordProcessor.Internal.CorrectionDefinition
{
    internal interface ICorrectionDefinition
    {
        CorrectionType CorrectionType { get; }

        bool WordRequiresCorrection(IDefinitionState definitionState);
    }
}