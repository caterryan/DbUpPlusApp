# DbUpPlusApp
#### Command line utility that helps manage database migrations by extending DbUp using ideas from Rondhouse and Grate 

## Extensions

### Script Categories
- TODO One-time Scripts - run exactly once per database
- TODO Anytime Scripts - run any time script is added or updated
- TODO Everytime Scripts - run on every execution

### Run UpChanges 
-TODO

### Run DownChanges
-TODO

### Run migrations from CLI or using scripts (e.g. Powershell)
- TODO Create New - drops and builds database from scratch
- TODO Update Only - updates to latest
- TODO AnyTime Service - runs in the background and runs update when AnyTime scripts are updated or added

### Assembly Scripts
- TODO

### File System Scripts
- TODO

### Debug Mode
- TODO 

## Usage
TODO
### Configuration
Accepts various configuration inputs using both [System.CommandLine](https://learn.microsoft.com/en-us/dotnet/standard/commandline/syntax) and [Microsoft.Extensions.Hosting](https://learn.microsoft.com/en-us/dotnet/core/extensions/generic-host)

Development, Staging, and Production profiles/environment are available, but the default is Production.

Configuration is set from highest to lowest priority based on the following inputs:
* Command Line
* Environment Variables
* [Secret Manager](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-7.0&tabs=windows#:~:text=by%20a%20%3A-,Secret%20Manager,-The%20Secret%20Manager) when running in the Development environment
* appsettings.{*environment*}.json
* appsettings.json

#### Command Line
The command line provides help documentation including configuration values that may be set via the command line. Use the -h modifier from the command line to access the help documentation. For example:

	.\DbUpPlus.exe -help

or

	.\DbUpPlus.exe run --h
#### Environment Variables
Configuration values may be changed by setting system environment variables. The variables must be fully qualified. For example:

	DBUPOPTIONS:RUNONETIMEOPTIONS:DROPDATABASE true

#### Setting the Environment
The environment is set by assigning the environment variable DOTNET_{*environment*}. Three environments are provided:

* DOTNET_ENVIRONMENT Development
* DOTNET_ENVIRONMENT Staging
* DOTNET_ENVIRONMENT Production (default)

The environment can also be set by assigning the launch profile to any of the three provided profiles: Development, Staging, and Production. These can be selected in Visual Studio or by command line. For example:

	dotnet run --launch-profile Staging DbUpPlus -- run -h
