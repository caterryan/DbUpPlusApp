namespace DbUpPlus.UI.Cli.Commands;

internal class RunCommand : Command
{
    public RunCommand() : base("run")
    {
        Description = "Entrypoint for running migration scripts";
        TreatUnmatchedTokensAsErrors = true;

        AddCommand(new RunOneTimeCommand());
        AddCommand(new RunEveryTimeCommand());
        AddCommand(new RunAnyTimeCommand());
    }
}