namespace DbUpPlus.UI.Cli.Commands
{
    internal class RunEveryTimeCommand : Command
    {
        public RunEveryTimeCommand() : base("everytime")
        {
            Description = "Run every time this command is executed";
            TreatUnmatchedTokensAsErrors = true;
        }
    }
}