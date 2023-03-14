namespace DbUpPlus.UI.Cli.ConfigOptions;

internal class RunOneTimeOptions
{
    public const string ConfigurationSectionName = "DbUpOptions:RunOneTimeOptions";
    public bool DropDatabase { get; set; }
}
