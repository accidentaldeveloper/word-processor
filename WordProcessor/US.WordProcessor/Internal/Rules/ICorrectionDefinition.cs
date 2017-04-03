namespace US.WordProcessor.Internal.Rules
{
    internal interface ICorrectionDefinition
    {
        CorrectionType CorrectionType { get; }
        bool WordRequiresCorrection(IDefinitionState definitionState);
    }
}