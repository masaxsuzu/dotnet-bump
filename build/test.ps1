function Run {
	[CmdletBinding()]
	param (
		[Parameter()]
		[string[]]
		$_args
	)
	
	$config = $_args[0]

	Set-Location ..
	dotnet format

	StopIfError "Format error"

	dotnet build -c $config

	StopIfError "Build error"

	dotnet test -c $config

	StopIfError "Test error"

}

function StopIfError{
	param(
		$message
	)

	if($?)
	{
		return
	}

	Write-Error $message
}

$ErrorActionPreference = "Stop"
try {
	Push-Location (Split-Path -Path $MyInvocation.MyCommand.Definition)
	Run $Args
}
finally {
	Pop-Location
}