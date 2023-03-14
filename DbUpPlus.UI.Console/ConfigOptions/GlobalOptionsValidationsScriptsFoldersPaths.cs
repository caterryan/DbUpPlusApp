using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbUpPlus.UI.Cli.ConfigOptions
{
    sealed partial class GlobalOptionsValidationsScriptsFoldersPaths : IValidateOptions<GlobalOptions>
    {
        public GlobalOptions? _options;

        public GlobalOptionsValidationsScriptsFoldersPaths(IConfiguration configuration)
        {
            _options = configuration
                        .GetSection(GlobalOptions.ConfigurationSectionName)
                        .Get<GlobalOptions>();
        }

        public ValidateOptionsResult Validate(string? name, GlobalOptions options)
        {
            StringBuilder failure = new();


            #region Validations

            DirectoryInfo scriptsFoldersDirectory = new(_options.ScriptsFoldersPaths);
            if (!scriptsFoldersDirectory.Exists)
            {
                failure.AppendLine($"Invalid Configuration Input: '{nameof(_options.ScriptsFoldersPaths)}: {_options.ScriptsFoldersPaths}' is not a valid directory.");
            }

            DirectoryInfo? anytimeScriptsDirectory = new($"{_options.ScriptsFoldersPaths}/anytime");
            if (!anytimeScriptsDirectory.Exists)
            {
                failure.AppendLine($"Subdirectory 'anytime' does not exist at '{nameof(_options.ScriptsFoldersPaths)}: {_options.ScriptsFoldersPaths}'.");
            }

            DirectoryInfo? everytimeScriptsDirectory = new($"{_options.ScriptsFoldersPaths}/everytime");
            if (!everytimeScriptsDirectory.Exists)
            {
                failure.AppendLine($"Subdirectory 'everytime' does not exist at '{nameof(_options.ScriptsFoldersPaths)}: {_options.ScriptsFoldersPaths}'.");
            }

            DirectoryInfo? onetimeScriptsDirectory = new($"{_options.ScriptsFoldersPaths}/onetime");
            if (!onetimeScriptsDirectory.Exists)
            {
                failure.AppendLine($"Subdirectory 'onetime' does not exist at '{nameof(_options.ScriptsFoldersPaths)}: {_options.ScriptsFoldersPaths}'.");
            }

            #endregion


            return failure.Length > 0
            ? ValidateOptionsResult.Fail(failure.ToString())
            : ValidateOptionsResult.Success;
        }
    }
}
