namespace DbUpPlus.UI.Cli.ConfigOptions;

internal class GlobalOptions
{
    public const string ConfigurationSectionName = "DbUpOptions:GlobalOptions";
    public string? ConnectionString { get; set; }
    public string? ScriptsFoldersPaths { get; set; }
}

