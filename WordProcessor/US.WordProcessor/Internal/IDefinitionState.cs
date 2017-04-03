namespace US.WordProcessor.Internal
{
    internal interface IDefinitionState
    {
        Definition CurrentDefinition { get; }
        string CurrentWord { get; }
        Definition NextDefinition { get; }
        string NextWord { get; }
        Definition PreviousDefinition { get; }
        string PreviousWord { get; }
        string SourceString { get; }
    }
}