function Run {
	[CmdletBinding()]
	param (
		[Parameter()]
		[string[]]
		$_args
	)

	Set-Location ..

	dotnet pack --output pkg -c Release --no-build

	StopIfError "Package error"

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