namespace US.WordProcessor.Internal
{
    internal interface ICorrectionRule
    {
        Correction CheckForCorrection(IDefinitionState definitionState);
    }
}