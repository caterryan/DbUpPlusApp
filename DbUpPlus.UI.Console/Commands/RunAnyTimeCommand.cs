namespace DbUpPlus.UI.Cli.Commands
{
    internal class RunAnyTimeCommand : Command
    {
        public RunAnyTimeCommand() : base("anytime")
        {
            Description = "Run any time script is added or updated using the DbUpAnytime background service";
            TreatUnmatchedTokensAsErrors = true;
        }
    }
}